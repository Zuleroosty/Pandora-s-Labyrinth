using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public float health, maxHealth, damage, flashTimer;
    SpriteRenderer projectileSprite;
    Color thisColour;
    public bool flashDamage;
    GameObject projectileParent;
    int childID, childMax;

    private void Start()
    {
        projectileParent = GameObject.Find("----ProjectileParent----");
        thisColour = GetComponent<SpriteRenderer>().color;

        maxHealth = 150;
        health = maxHealth;
    }

    private void Update()
    {
        ProjectileCollisionTest();

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
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void ProjectileCollisionTest()
    {
        if (!flashDamage) // PROJECTILE (BULLET)
        {
            if (childID < childMax && childMax == projectileParent.transform.childCount)
            {
                projectileSprite = projectileParent.transform.GetChild(childID).GetComponent<SpriteRenderer>();
                if (projectileSprite.gameObject.GetComponent<ProjectileMovement>().isPlayerOwned && projectileSprite.gameObject != null)
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
        }
    }
}
