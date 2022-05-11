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
    PlayerController playerCon;
    int childID, childMax, randNum;

    private void Start()
    {
        projectileParent = GameObject.Find("----ProjectileParent----");
        thisColour = GetComponent<SpriteRenderer>().color;

        maxHealth = 75;
        health = maxHealth;

        randNum = Random.Range(0, 101);
        if (randNum > 50)
        {
            randNum = Random.Range(0, 101);
            if (randNum > 75)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        if (playerCon == null) playerCon = GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>();
        else
        {
            if (playerCon.health < playerCon.maxHealth / 4) dropObject = medkitObject;
            else
            {
                if (playerCon.totalMedkits == 3) dropObject = bombObject;
                else if (playerCon.totalBombs == 3) dropObject = medkitObject;
                else
                {
                    randNum = Random.Range(0, 101);
                    if (randNum <= 65) dropObject = medkitObject;
                    else
                    {
                        randNum -= 65;
                        if (randNum <= 30) dropObject = bombObject;
                        else
                        {
                            randNum -= 30;
                            if (randNum <= 5) dropObject = goldObject;
                            else dropObject = xpObject;
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
