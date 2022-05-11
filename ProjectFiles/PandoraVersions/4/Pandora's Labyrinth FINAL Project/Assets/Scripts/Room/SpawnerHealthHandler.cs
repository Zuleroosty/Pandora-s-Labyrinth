using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHealthHandler : MonoBehaviour
{
    public float health, maxHealth, damage, flashTimer;
    PlayerController playerCon;
    SpriteRenderer projectileSprite;
    Color thisColour;
    public bool flashDamage;
    GameObject projectileParent;
    int childID, childMax, radialCooldown;

    private void Start()
    {
        projectileParent = GameObject.Find("----ProjectileParent----");
        playerCon = GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>();
        thisColour = GetComponent<SpriteRenderer>().color;

        maxHealth = 1750;
        health = maxHealth;

        radialCooldown = 1;
    }

    private void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().currentRoomParent == this.transform.parent.parent.gameObject)
        {
            ProjectileCollisionTest();
            ProjectileCollisionTest();
        }

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

        if (health <= 0)
        {
            print("SpawnerDestroyed");
            GameObject.Find(">GameManager<").GetComponent<GameManager>().currentRoomParent.GetComponent<RoomHandler>().destroyedSpawners++;
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        flashDamage = true;
        health -= damageAmount;
    }
    private void ProjectileCollisionTest()
    {
        if (childID < childMax && childMax == projectileParent.transform.childCount)
        {
            projectileSprite = projectileParent.transform.GetChild(childID).GetComponent<SpriteRenderer>();
            if (GetComponent<SpriteRenderer>().bounds.Intersects(projectileSprite.bounds))
            {
                TakeDamage(projectileSprite.gameObject.GetComponent<ProjectileMovement>().projectileDamage);
                projectileSprite.gameObject.GetComponent<ProjectileMovement>().OnCollisionDestroy();
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
