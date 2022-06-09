using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject playerRadius, player, projectilePrefab, spriteObject, collisionObject, damageObject;
    public SpriteRenderer thisSpriteRen;
    public Vector3 velocity;
    public bool playerInRadius, canMove, activateBoost, canBoost;
    public float speed, spawnSpeed, xSpeed, ySpeed;
    public int radiusDistance, boostTimer, maxBoostTime, boostCooldown, shootCooldown, boostRand, projectileRange;
    public enum type {Normal, Fast, Hard, Ranged}
    public type enemyType;

    // PROJECTILE
    GameObject newProjectile;
    public Sprite projectileFireSprite, projectileHitSprite;

    // Start is called before the first frame update
    void Awake()
    {
        thisSpriteRen = spriteObject.GetComponent<SpriteRenderer>();
        player = GameObject.Find("----PlayerObjectParent----");
        projectileRange = 5;

        switch (enemyType)
        {
            case type.Normal:
                GetComponent<LootSpawner>().xpDrop = Random.Range(5, 9);
                break;
            case type.Fast:
                GetComponent<LootSpawner>().xpDrop = Random.Range(3, 6);
                break;
            case type.Ranged:
                GetComponent<LootSpawner>().xpDrop = Random.Range(3, 6);
                break;
            case type.Hard:
                GetComponent<LootSpawner>().xpDrop = Random.Range(10, 16);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (transform.parent == null) transform.parent = GameObject.Find("----EnemyParent----").transform;
            if (player == null) player = GameObject.Find("----PlayerObjectParent----");

            // IS PLAYER IN RADIUS?
            if (GameObject.Find("PCollision").GetComponent<SpriteRenderer>().bounds.Intersects(playerRadius.GetComponent<SpriteRenderer>().bounds))
            {
                if (shootCooldown > 0) shootCooldown--;
                if (shootCooldown <= 0)
                {
                    if (enemyType == type.Ranged) SpawnProjectile();
                    if (enemyType == type.Hard && !activateBoost) SpawnProjectile();
                }
                playerInRadius = true;
            }
            else playerInRadius = false;
        }
        else if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Reset) Destroy(gameObject);
    }
    private void SpawnProjectile()
    {
        //-----PROJECTILE SPAWNING---------
        ProjectileMovement projectileManager;
        newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectileManager = newProjectile.GetComponent<ProjectileMovement>();
        projectileManager.isPlayerOwned = false;
        projectileManager.fireColour = ProjectileMovement.colour.Blue;
        //---------------------------------

        shootCooldown = Random.Range(60, 240);
    }
}
