using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public GameObject playerObject, projectileObject, enemyObject;
    public bool disableCollision;
    PlayerController playerScript;
    EnemyAI enemyScript;
    SpriteRenderer playerSprite, thisSprite;
    Vector3 pushBack, enemyPushBack;
    float plBounds, prBounds, puBounds, pdBounds, playerSizeX, playerSizeY, offSet;
    float elBounds, erBounds, euBounds, edBounds, eSizeX, eSizeY, eOffSet;

    // PROJECTILE
    GameObject projectileParent;
    SpriteRenderer projectileSprite;
    public int projectileChildMax, projectileChildID;

    // ENEMY
    GameObject enemyParent;
    SpriteRenderer enemySprite;
    public int enemyChildMax, enemyChildID;

    // Start is called before the first frame update
    void Start()
    {
        projectileParent = GameObject.Find("----ProjectileParent----");
        enemyParent = GameObject.Find("----EnemyParent----");
        thisSprite = GetComponent<SpriteRenderer>();
        offSet = 0.09f;
        eOffSet = offSet;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (!disableCollision)
            {
                if (!this.name.Contains("Player"))
                {
                    UpdatePlayerCollision();
                    if (!this.name.Contains("Enemy")) UpdateProjectileCollision();
                }
                if (this.name.Contains("Player")) UpdateEnemyCollision();
            }
        }
        else if (GameObject.Find("----PlayerObjectParent----") != null)
        {
            playerObject = GameObject.Find("----PlayerObjectParent----");
            playerScript = playerObject.GetComponent<PlayerController>();
            playerSprite = GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>();
        }
    }
    private void UpdatePlayerCollision()
    {
        // Reset pushback
        pushBack = new Vector3(0, 0);

        // Update size of player
        playerSizeX = playerSprite.bounds.max.x - playerSprite.bounds.min.x;
        playerSizeY = playerSprite.bounds.max.y - playerSprite.bounds.min.y;

        // Set player bounds
        plBounds = playerObject.transform.position.x - (playerSizeX / 2);
        prBounds = playerObject.transform.position.x + (playerSizeX / 2);
        pdBounds = playerObject.transform.position.y - (playerSizeY / 2);
        puBounds = playerObject.transform.position.y + (playerSizeY / 2);

        //Determine player relative position
        if (playerObject.transform.position.x > thisSprite.bounds.max.x - offSet)
        {
            if (plBounds < thisSprite.bounds.max.x && plBounds > thisSprite.bounds.min.x) // Right
            {
                pushBack = new Vector3((playerScript.speed + 0.02f), 0);
            }
        }
        if (playerObject.transform.position.x < thisSprite.bounds.min.x + offSet)
        {
            if (prBounds < thisSprite.bounds.max.x && prBounds > thisSprite.bounds.min.x) // Left
            {
                pushBack = new Vector3((playerScript.speed + 0.02f) * -1, 0);
            }
        }
        if (playerObject.transform.position.y > thisSprite.bounds.max.y - offSet)
        {
            if (pdBounds < thisSprite.bounds.max.y && pdBounds > thisSprite.bounds.min.y) // Up
            {
                pushBack = new Vector3(0, (playerScript.speed + 0.02f));
            }
        }
        if (playerObject.transform.position.y < thisSprite.bounds.min.y + offSet)
        {
            if (puBounds < thisSprite.bounds.max.y && puBounds > thisSprite.bounds.min.y) // Down
            {
                pushBack = new Vector3(0, (playerScript.speed + 0.02f) * -1);
            }
        }

        // Collision Trigger
        if (thisSprite.bounds.Intersects(playerSprite.bounds))
        {
            playerObject.transform.position += pushBack;
        }
    }

    private void UpdateEnemyCollision()
    {
        if (enemyParent.transform.childCount > 0)
        {
            // ENEMY COLLISION
            if (enemyChildID < enemyChildMax && enemyChildMax == enemyParent.transform.childCount)
            {
                enemySprite = enemyParent.transform.GetChild(enemyChildID).GetComponent<SpriteRenderer>();
                if (GetComponent<SpriteRenderer>().bounds.Intersects(enemySprite.bounds))
                {
                    if (enemySprite.gameObject != this.gameObject)
                    {
                        enemyObject = enemySprite.gameObject;
                        enemyScript = enemyObject.GetComponent<EnemyAI>();
                    }
                    else enemyObject = null;
                }
            }
            else
            {
                enemyChildID = -1;
                enemyChildMax = enemyParent.transform.childCount;
            }
            enemyChildID++;

            if (enemyObject != null)
            {
                // Reset pushback
                enemyPushBack = new Vector3(0, 0);

                // Update size of player
                eSizeX = enemySprite.bounds.max.x - enemySprite.bounds.min.x;
                eSizeY = enemySprite.bounds.max.y - enemySprite.bounds.min.y;

                // Set player bounds
                elBounds = enemyObject.transform.position.x - (eSizeX / 2);
                erBounds = enemyObject.transform.position.x + (eSizeX / 2);
                edBounds = enemyObject.transform.position.y - (eSizeY / 2);
                euBounds = enemyObject.transform.position.y + (eSizeY / 2);

                //Determine player relative position
                if (enemyObject.transform.position.x > thisSprite.bounds.max.x - offSet)
                {
                    if (elBounds < thisSprite.bounds.max.x && elBounds > thisSprite.bounds.min.x) // Right
                    {
                        enemyPushBack = new Vector3((enemyScript.speed + 0.02f), 0);
                    }
                }
                if (enemyObject.transform.position.x < thisSprite.bounds.min.x + offSet)
                {
                    if (erBounds < thisSprite.bounds.max.x && erBounds > thisSprite.bounds.min.x) // Left
                    {
                        enemyPushBack = new Vector3((enemyScript.speed + 0.02f) * -1, 0);
                    }
                }
                if (enemyObject.transform.position.y > thisSprite.bounds.max.y - offSet)
                {
                    if (edBounds < thisSprite.bounds.max.y && edBounds > thisSprite.bounds.min.y) // Up
                    {
                        enemyPushBack = new Vector3(0, (enemyScript.speed + 0.02f));
                    }
                }
                if (enemyObject.transform.position.y < thisSprite.bounds.min.y + offSet)
                {
                    if (euBounds < thisSprite.bounds.max.y && euBounds > thisSprite.bounds.min.y) // Down
                    {
                        enemyPushBack = new Vector3(0, (enemyScript.speed + 0.02f) * -1);
                    }
                }

                // Collision Trigger
                if (thisSprite.bounds.Intersects(enemySprite.bounds))
                {
                    enemyObject.transform.position += enemyPushBack;
                }
            }
        }
    }
    private void UpdateProjectileCollision()
    {
        // PROJECTILE COLLISION
        if (projectileChildID < projectileChildMax && projectileChildMax == projectileParent.transform.childCount)
        {
            projectileSprite = projectileParent.transform.GetChild(projectileChildID).GetComponent<SpriteRenderer>();
            projectileObject = projectileSprite.gameObject;
            if (GetComponent<SpriteRenderer>().bounds.Intersects(projectileSprite.bounds))
            {
                projectileObject.GetComponent<ProjectileMovement>().OnCollisionDestroy();
            }
        }
        else
        {
            projectileChildID = -1;
            projectileChildMax = projectileParent.transform.childCount;
        }
        projectileChildID++;
    }
}
