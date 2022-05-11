using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGen : MonoBehaviour
{
    // ROOM CONTENTS
    public GameObject combat1, combat2, combat3, combat4;
    public GameObject empty1, empty2, empty3, empty4;
    public GameObject pandora1, pandora2, pandora3, pandora4;
    public GameObject upgrade1, upgrade2, upgrade3, upgrade4;
    public GameObject newContents;

    bool hasGeneratedRoom;
    int randNum, randRoomNum, spawnDelay, spawnTimer;

    private void Start()
    {
        spawnDelay = Random.Range(20, 60);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame) // RUN CODE WHEN GAME STARTS (IN GAME)
        {
            if (spawnTimer < spawnDelay) spawnTimer++; // INITIAL DELAY TO INCREASE RANDOMNESS
            if (spawnTimer >= spawnDelay)
            {
                if (newContents == null) // WILL RUN CODE UNTIL A ROOM IS GENERATED
                {
                    if (!transform.parent.name.Contains("Spawn"))
                    {
                        if (transform.parent.name.Contains("Objective"))
                        {
                            //spawn pandora
                            SpawnPandoraRoom();
                        }
                        else
                        {
                            randNum = Random.Range(0, 2); // TILT CHANCES OF EMPTY/COMBAT ROOMS

                            if (randNum == 0) 
                            {
                                randNum = Random.Range(0, 101);
                                if (randNum <= 55) // 55% EMPTY ROOM
                                {
                                    SpawnEmptyRoom();
                                }
                                else
                                {
                                    randNum -= 55;
                                    if (randNum <= 40) // 40% COMBAT ROOM
                                    {
                                        SpawnCombatRoom();
                                    }
                                    else
                                    {
                                        randNum -= 40;
                                        if (randNum <= 5) // 5% UPGRADE ROOM
                                        {
                                            if (!GameObject.Find("UpgradeContents (1)(Clone)") && transform.position.y < ((3 * 21) * -1)) // SPAWN ONLY 1 UPGRADE ROOM PER LEVEL
                                            {
                                                SpawnUpgradeRoom();
                                            }
                                        }
                                    }
                                }
                            }
                            if (randNum == 1)
                            {
                                randNum = Random.Range(0, 101);
                                if (randNum <= 55) // 55% COMBAT ROOM
                                {
                                    SpawnCombatRoom();
                                }
                                else
                                {
                                    randNum -= 55;
                                    if (randNum <= 40) // 40% EMPTY ROOM
                                    {
                                        SpawnEmptyRoom();
                                    }
                                    else
                                    {
                                        randNum -= 40;
                                        if (randNum <= 5) // 5% UPGRADE ROOM
                                        {
                                            if (!GameObject.Find("UpgradeContents (1)(Clone)") && transform.position.y < ((3 * 21) * -1)) // SPAWN ONLY 1 UPGRADE ROOM PER LEVEL
                                            {
                                                SpawnUpgradeRoom();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    private void SpawnCombatRoom()
    {
        randRoomNum = Random.Range(0, 4); // MAX RANGE = MAX ROOMS PLUS 1
        switch (randRoomNum)
        {
            case 0:
                newContents = Instantiate(combat1, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                break;
            case 1:
                newContents = Instantiate(combat2, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                break;
            case 2:
                newContents = Instantiate(combat3, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                break;
            case 3:
                newContents = Instantiate(combat4, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                break;
        }
        newContents.transform.parent = transform;
        hasGeneratedRoom = true;
    }
    private void SpawnEmptyRoom() // CHOOSE AND SPAWN RANDOM VARIANT OF ROOM
    {
        randRoomNum = Random.Range(0, 4); // MAX RANGE = MAX ROOMS PLUS 1
        switch (randRoomNum)
        {
            case 0:
                newContents = Instantiate(empty1, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                break;
            case 1:
                newContents = Instantiate(empty2, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                break;
            case 2:
                newContents = Instantiate(empty3, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                break;
            case 3:
                newContents = Instantiate(empty4, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                break;
        }
        newContents.transform.parent = transform;
        hasGeneratedRoom = true;
    }
    private void SpawnPandoraRoom() // CHOOSE AND SPAWN RANDOM VARIANT OF ROOM
    {
        randRoomNum = Random.Range(0, 4); // MAX RANGE = MAX ROOMS PLUS 1
        switch (randRoomNum)
        {
            case 0:
                newContents = Instantiate(pandora1, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                break;
            case 1:
                newContents = Instantiate(pandora2, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                break;
            case 2:
                newContents = Instantiate(pandora3, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                break;
            case 3:
                newContents = Instantiate(pandora4, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                break;
        }
        newContents.transform.parent = transform;
        hasGeneratedRoom = true;
    }
    private void SpawnUpgradeRoom() // CHOOSE AND SPAWN RANDOM VARIANT OF ROOM
    {
        randRoomNum = Random.Range(0, 4); // MAX RANGE = MAX ROOMS PLUS 1
        switch (randRoomNum)
        {
            case 0:
                newContents = Instantiate(upgrade1, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                break;
            case 1:
                newContents = Instantiate(upgrade2, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                break;
            case 2:
                newContents = Instantiate(upgrade3, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                break;
            case 3:
                newContents = Instantiate(upgrade4, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                break;
        }
        newContents.transform.parent = transform;
        hasGeneratedRoom = true;
    }
}
