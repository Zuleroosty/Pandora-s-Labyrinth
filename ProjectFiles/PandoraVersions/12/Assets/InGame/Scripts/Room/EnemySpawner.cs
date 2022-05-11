using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject gameManager, parentRoomObject, normalPrefab, fastPrefab, rangedPrefab, newEnemyPrefab, xpSpawnObject;
    PermissionsHandler permHandler;
    int randomTimer, timer, randNum, destroyTimer;
    float maxSeconds;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find(">GameManager<");
        permHandler = gameManager.GetComponent<PermissionsHandler>();
        if (!transform.parent.name.Contains("Boss")) parentRoomObject = transform.parent.parent.parent.parent.gameObject;

        randomTimer = Random.Range(60, 241);

        maxSeconds = (10 * (1 + (gameManager.GetComponent<LevelHandler>().averagePlayerLevel * 0.2f)));

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
                    if (randNum <= 50) Instantiate(normalPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    else
                    {
                        randNum -= 50;
                        if (randNum <= 30) Instantiate(fastPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                        else
                        {
                            randNum -= 30;
                            if (randNum <= 15) Instantiate(rangedPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                        }
                    }
                    randomTimer = Random.Range(60, 121);
                    timer = 0;
                }
            }
            else if (permHandler.canSpawn && parentRoomObject.GetComponent<RoomHandler>().enemyCount < parentRoomObject.GetComponent<RoomHandler>().enemyMax)
            {
                if (parentRoomObject == gameManager.GetComponent<GameManager>().currentRoomParent)
                {
                    // DESTROY TIMER
                    if (destroyTimer >= maxSeconds * 60)
                    {
                        if (transform.localScale.x > 0) transform.localScale -= new Vector3(0.1f, 0.1f, 0);
                        else
                        {
                            parentRoomObject.GetComponent<RoomHandler>().destroyedSpawners++;
                            Instantiate(xpSpawnObject, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
                            Destroy(gameObject);
                        }
                    }
                    else // SPAWN ENEMY EVERY X FRAMES IF MAX ENEMIES HAVE NOT BEEN SPAWNED
                    {
                        destroyTimer++;
                        if (transform.localScale.x < 5) transform.localScale += new Vector3(0.25f, 0.25f, 0);
                        if (timer < randomTimer) timer++;
                        if (timer >= randomTimer)
                        {
                            randNum = Random.Range(0, 101);
                            if (randNum <= 50) Instantiate(normalPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                            else
                            {
                                randNum -= 50;
                                if (randNum <= 30) Instantiate(fastPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                                else
                                {
                                    randNum -= 30;
                                    if (randNum <= 15) Instantiate(rangedPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                                }
                            }
                            randomTimer = Random.Range(60, 121);
                            timer = 0;
                        }
                    }
                }
            }
        }
    }
}
