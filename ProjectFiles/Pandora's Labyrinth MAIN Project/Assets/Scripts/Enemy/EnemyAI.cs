using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public AudioClip rand1, rand2;
    public GameObject playerRadius, player, projectilePrefab, spriteObject, collisionObject, damageObject, projectileSpawn, pathObject;
    public SpriteRenderer thisSpriteRen;
    public Vector3 velocity, projectileSpawnPos, shootToPos;
    public bool playerInRadius, canMove, activateBoost, canBoost;
    public float speed, spawnSpeed, xSpeed, ySpeed;
    public int radiusDistance, boostTimer, maxBoostTime, boostCooldown, shootCooldown, boostRand, projectileRange, fxTimer, fxRand;
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
        fxRand = 15 * Random.Range(2, 7);

        switch (enemyType)
        {
            case type.Normal:
                GetComponent<LootSpawner>().xpDrop = Random.Range(5, 9);
                spawnSpeed = 10f;
                speed = (spawnSpeed * (1 + (GameObject.Find(">GameManager<").GetComponent<LevelHandler>().averagePlayerLevel) * 0.2f)) * Random.Range(0.9f, 1.06f);
                break;
            case type.Fast:
                GetComponent<LootSpawner>().xpDrop = Random.Range(1, 3);
                spawnSpeed = 15f;
                speed = (spawnSpeed * (1 + (GameObject.Find(">GameManager<").GetComponent<LevelHandler>().averagePlayerLevel) * 0.2f)) * Random.Range(0.9f, 1.06f);
                break;
            case type.Ranged:
                GetComponent<LootSpawner>().xpDrop = Random.Range(3, 6);
                spawnSpeed = 5f;
                speed = (spawnSpeed * (1 + (GameObject.Find(">GameManager<").GetComponent<LevelHandler>().averagePlayerLevel) * 0.2f)) * Random.Range(0.9f, 1.06f);
                break;
            case type.Hard:
                GetComponent<LootSpawner>().xpDrop = Random.Range(10, 16);
                spawnSpeed = 8f;
                speed = (spawnSpeed * (1 + (GameObject.Find(">GameManager<").GetComponent<LevelHandler>().averagePlayerLevel) * 0.2f)) * Random.Range(0.9f, 1.06f);
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

            PlayRandomFX();

            // IS PLAYER IN RADIUS?
            if (GameObject.Find("PCollision").GetComponent<SpriteRenderer>().bounds.Intersects(playerRadius.GetComponent<SpriteRenderer>().bounds))
            {
                if (shootCooldown > 0) shootCooldown--;
                if (shootCooldown <= 0)
                {
                    shootCooldown = Random.Range(60, 240);
                    if (enemyType == type.Ranged) SpawnProjectile();
                    if (enemyType == type.Hard) SpawnProjectile();
                }
                playerInRadius = true;
            }
            else playerInRadius = false;
        }
        else if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Reset) Destroy(gameObject);
    }
    private void PlayRandomFX()
    {
        if (fxTimer < fxRand) fxTimer++;
        if (fxTimer >= fxRand)
        {
            fxRand = Random.Range(1, 3);
            fxTimer = 0;
            GetComponent<AudioSource>().volume = GameObject.Find(">GameManager<").GetComponent<AudioHandler>().effectsMax + Random.Range(-0.09f, 0.09f);
            GetComponent<AudioSource>().pitch = Random.Range(0.95f, 1.05f);
            if (fxRand == 1) GetComponent<AudioSource>().PlayOneShot(rand1);
            if (fxRand == 2) GetComponent<AudioSource>().PlayOneShot(rand2);
            fxRand = 30 * Random.Range(2, 7);
        }
    }
    private void SpawnProjectile()
    {
        // GET PROJECTILE SPAWN POSITION
        if (projectileSpawn == null) projectileSpawnPos = transform.position;
        else projectileSpawnPos = projectileSpawn.transform.position;

        //-----PROJECTILE SPAWNING---------
        ProjectileMovement projectileManager;
        newProjectile = Instantiate(projectilePrefab, projectileSpawnPos, Quaternion.identity);
        projectileManager = newProjectile.GetComponent<ProjectileMovement>();
        projectileManager.moveToPosition = GameObject.Find("----PlayerObjectParent----").transform.position;
        projectileManager.isPlayerOwned = false;
        projectileManager.fireColour = ProjectileMovement.colour.Magenta;
        //---------------------------------

        GetComponent<AudioSource>().volume = GameObject.Find(">GameManager<").GetComponent<AudioHandler>().effectsMax + Random.Range(-0.09f, 0.09f);
        GetComponent<AudioSource>().pitch = Random.Range(0.95f, 1.05f);
        if (fxRand == 1) GetComponent<AudioSource>().PlayOneShot(rand1);
    }
}
