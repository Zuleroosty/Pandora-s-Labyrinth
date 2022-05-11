using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGen : MonoBehaviour
{
    public GameObject newContents, combatContents, emptyContents, pandoraContents, upgradeContents;
    bool hasGeneratedRoom;
    int randNum, spawnDelay, spawnTimer;

    private void Start()
    {
        spawnDelay = Random.Range(20, 60);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer < spawnDelay) spawnTimer++;
        if (spawnTimer >= spawnDelay)
        {
            if (!hasGeneratedRoom && GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Start)
            {
                if (!transform.parent.name.Contains("Spawn"))
                {
                    if (transform.parent.name.Contains("Pandora"))
                    {
                        //spawn pandora
                        newContents = Instantiate(pandoraContents, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                        newContents.transform.parent = transform;
                        hasGeneratedRoom = true;
                    }
                    else
                    {
                        randNum = Random.Range(0, 2);
                        if (randNum == 0)
                        {
                            randNum = Random.Range(0, 101);
                            if (randNum <= 60) // 60% EMPTY ROOM
                            {
                                //spawn empty
                                newContents = Instantiate(emptyContents, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                                newContents.transform.parent = transform;
                                hasGeneratedRoom = true;
                            }
                            else
                            {
                                randNum -= 60;
                                if (randNum <= 35) // 35% COMBAT ROOM
                                {
                                    //spawn combat
                                    newContents = Instantiate(combatContents, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                                    newContents.transform.parent = transform;
                                    hasGeneratedRoom = true;
                                }
                                else
                                {
                                    randNum -= 35;
                                    if (randNum <= 5) // 5% UPGRADE ROOM
                                    {
                                        //spawn empty
                                        if (!GameObject.Find("UpgradeContents (1)(Clone)"))
                                        {
                                            newContents = Instantiate(upgradeContents, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                                            newContents.transform.parent = transform;
                                            hasGeneratedRoom = true;
                                        }
                                    }
                                }
                            }
                        }
                        if (randNum == 1) // FLIP CHANCES
                        {
                            randNum = Random.Range(0, 101);
                            if (randNum <= 60) // 60% COMBAT ROOM
                            {
                                //spawn combat
                                newContents = Instantiate(combatContents, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                                newContents.transform.parent = transform;
                                hasGeneratedRoom = true;
                            }
                            else
                            {
                                randNum -= 60;
                                if (randNum <= 35) // 35% EMPTY ROOM
                                {
                                    //spawn empty
                                    newContents = Instantiate(emptyContents, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                                    newContents.transform.parent = transform;
                                    hasGeneratedRoom = true;
                                }
                                else
                                {
                                    randNum -= 35;
                                    if (randNum <= 5) // 5% UPGRADE ROOM
                                    {
                                        //spawn empty
                                        if (!GameObject.Find("UpgradeContents (1)(Clone)"))
                                        {
                                            newContents = Instantiate(upgradeContents, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                                            newContents.transform.parent = transform;
                                            hasGeneratedRoom = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else hasGeneratedRoom = true;
            }
        }
    }
}
