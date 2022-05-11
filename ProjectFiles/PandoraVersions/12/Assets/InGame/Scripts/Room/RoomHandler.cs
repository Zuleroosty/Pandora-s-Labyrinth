using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    public GameObject gameManager, roomCollider, door1, door2, door3, door4, roomCover, player;
    Color coverColour;
    PermissionsHandler permHandler;
    public bool isRoomComplete, isRoomLocked, updateDoors, mainGrid, isCombatRoom;
    public int posX, posY, childID, childMax, doorMax, doorCount, enemyCount, maxSpawners, destroyedSpawners;
    public float enemyMax;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find(">GameManager<");
        permHandler = gameManager.GetComponent<PermissionsHandler>();
        transform.parent = GameObject.Find("----Rooms----").transform;
        player = GameObject.Find("----PlayerObjectParent----");

        maxSpawners = 4;

        coverColour = roomCover.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        // SHOW/HIDE ROOM
        if (gameManager.GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().bounds.Intersects(roomCollider.GetComponent<SpriteRenderer>().bounds))
            {
                gameManager.GetComponent<GameManager>().currentRoomParent = this.gameObject;
                if (!player.GetComponent<PlayerController>().hasPandorasBox && isCombatRoom)
                {
                    if (isRoomComplete) //---UNLOCK DOORS ON COMPLETE AND TAKE PLAYER OUT OF COMBAT MODE
                    {
                        permHandler.canSpawn = false;
                        player.GetComponent<PlayerController>().inCombat = false;
                        isRoomLocked = false;
                    }
                    else
                    {
                        if (isRoomLocked) //---LOCK DOORS AND PUT PLAYER INTO COMBAT MODE
                        {
                            permHandler.canSpawn = true;
                            GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().inCombat = true;
                        }
                        else isRoomLocked = true;
                    }
                }
                else
                {
                    isRoomLocked = false; //---CANNOT LOCK IF PLAYER HAS PANDORA'S BOX
                }
            }
            // UPDATE ROOM COVER TRANSPARENCY
            if (GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds))
            {
                if (coverColour.a > 0.15f) coverColour.a -= 0.1f;
            }
            else //---PLAYER IS NOT IN ROOM
            {
                if (coverColour.a < 1f) coverColour.a += 0.1f;
            }
            roomCover.GetComponent<SpriteRenderer>().color = coverColour;

            // IF ROOM IS COMBAT
            if (isCombatRoom)
            {
                enemyMax = 4 * (1 + (gameManager.GetComponent<LevelHandler>().averagePlayerLevel * 0.2f));
                if (destroyedSpawners >= maxSpawners) //----WHEN ENEMYS HAVE BEEN DEFEATED
                {
                    destroyedSpawners = 0;
                    isRoomComplete = true;
                    //REVEAL PUZZLE??
                }
                if (doorCount < doorMax)
                {
                    UpdateDoorObjects();
                }
            }
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
        GameObject objectCheck;
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
