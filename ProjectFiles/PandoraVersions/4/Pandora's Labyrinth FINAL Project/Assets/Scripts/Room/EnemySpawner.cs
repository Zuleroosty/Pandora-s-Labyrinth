using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject gameManager, parentRoomObject, normalPrefab, fastPrefab, rangedPrefab, newEnemyPrefab;
    PermissionsHandler permHandler;
    int randomTimer, timer, randNum;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find(">GameManager<");
        permHandler = gameManager.GetComponent<PermissionsHandler>();
        parentRoomObject = transform.parent.parent.parent.parent.gameObject;

        randomTimer = Random.Range(60, 241);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.name.Contains("Boss"))
        {
            randomTimer = 240;
            if (timer < randomTimer) timer++;
            if (timer >= randomTimer)
            {
                randNum = Random.Range(0, 101);
                if (randNum <= 50)
                {
                    Instantiate(rangedPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                }
                else
                {
                    randNum -= 50;
                    if (randNum <= 30)
                    {
                        Instantiate(normalPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    }
                    else
                    {
                        randNum -= 30;
                        if (randNum <= 15)
                        {
                            Instantiate(fastPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                        }
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
                if (timer < randomTimer) timer++;
                if (timer >= randomTimer)
                {
                    randNum = Random.Range(0, 101);
                    if (randNum <= 50) Instantiate(rangedPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity); 
                    else
                    {
                        randNum -= 50;
                        if (randNum <= 30) Instantiate(normalPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity); 
                        else
                        {
                            randNum -= 30;
                            if (randNum <= 15) Instantiate(fastPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                        }
                    }
                    randomTimer = Random.Range(60, 121);
                    timer = 0;
                }
            }
        }
    }
}
