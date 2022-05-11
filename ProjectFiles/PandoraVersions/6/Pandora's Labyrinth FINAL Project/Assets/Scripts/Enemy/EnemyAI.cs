using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject playerRadius, player, projectilePrefab;
    public Vector3 velocity;
    public bool playerInRadius, canMove, activateBoost, canBoost;
    public float speed, spawnSpeed, xSpeed, ySpeed;
    public int radiusDistance, boostTimer, maxBoostTime, boostCooldown, shootCooldown, boostRand, projectileRange;
    public enum type {Normal, Fast, Hard, Ranged}
    public type enemyType;

    // PROJECTILE
    GameObject newProjectile;
    public Sprite projectileFireSprite, projectileHitSprite;
    int projectileSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("----PlayerObjectParent----");
        playerRadius = transform.GetChild(0).gameObject;
        canMove = true;
        shootCooldown = Random.Range(120, 361);
        projectileSpeed = 2;
        maxBoostTime = 20;
        projectileRange = 5;

        switch (enemyType)
        {
            case type.Normal:
                spawnSpeed = Random.Range(0.05f, 0.058f);
                GetComponent<LootSpawner>().xpDrop = Random.Range(5, 9);
                break;
            case type.Fast:
                spawnSpeed = Random.Range(0.075f, 0.085f);
                GetComponent<LootSpawner>().xpDrop = Random.Range(3, 6);
                break;
            case type.Ranged:
                spawnSpeed = Random.Range(0.05f, 0.058f);
                GetComponent<LootSpawner>().xpDrop = Random.Range(3, 6);
                break;
            case type.Hard:
                spawnSpeed = Random.Range(0.04f, 0.048f);
                GetComponent<LootSpawner>().xpDrop = Random.Range(10, 16);
                maxBoostTime = 40;
                break;
        }
        
        speed = spawnSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.parent == null) transform.parent = GameObject.Find("----EnemyParent----").transform;
        if (player == null) player = GameObject.Find("----PlayerObjectParent----");
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {

            // RESET & SET NEW VELOCITY
            xSpeed = 0;
            ySpeed = 0;

            if (player.transform.position.x > transform.position.x) xSpeed = speed;
            else if (player.transform.position.x < transform.position.x) xSpeed = -speed;

            if (player.transform.position.y > transform.position.y) ySpeed = speed;
            else if (player.transform.position.y < transform.position.y) ySpeed = -speed;

            velocity = new Vector3(xSpeed, ySpeed, 0);

            // IS PLAYER IN RADIUS?
            if (GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().bounds.Intersects(playerRadius.GetComponent<SpriteRenderer>().bounds))
            {
                if (!playerInRadius)
                {
                    if (enemyType == type.Ranged) SpawnProjectile();
                    if (enemyType == type.Hard && !activateBoost) SpawnProjectile();
                    playerInRadius = true;
                }
            }
            else
            {
                playerInRadius = false;
                if (canMove)
                {
                    if (activateBoost)
                    {
                        if (speed <= spawnSpeed) speed = spawnSpeed + 0.1f;
                        transform.GetChild(1).GetComponent<BasicAttack>().damage = 2;
                        boostTimer++;
                        if (boostTimer >= maxBoostTime)
                        {
                            transform.GetChild(1).GetComponent<BasicAttack>().damage = 1;
                            activateBoost = false;
                            boostTimer = 0;
                            if (speed > spawnSpeed) speed = spawnSpeed;
                        }
                    }
                    transform.position += velocity;
                }
            }

            if (enemyType == type.Ranged)
            {
                if (Vector3.Distance(transform.position, player.transform.position) < projectileRange)
                {
                    if (shootCooldown > 0) shootCooldown--;
                    if (shootCooldown <= 0) SpawnProjectile();
                }
            }
            if (boostCooldown <= 0 && canBoost && (enemyType == type.Normal || enemyType == type.Hard))
            {
                boostRand = Random.Range(1, 101);
                if (!activateBoost && boostRand > 95)
                {
                    activateBoost = true;
                    boostCooldown = 360;
                }
            }
            if (boostCooldown > 0) boostCooldown--;
        }
    }
    private void SpawnProjectile()
    {
        //-----PROJECTILE SPAWNING---------
        ProjectileMovement projectileManager;
        newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectileManager = newProjectile.GetComponent<ProjectileMovement>();
        projectileManager.isPlayerOwned = false;
        projectileManager.projectileType = ProjectileMovement.type.Fire;
        //---------------------------------

        shootCooldown = Random.Range(120, 360);
    }
}
