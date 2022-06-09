using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelHealthHandler : MonoBehaviour
{
    public float health, maxHealth, damage, flashTimer;
    public AudioClip destroyFX;
    SpriteRenderer projectileSprite;
    Color thisColour;
    public bool flashDamage;
    public GameObject projectileParent, medkitObject, bombObject , goldObject, xpObject, dropObject;
    int childID, childMax, randNum;

    private void Start()
    {
        projectileParent = GameObject.Find("----ProjectileParent----");
        thisColour = GetComponent<SpriteRenderer>().color;

        maxHealth = 75;
        health = maxHealth;

        // RANDOMLY DESTROY ON START TO BALANCE AMOUNT OF IN-WORLD OBJECTS
        randNum = Random.Range(0, 101); // 50% CHANCE
        if (randNum > 50)
        {
            randNum = Random.Range(0, 101); // RANDOMISE AGAIN FOR BETTER RESULTS
            if (randNum > 75) // 75% CHANCE
            {
                if (!transform.parent.parent.gameObject.name.Contains("Spawn")) Destroy(gameObject);
            }
        }

        // RANDOMISED DROP ON DESTROY [ BOMB 31% - POTION 30% - XP 24% - NOTHING 12% - GOLD 3% ]
        randNum = Random.Range(0, 101);
        if (randNum <= 31) dropObject = bombObject;
        else
        {
            randNum -= 31;
            if (randNum <= 30) dropObject = medkitObject;
            else
            {
                randNum -= 30;
                if (randNum <= 24) dropObject = xpObject;
                else
                {
                    randNum -= 24;
                    if (randNum <= 12) dropObject = null;
                    else
                    {
                        randNum -= 12;
                        if (randNum <= 3) dropObject = goldObject;
                    }
                }
            }
        }
    }

    private void Update()
    {
        if (projectileParent.transform.childCount > 0) ProjectileCollisionTest(); // TEST OF COLLISION ONLY WHEN PROJECTILE IS PRESENT

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
            if (dropObject != null) Instantiate(dropObject, new Vector3(transform.position.x, transform.position.y - 0.75f, 0), Quaternion.identity);
            GameObject.Find(">GameManager<").GetComponent<GameManager>().SpawnNew3DFX(destroyFX, transform.position);
            Destroy(this.gameObject);
        }
    }
    private void ProjectileCollisionTest()
    {
        if (!flashDamage) // PROJECTILE (PLAYER SPEAR) COLLISION TEST
        {
            if (childID < childMax && childMax == projectileParent.transform.childCount)
            {
                if (projectileParent.transform.GetChild(childID).GetComponent<SpriteRenderer>() != null)
                {
                    projectileSprite = projectileParent.transform.GetChild(childID).GetComponent<SpriteRenderer>();
                    if (GetComponent<SpriteRenderer>().bounds.Intersects(projectileSprite.bounds))
                    {
                        if (projectileSprite != null)
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
