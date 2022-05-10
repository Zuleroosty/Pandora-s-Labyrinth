using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelHealthHandler : MonoBehaviour
{
    public float health, maxHealth, damage, flashTimer;
    public AudioClip hitFX;
    SpriteRenderer projectileSprite;
    Color thisColour;
    public bool flashDamage;
    public GameObject projectileParent, medkitObject, bombObject , goldObject, dropObject, xpObject;
    int childID, childMax, randNum;

    private void Start()
    {
        projectileParent = GameObject.Find("----ProjectileParent----");
        thisColour = GetComponent<SpriteRenderer>().color;

        maxHealth = 75;
        health = maxHealth;
    }

    private void Update()
    {
        
        if (dropObject == null)
        {
            randNum = Random.Range(0, 101);
            if (randNum > 65) //DROP NOTHING
            {
                randNum -= 65;
                if (randNum <= 15) //MEDKIT CHANCE
                {
                    if (GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().totalMedkits + GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().totalBombs < 5) dropObject = medkitObject;
                    else dropObject = xpObject;
                }
                else
                {
                    randNum -= 15;
                    if (randNum <= 10) //BOMB CHANCE
                    {
                        if (GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().totalMedkits + GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().totalBombs < 5) dropObject = bombObject;
                        else dropObject = xpObject;
                    }
                    else
                    {
                        randNum -= 10;
                        if (randNum <= 3) //GOLD CHANCE
                        {
                            dropObject = xpObject;
                        }
                        else
                        {
                            dropObject = goldObject;
                        }
                    }
                }
            }
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
                if (projectileParent.transform.GetChild(childID).GetComponent<SpriteRenderer>() != null)
                {
                    projectileSprite = projectileParent.transform.GetChild(childID).GetComponent<SpriteRenderer>();
                    if (GetComponent<SpriteRenderer>().bounds.Intersects(projectileSprite.bounds))
                    {
                        if (projectileSprite.gameObject.name.Contains("RadialBurst(Clone)") && GameObject.Find("RadialBurst(Clone)").GetComponent<RadialBurst>().bombExplode)
                        {
                            TakeDamage(health);
                        }
                        else if (projectileSprite.gameObject.GetComponent<ProjectileMovement>().isPlayerOwned)
                        {
                            TakeDamage(projectileSprite.gameObject.GetComponent<ProjectileMovement>().projectileDamage);
                            flashDamage = true;
                            projectileSprite.gameObject.GetComponent<ProjectileMovement>().OnCollisionDestroy(1);
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
