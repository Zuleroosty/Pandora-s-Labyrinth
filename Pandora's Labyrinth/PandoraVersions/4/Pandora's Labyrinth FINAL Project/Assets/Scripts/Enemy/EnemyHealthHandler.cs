using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    public float health, maxHealth, damage, flashTimer;
    SpriteRenderer projectileSprite;
    Color thisColour;
    public bool flashDamage, radialDamage;
    GameObject projectileParent;
    int childID, childMax;

    private void Start()
    {
        projectileParent = GameObject.Find("----ProjectileParent----");
        thisColour = GetComponent<SpriteRenderer>().color;

        if (this.name.Contains("Ranged")) maxHealth = 40;
        if (this.name.Contains("Fast")) maxHealth = 20;
        if (this.name.Contains("Normal")) maxHealth = 60;
        if (this.name.Contains("Boss")) maxHealth = 10000;
        else GameObject.Find(">GameManager<").GetComponent<GameManager>().currentRoomParent.GetComponent<RoomHandler>().enemyCount++;

        health = maxHealth;
    }

    private void FixedUpdate()
    {
        if (flashDamage)
        {
            if (flashTimer < 15) flashTimer++;
            if (flashTimer >= 5 && flashTimer < 10) GetComponent<SpriteRenderer>().color = Color.red;
            if (flashTimer >= 10) GetComponent<SpriteRenderer>().color = thisColour;
            if (flashTimer >= 15)
            {
                flashDamage = false;
                flashTimer = 0;
            }
        }
        else
        {
            ProjectileCollisionTest(); //REPEATED TO AVOID NOT HITTING
            ProjectileCollisionTest();
            ProjectileCollisionTest();
            ProjectileCollisionTest();
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
            if (projectileSprite.gameObject.GetComponent<ProjectileMovement>().isPlayerOwned)
            {
                if (GetComponent<SpriteRenderer>().bounds.Intersects(projectileSprite.bounds))
                {
                    TakeDamage(projectileSprite.gameObject.GetComponent<ProjectileMovement>().projectileDamage);
                    flashDamage = true;
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

        if (GameObject.Find("RadialBurst(Clone)").GetComponent<SpriteRenderer>().bounds.Intersects(GetComponent<SpriteRenderer>().bounds))
        {
            if (!radialDamage)
            {
                TakeDamage(76);
                flashDamage = true;
                radialDamage = true;
            }
        }
        else radialDamage = false;
    }
    private void OnDestroy()
    {
        GameObject.Find(">GameManager<").GetComponent<GameManager>().currentRoomParent.GetComponent<RoomHandler>().enemyCount--;
    }
}
