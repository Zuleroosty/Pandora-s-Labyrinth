using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    public float health, maxHealth, damage, flashTimer, offSet;
    SpriteRenderer projectileSprite;
    Color thisColour;
    public bool flashDamage, radialDamage;
    GameObject projectileParent;
    int childID, childMax;

    private void Start()
    {
        projectileParent = GameObject.Find("----ProjectileParent----");
        thisColour = GetComponent<EnemyAI>().damageObject.GetComponent<SpriteRenderer>().color;
        thisColour.a = 0;
        GetComponent<EnemyAI>().damageObject.GetComponent<SpriteRenderer>().color = thisColour;

        offSet = GameObject.Find(">GameManager<").GetComponent<LevelHandler>().averagePlayerLevel;
        offSet += 0.5f;

        if (!this.name.Contains("Boss")) GameObject.Find(">GameManager<").GetComponent<GameManager>().currentRoomParent.GetComponent<RoomHandler>().enemyCount++;
    }

    private void FixedUpdate()
    {
        if (flashDamage)
        {
            if (flashTimer < 15) flashTimer++;
            if (flashTimer >= 5 && flashTimer < 10) thisColour.a = 1;
            if (flashTimer >= 10) thisColour.a = 0;
            if (flashTimer >= 15)
            {
                flashDamage = false;
                flashTimer = 0;
            }
        }
        else
        {
            ProjectileCollisionTest(); //REPEATED TO AVOID MISSING
            ProjectileCollisionTest();
            ProjectileCollisionTest();
        }
        GetComponent<EnemyAI>().damageObject.GetComponent<SpriteRenderer>().color = thisColour;
        if (maxHealth <= 0)
        {
            if (this.name.Contains("Ranged")) maxHealth = 125 * offSet;
            if (this.name.Contains("Fast")) maxHealth = 88 * offSet;
            if (this.name.Contains("Normal")) maxHealth = 175 * offSet;
            if (this.name.Contains("Boss")) maxHealth = 100000 * offSet;

            health = maxHealth;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (GetComponent<EnemyAI>().activateBoost) GetComponent<EnemyAI>().boostTimer = 20;
        health -= damageAmount;
        if (health <= 0) Destroy(this.gameObject);
    }
    private void ProjectileCollisionTest()
    {
        if (childID < childMax && childMax == projectileParent.transform.childCount)
        {
            projectileSprite = projectileParent.transform.GetChild(childID).GetComponent<SpriteRenderer>();
            if (projectileSprite.name.Contains("Explosion"))
            {
                if (projectileSprite.GetComponent<SpriteRenderer>().bounds.Intersects(GetComponent<EnemyAI>().collisionObject.GetComponent<SpriteRenderer>().bounds))
                {
                        transform.position += (transform.position - projectileSprite.transform.position);
                        if (!radialDamage)
                        {
                            TakeDamage(maxHealth/2.5f);
                            flashDamage = true;
                            radialDamage = true;
                        }
                }
                else radialDamage = false;
            }
            else if (projectileSprite.gameObject.GetComponent<ProjectileMovement>().isPlayerOwned)
            {
                if (GetComponent<EnemyAI>().collisionObject.GetComponent<SpriteRenderer>().bounds.Intersects(projectileSprite.bounds))
                {
                    flashDamage = true;
                    projectileSprite.gameObject.GetComponent<ProjectileMovement>().enemyObject = this.gameObject;
                    projectileSprite.gameObject.GetComponent<ProjectileMovement>().OnCollisionDestroy();
                }
            }
        }
        else
        {
            childID = -1;
            childMax = projectileParent.transform.childCount;
        }
        childID++;
    }
    private void OnDestroy()
    {
        GameObject.Find(">GameManager<").GetComponent<GameManager>().currentRoomParent.GetComponent<RoomHandler>().enemyCount--;
    }
}
