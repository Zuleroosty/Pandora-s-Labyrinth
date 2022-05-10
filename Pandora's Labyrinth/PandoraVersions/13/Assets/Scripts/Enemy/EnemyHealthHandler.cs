using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    public float health, maxHealth, damage, flashTimer, offSet;
    public AudioClip deathFX;
    public bool flashDamage, radialDamage;
    SpriteRenderer projectileSprite;
    Color thisColour;
    GameObject projectileParent;
    int childID, childMax;

    private void Start()
    {
        projectileParent = GameObject.Find("----ProjectileParent----");
        thisColour = GetComponent<EnemyAI>().damageObject.GetComponent<SpriteRenderer>().color;
        thisColour.a = 0;
        GetComponent<EnemyAI>().damageObject.GetComponent<SpriteRenderer>().color = thisColour;

        offSet = 0.5f;
        offSet += (GameObject.Find(">GameManager<").GetComponent<LevelHandler>().averagePlayerLevel + GameObject.Find(">GameManager<").GetComponent<LevelHandler>().currentPlayLevel) * 0.2f;

        if (maxHealth <= 0)
        {
            if (this.name.Contains("Ranged")) maxHealth = 330 * offSet;
            if (this.name.Contains("Fast")) maxHealth = 105 * offSet;
            if (this.name.Contains("Normal")) maxHealth = 110 * offSet;
            if (this.name.Contains("Boss")) maxHealth = 3200 * offSet;

            health = maxHealth;
        }

        if (!this.name.Contains("Boss")) GameObject.Find(">GameManager<").GetComponent<GameManager>().currentRoomParent.GetComponent<RoomHandler>().enemyCount++;
    }

    private void Update()
    {
        ProjectileCollisionTest(); //REPEATED TO AVOID MISSING
        ProjectileCollisionTest();

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

        GetComponent<EnemyAI>().damageObject.GetComponent<SpriteRenderer>().color = thisColour;
    }

    public void TakeDamage(float damageAmount)
    {
        if (GetComponent<EnemyAI>().activateBoost) GetComponent<EnemyAI>().boostTimer = 20;
        health -= damageAmount;
        if (health <= 0)
        {
            GameObject.Find(">GameManager<").GetComponent<GameManager>().SpawnNew3DFX(deathFX, transform.position);
            // UPDATE STAT TRACKER
            if (this.name.Contains("Ranged")) GameObject.Find(">GameManager<").GetComponent<StatHandler>().scorpionsKilled++;
            if (this.name.Contains("Fast")) GameObject.Find(">GameManager<").GetComponent<StatHandler>().spidersKilled++;
            if (this.name.Contains("Normal")) GameObject.Find(">GameManager<").GetComponent<StatHandler>().goblinsKilled++;
            if (this.name.Contains("Boss")) GameObject.Find(">GameManager<").GetComponent<StatHandler>().minotaursKilled++;
            else GameObject.Find(">GameManager<").GetComponent<GameManager>().currentRoomParent.GetComponent<RoomHandler>().enemyCount--;

            Destroy(this.gameObject);
        }
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
                            if (this.name.Contains("Boss")) TakeDamage(maxHealth/5f);
                            else TakeDamage(200 * offSet);
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
                    projectileSprite.gameObject.GetComponent<ProjectileMovement>().OnCollisionDestroy(4);
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
}
