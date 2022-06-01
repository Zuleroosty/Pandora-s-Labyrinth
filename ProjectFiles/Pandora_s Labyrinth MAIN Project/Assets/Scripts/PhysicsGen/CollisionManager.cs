using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public GameObject playerObject, projectileObject, enemyObject;
    public AudioClip woodFX, stoneFX, metalFX;
    public bool disableCollision, projectile, player, enemy;
    PlayerController playerScript;
    EnemyAI enemyScript;
    SpriteRenderer playerSprite, thisSprite;
    Vector3 pushBack, enemyPushBack;
    float plBounds, prBounds, puBounds, pdBounds, playerSizeX, playerSizeY, offSet;
    float elBounds, erBounds, euBounds, edBounds, eSizeX, eSizeY, eOffSet;

    public enum type { wood, stone, metal}
    public type objectType;
    int objectMaterial;

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
        if (this.name.Contains("Door")) offSet = 0.01f;
        else offSet = 0.05f;
        eOffSet = offSet;

        if (GameObject.Find("PCollision") != null)
        {
            playerObject = GameObject.Find("PCollision");
            playerScript = GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>();
            playerSprite = playerObject.GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("PCollision") != playerObject)
        {
            playerObject = GameObject.Find("PCollision");
            playerScript = GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>();
            playerSprite = playerObject.GetComponent<SpriteRenderer>();
        }
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (!disableCollision)
            {
                if (player)
                { 
                    UpdatePlayerCollision(); // COLLISION TESTS DOUBLED PER FRAME TO ENSURE CORRECT COLLISIONS
                    UpdatePlayerCollision();
                }
                if (projectile)
                {
                    UpdateProjectileCollision();
                    UpdateProjectileCollision();
                }
                if (enemy)
                {
                    UpdateEnemyCollision();
                    UpdateEnemyCollision();
                }
            }
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
        if (playerSprite.transform.position.y > thisSprite.bounds.max.y - offSet)
        {
            if (pdBounds < thisSprite.bounds.max.y && pdBounds > thisSprite.bounds.min.y) // Up
            {
                pushBack = new Vector3(0, (playerScript.speed + 0.02f), 0);
            }
        }
        if (playerSprite.transform.position.y < thisSprite.bounds.min.y + offSet)
        {
            if (puBounds < thisSprite.bounds.max.y && puBounds > thisSprite.bounds.min.y) // Down
            {
                pushBack = new Vector3(0, (playerScript.speed + 0.02f) * -1, 0);
            }
        }
        if (playerSprite.transform.position.x > thisSprite.bounds.max.x - offSet)
        {
            if (plBounds < thisSprite.bounds.max.x && plBounds > thisSprite.bounds.min.x) // Right
            {
                pushBack = new Vector3((playerScript.speed + 0.02f), 0, 0);
            }
        }
        if (playerSprite.transform.position.x < thisSprite.bounds.min.x + offSet)
        {
            if (prBounds < thisSprite.bounds.max.x && prBounds > thisSprite.bounds.min.x) // Left
            {
                pushBack = new Vector3((playerScript.speed + 0.02f) * -1, 0);
            }
        }

        // Collision Trigger
        if (thisSprite.bounds.Intersects(playerSprite.bounds))
        {
            GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().isColliding = true;
            GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().collisionObject = this.gameObject;
            if (this.name.Contains("Door4"))
            {
                print("4");
                if (transform.localPosition.x < 0) GameObject.Find("----PlayerObjectParent----").transform.position += new Vector3(1, 0, 0);
                if (transform.localPosition.x > 0) GameObject.Find("----PlayerObjectParent----").transform.position -= new Vector3(1, 0, 0);
                if (transform.localPosition.y < 0) GameObject.Find("----PlayerObjectParent----").transform.position += new Vector3(0, 1, 0);
                if (transform.localPosition.y > 0) GameObject.Find("----PlayerObjectParent----").transform.position -= new Vector3(0, 1, 0);
            }
            else GameObject.Find("----PlayerObjectParent----").transform.position += pushBack;
        }
        else if (GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().collisionObject == this.gameObject)
        {
            GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().collisionObject = null;
            GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().isColliding = false;
        }
    }

    private void UpdateEnemyCollision()
    {
        if (enemyParent.transform.childCount > 0)
        {
            // ENEMY COLLISION
            if (enemyChildID < enemyChildMax && enemyChildMax == enemyParent.transform.childCount)
            {
                enemyObject = enemyParent.transform.GetChild(enemyChildID).gameObject;
                if (GetComponent<SpriteRenderer>().bounds.Intersects(enemyObject.GetComponent<EnemyAI>().collisionObject.GetComponent<SpriteRenderer>().bounds))
                {
                    if (enemyObject.gameObject != this.gameObject)
                    {
                        enemyScript = enemyObject.GetComponent<EnemyAI>();
                        enemySprite = enemyScript.spriteObject.GetComponent<SpriteRenderer>();
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
                if (enemySprite != null)
                {
                    eSizeX = enemySprite.bounds.max.x - enemySprite.bounds.min.x;
                    eSizeY = enemySprite.bounds.max.y - enemySprite.bounds.min.y;
                }

                // Set player bounds
                elBounds = enemyObject.transform.position.x - (eSizeX / 2);
                erBounds = enemyObject.transform.position.x + (eSizeX / 2);
                edBounds = enemyObject.transform.position.y - (eSizeY / 2);
                euBounds = enemyObject.transform.position.y + (eSizeY / 2);

                if (enemyScript != null)
                {
                    //Determine player relative position
                    if (enemyObject.transform.position.x > thisSprite.bounds.max.x - offSet)
                    {
                        if (elBounds < thisSprite.bounds.max.x && elBounds > thisSprite.bounds.min.x) // Right
                        {
                            enemyPushBack = new Vector3(0.2f, 0, 0);
                        }
                    }
                    if (enemyObject.transform.position.x < thisSprite.bounds.min.x + offSet)
                    {
                        if (erBounds < thisSprite.bounds.max.x && erBounds > thisSprite.bounds.min.x) // Left
                        {
                            enemyPushBack = new Vector3(0.2f * -1, 0, 0);
                        }
                    }
                    if (enemyObject.transform.position.y > thisSprite.bounds.max.y - offSet)
                    {
                        if (edBounds < thisSprite.bounds.max.y && edBounds > thisSprite.bounds.min.y) // Up
                        {
                            enemyPushBack = new Vector3(0, 0.2f, 0);
                        }
                    }
                    if (enemyObject.transform.position.y < thisSprite.bounds.min.y + offSet)
                    {
                        if (euBounds < thisSprite.bounds.max.y && euBounds > thisSprite.bounds.min.y) // Down
                        {
                            enemyPushBack = new Vector3(0, 0.2f * -1, 0);
                        }
                    }
                }

                // Collision Trigger
                if (thisSprite != null && enemySprite != null)
                {
                    if (thisSprite.bounds.Intersects(enemySprite.bounds))
                    {
                        enemyObject.transform.position += enemyPushBack;
                    }
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
            if (GetComponent<SpriteRenderer>().bounds.Intersects(projectileSprite.bounds) && !projectileSprite.name.Contains("Explosion"))
            {
                switch(objectType)
                {
                    case type.wood:
                        projectileObject.GetComponent<ProjectileMovement>().OnCollisionDestroy(1); 
                        break;
                    case type.stone:
                        projectileObject.GetComponent<ProjectileMovement>().OnCollisionDestroy(2); 
                        break;
                    case type.metal:
                        projectileObject.GetComponent<ProjectileMovement>().OnCollisionDestroy(3); 
                        break;
                }
                
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
