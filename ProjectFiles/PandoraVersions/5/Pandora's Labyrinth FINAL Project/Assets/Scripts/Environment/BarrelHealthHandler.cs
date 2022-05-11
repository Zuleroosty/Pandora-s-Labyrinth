using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelHealthHandler : MonoBehaviour
{
    public float health, maxHealth, damage, flashTimer;
    SpriteRenderer projectileSprite;
    Color thisColour;
    public bool flashDamage;
    public GameObject projectileParent, ammoObject, goldObject, dropObject;
    int childID, childMax, randNum;

    private void Start()
    {
        projectileParent = GameObject.Find("----ProjectileParent----");
        thisColour = GetComponent<SpriteRenderer>().color;

        maxHealth = 50;
        health = maxHealth;
    }

    private void Update()
    {
        if (dropObject == null)
        {
            if (goldObject != null)
            {
                randNum = Random.Range(0, 101);
                if (randNum <= 70) //AMMO CHANCE
                {
                    dropObject = ammoObject;
                }
                else
                {
                    randNum -= 70;
                    if (randNum <= 30) //GOLD CHANCE
                    {
                        dropObject = goldObject;
                    }
                }
            }
            else dropObject = ammoObject;
        }

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
            Instantiate(dropObject, new Vector3(transform.position.x, transform.position.y - 0.75f, 0), Quaternion.identity);
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
                if (!projectileSprite.name.Contains("RadialBurst"))
                {
                    if (projectileSprite.gameObject.GetComponent<ProjectileMovement>().isPlayerOwned && projectileSprite.gameObject != null)
                    {
                        if (GetComponent<SpriteRenderer>().bounds.Intersects(projectileSprite.bounds) && !projectileSprite.gameObject.name.Contains("Radial"))
                        {
                            TakeDamage(projectileSprite.gameObject.GetComponent<ProjectileMovement>().projectileDamage);
                            flashDamage = true;
                            projectileSprite.gameObject.GetComponent<ProjectileMovement>().OnCollisionDestroy();
                        }
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
