using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGen : MonoBehaviour
{
    // ROOM CONTENTS
    public GameObject combat1, combat2, combat3, combat4, combat5, combat6;
    public GameObject empty1, empty2, empty3, empty4, empty5, empty6;
    public GameObject pandora1, pandora2, pandora3, pandora4;
    public GameObject upgrade1, upgrade2, upgrade3, upgrade4;
    public GameObject newContents;
    public int maxY;

    bool hasGeneratedRoom;
    int randNum, randRoomNum, spawnDelay, spawnTimer;

    private void Start()
    {
        spawnDelay = Random.Range(90, 120);
        maxY = GameObject.Find(">GameManager<").GetComponent<GameManager>().maxY;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame) // RUN CODE WHEN GAME STARTS (IN GAME)
        {
            if (spawnTimer < spawnDelay) spawnTimer++; // INITIAL DELAY TO INCREASE RANDOMNESS
            if (spawnTimer >= spawnDelay)
            {
                if (!hasGeneratedRoom) // WILL RUN CODE UNTIL A ROOM IS GENERATED
                {
                    if (!transform.parent.name.Contains("Spawn"))
                    {
                        if (transform.parent.name.Contains("Objective"))
                        {
                            //spawn pandora
                            SpawnPandoraRoom();
                        }
                        else RandomSpawn();
                    }
                }
            }
        }
    }
    private void RandomSpawn()
    {
        randNum = Random.Range(0, 101);
        if (randNum <= 50) // 50% COMBAT ROOM
        {
            SpawnCombatRoom();
        }
        else
        {
            randNum -= 50;
            if (randNum <= 35) // 35% EMPTY ROOM
            {
                SpawnEmptyRoom();
            }
            else
            {
                randNum -= 35;
                if (randNum <= 15) // 15% UPGRADE ROOM
                {
                    if (GameObject.Find(">GameManager<").GetComponent<GameManager>().upgradeRooms <= 0 && transform.position.y < -20) // SPAWN ONLY 1 UPGRADE ROOM PER LEVEL
                    {
                        GameObject.Find(">GameManager<").GetComponent<GameManager>().upgradeRooms++;
                        SpawnUpgradeRoom();
                    }
                    else RandomSpawn();
                }
            }
        }
    }
    private void SpawnCombatRoom()
    {
        hasGeneratedRoom = true;
        randRoomNum = Random.Range(0, 6); // MAX RANGE = MAX ROOMS INCLUDING 0
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
            case 4:
                newContents = Instantiate(combat5, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                break;
            case 5:
                newContents = Instantiate(combat6, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                break;
        }
        newContents.transform.parent = transform;
    }
    private void SpawnEmptyRoom() // CHOOSE AND SPAWN RANDOM VARIANT OF ROOM
    {
        hasGeneratedRoom = true;
        randRoomNum = Random.Range(0, 6); // MAX RANGE = MAX ROOMS INCLUDING 0
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
            case 4:
                newContents = Instantiate(empty5, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                break;
            case 5:
                newContents = Instantiate(empty6, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                break;
        }
        newContents.transform.parent = transform;
    }
    private void SpawnPandoraRoom() // CHOOSE AND SPAWN RANDOM VARIANT OF ROOM
    {
        hasGeneratedRoom = true;
        randRoomNum = Random.Range(0, 4); // MAX RANGE = MAX ROOMS INCLUDING 0
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
    }
    private void SpawnUpgradeRoom() // CHOOSE AND SPAWN RANDOM VARIANT OF ROOM
    {
        hasGeneratedRoom = true;
        randRoomNum = Random.Range(0, 4); // MAX RANGE = MAX ROOMS INCLUDING 0
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
    }
}
