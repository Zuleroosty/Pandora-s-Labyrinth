using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject gameManager, parentRoomObject, normalPrefab, fastPrefab, rangedPrefab, newEnemyPrefab, xpSpawnObject, bossEnemySpawned;
    PermissionsHandler permHandler;
    Animator thisAnimator;
    int randomTimer, timer, spawnTimer, randNum, spawnType;
    bool readyToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find(">GameManager<");
        permHandler = gameManager.GetComponent<PermissionsHandler>();
        thisAnimator = GetComponent<Animator>();
        if (!transform.parent.name.Contains("Boss")) parentRoomObject = transform.parent.parent.parent.parent.gameObject;

        randomTimer = Random.Range(45,61);

        transform.localScale = new Vector3(0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (transform.parent.name.Contains("Boss"))
            {
                randomTimer = 240;
                if (timer < randomTimer) timer++;
                if (timer >= randomTimer)
                {
                    randNum = Random.Range(0, 101);
                    if (randNum <= 50) bossEnemySpawned = Instantiate(normalPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    else
                    {
                        randNum -= 50;
                        if (randNum <= 30) bossEnemySpawned = Instantiate(fastPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                        else
                        {
                            randNum -= 30;
                            if (randNum <= 15) bossEnemySpawned = Instantiate(rangedPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                        }
                    }
                    if (bossEnemySpawned != null) bossEnemySpawned.gameObject.GetComponent<LootSpawner>().isDisabled = true;
                    randomTimer = Random.Range(60, 121);
                    timer = 0;
                }
            }
            else if (parentRoomObject == gameManager.GetComponent<GameManager>().currentRoomParent)
            {
                thisAnimator.SetTrigger("isActive");
                if (gameManager.GetComponent<GameManager>().gameState == GameManager.state.InGame)
                {
                    if (parentRoomObject.GetComponent<RoomHandler>().enemyCount < parentRoomObject.GetComponent<RoomHandler>().enemyMax)
                    {
                        // DESTROY TIMER
                        if (parentRoomObject.GetComponent<RoomHandler>().isRoomComplete)
                        {
                            thisAnimator.ResetTrigger("isIdle");
                            thisAnimator.ResetTrigger("isSpawning");
                            thisAnimator.SetTrigger("isDestroy");
                            if (transform.localScale.x > 0.2f) transform.localScale -= new Vector3(0.1f, 0.1f, 0);
                            else
                            {
                                Instantiate(xpSpawnObject, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
                                Destroy(gameObject);
                            }
                        }
                        else if (permHandler.canSpawn) // SPAWN ENEMY EVERY X FRAMES IF MAX ENEMIES HAVE NOT BEEN SPAWNED
                        {
                            if (!readyToSpawn)
                            {
                                if (transform.localScale.x < 5) transform.localScale += new Vector3(0.25f, 0.25f, 0);
                                if (timer < randomTimer) timer++;
                                if (timer >= randomTimer)
                                {
                                    spawnType = 0;
                                    randNum = Random.Range(0, 101);
                                    switch (parentRoomObject.GetComponent<RoomHandler>().enemyTypes)
                                    {
                                        case 1: // GOBLIN ONLY
                                            if (randNum > 50) spawnType = 1;
                                            break;
                                        case 2: // GOBLIN & SPIDER
                                            if (randNum <= 50) spawnType = 1;
                                            else
                                            {
                                                randNum -= 50;
                                                if (randNum <= 30) spawnType = 2;
                                            }
                                            break;
                                        case 3: // SPIDER & SCORPION
                                            if (randNum <= 30) spawnType = 2;
                                            else
                                            {
                                                randNum -= 30;
                                                if (randNum <= 15) spawnType = 3;
                                            }
                                            break;
                                        case 4: // SCORPION & GOBLIN
                                            if (randNum <= 50) spawnType = 1;
                                            else
                                            {
                                                randNum -= 50;
                                                if (randNum <= 15) spawnType = 3;
                                            }
                                            break;
                                        case 5: // ALL ENEMY TYPES
                                            if (randNum <= 50) spawnType = 1;
                                            else
                                            {
                                                randNum -= 50;
                                                if (randNum <= 30) spawnType = 2;
                                                else
                                                {
                                                    randNum -= 30;
                                                    if (randNum <= 15) spawnType = 3;
                                                }
                                            }
                                            break;
                                    }
                                    spawnTimer = 0;
                                    readyToSpawn = true;
                                    randomTimer = Random.Range(60, 121);
                                    timer = 0;
                                }
                            }
                            else
                            {
                                if (spawnType != 0) SpawnEnemy(spawnType);
                                else readyToSpawn = false;
                            }
                        }
                    }
                }
                else if (!parentRoomObject.GetComponent<RoomHandler>().isRoomComplete)
                {
                    thisAnimator.SetTrigger("isIdle");
                    thisAnimator.ResetTrigger("isSpawning");
                }
            }
        }
    }
    void SpawnEnemy(int type)
    {
        if (spawnTimer < 55) spawnTimer++;
        {
            thisAnimator.SetTrigger("isSpawning");
            thisAnimator.ResetTrigger("isIdle");
        }
        if (spawnTimer >= 55)
        {
            
            switch (type)
            {
                case 1: // GOBLINS
                    Instantiate(normalPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    break;
                case 2: // SPIDERS
                    Instantiate(fastPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    break;
                case 3: // SCORPIONS
                    Instantiate(rangedPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    break;
            }

            readyToSpawn = false;
            thisAnimator.SetTrigger("isIdle");
            thisAnimator.ResetTrigger("isSpawning");
        }
    }
}
