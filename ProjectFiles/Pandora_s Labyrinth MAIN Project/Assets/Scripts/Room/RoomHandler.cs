using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    public GameObject gameManager, roomCollider, door1, door2, door3, door4, roomCover, player;
    Color coverColour;
    PermissionsHandler permHandler;
    public bool isRoomComplete, isRoomLocked, updateDoors, isCombatRoom, hasEntered, hasDiscovered;
    public int childID, childMax, doorMax, doorCount, enemyCount, maxSpawners, frameCounter, enemyTypes;
    public float enemyMax, maxSeconds, currentSeconds;
    GameObject objectCheck;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find(">GameManager<");
        permHandler = gameManager.GetComponent<PermissionsHandler>();
        transform.parent = GameObject.Find("----Rooms----").transform;
        player = GameObject.Find("----PlayerObjectParent----");
        coverColour = roomCover.GetComponent<SpriteRenderer>().color;

        maxSpawners = 4;
        maxSeconds = (10 * (1 + (gameManager.GetComponent<LevelHandler>().averagePlayerLevel * 0.2f))) * Random.Range(1.0f, 1.65f);

        // MANAGE FIRST 4 ROOMS ON LEVEL 1 TO INTRODUCE ENEMIES OVERTIME
        if (gameManager.GetComponent<LevelHandler>().currentPlayLevel == 1)
        {
            if (gameManager.GetComponent<GameManager>().roomLevel == 0) enemyTypes = 1;
            else if (gameManager.GetComponent<GameManager>().roomLevel == 1) enemyTypes = 2;
            else if (gameManager.GetComponent<GameManager>().roomLevel == 2) enemyTypes = 4;
            else if (gameManager.GetComponent<GameManager>().roomLevel == 3) enemyTypes = 3;
            gameManager.GetComponent<GameManager>().roomLevel++;
        }
        if (enemyTypes == 0) enemyTypes = Random.Range(1, 6);
        // 1 = only goblins / 2 = goblins & spiders / 3 = spiders & scorpions / 4 = scorpions & goblins / 5 = all
    }

    // Update is called once per frame
    void Update()
    {
        // SHOW/HIDE ROOM
        if (gameManager.GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (doorCount < doorMax && childMax < transform.childCount) UpdateDoorObjects();

            // SET CURRENT ROOM ON ENTRY
            if (GameObject.Find("PCollision").GetComponent<SpriteRenderer>().bounds.Intersects(roomCollider.GetComponent<SpriteRenderer>().bounds) && gameManager.GetComponent<GameManager>().currentRoomParent != this.gameObject)
            {
                gameManager.GetComponent<GameManager>().currentRoomParent = this.gameObject;
                //------------------------- STATS UPDATE -------------------------------------------

                if (!hasEntered)
                {
                    GameObject.Find(">GameManager<").GetComponent<StatHandler>().roomsEntered++;
                    hasEntered = true;
                }
                if (!hasDiscovered)
                {
                    GameObject.Find(">GameManager<").GetComponent<StatHandler>().roomsDiscovered++;
                    hasDiscovered = true;
                }

                //----------------------------------------------------------------------------------
            }
            else if (gameManager.GetComponent<GameManager>().currentRoomParent == this.gameObject)
            {
                if (coverColour.a > 0.15f) coverColour.a -= 0.1f; // HIDE ROOM COVER
                if (!player.GetComponent<PlayerController>().hasPandorasBox && isCombatRoom)
                {
                    if (isRoomComplete) //---UNLOCK DOORS ON COMPLETE AND TAKE PLAYER OUT OF COMBAT MODE
                    {
                        permHandler.canSpawn = false;
                        if (enemyCount <= 0)
                        {
                            isRoomLocked = false;
                            player.GetComponent<PlayerController>().inCombat = false;
                        }
                    }
                    else
                    {
                        enemyMax = 5 * (1 + (gameManager.GetComponent<LevelHandler>().averagePlayerLevel * 0.2f));
                        if (isRoomLocked) //---LOCK DOORS AND PUT PLAYER INTO COMBAT MODE
                        {
                            permHandler.canSpawn = true;
                            GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().inCombat = true;

                            if (currentSeconds >= maxSeconds) //----WHEN ENEMIES HAVE BEEN DEFEATED
                            {
                                isRoomComplete = true;
                            }
                            else if (frameCounter < 60) frameCounter++;
                            if (frameCounter >= 60)
                            {
                                frameCounter = 0;
                                currentSeconds++;
                            }
                        }
                        else isRoomLocked = true;
                    }
                }
                else
                {
                    isRoomLocked = false; //---CANNOT LOCK IF PLAYER HAS PANDORA'S BOX
                }
            }
            else
            {
                hasEntered = false; // RESET TO UPDATE STATS ON RE-ENTRY
                if (coverColour.a < 1f) coverColour.a += 0.1f; // SHOW ROOM COVER ON EXIT
            }
            roomCover.GetComponent<SpriteRenderer>().color = coverColour;

        }
        // STOP DUPLICATE ROOMS SPAWNING
        if (gameManager.GetComponent<GameManager>().gameState == GameManager.state.GenLevel)
        {
            if (GetComponent<SpriteRenderer>().bounds.Intersects(GameObject.Find("TestSprite").GetComponent<SpriteRenderer>().bounds))
            {
                GameObject.Find("RoomSpawner").GetComponent<ActiveRoomSpawner>().canSpawnHere = false;
            }
        }
    }
    void UpdateDoorObjects()
    {
        // ADD ALL ROOM DOORS TO SCRIPT
        if (childID < childMax && childMax == transform.childCount)
        {
            objectCheck = this.transform.GetChild(childID).gameObject;

            if (objectCheck != door1 && objectCheck.name.Contains("Door") && door1 == null)
            {
                door1 = objectCheck;
                doorCount = 1;
            }
            else if (objectCheck != door2 && objectCheck.name.Contains("Door") && door2 == null)
            {
                door2 = objectCheck;
                doorCount = 2;
            }
            else if (objectCheck != door3 && objectCheck.name.Contains("Door") && door3 == null)
            {
                door3 = objectCheck;
                doorCount = 3;
            }
            else if (objectCheck != door4 && objectCheck.name.Contains("Door") && door4 == null)
            {
                door4 = objectCheck;
                doorCount = 4;
            }
        }
        else
        {
            updateDoors = true;
            childMax = transform.childCount;
        }
        childID++;
    }
}
