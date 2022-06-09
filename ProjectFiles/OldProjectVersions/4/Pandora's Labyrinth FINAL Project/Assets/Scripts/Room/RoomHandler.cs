using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    public GameObject gameManager, door1, door2, door3, door4, roomCover;
    Color coverColour;
    PermissionsHandler permHandler;
    public bool isRoomComplete, isRoomLocked, updateDoors, mainGrid;
    public int posX, posY, childID, childMax, doorMax, doorCount, enemyMax, enemyCount, maxSpawners, destroyedSpawners;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find(">GameManager<");
        permHandler = gameManager.GetComponent<PermissionsHandler>();
        if (roomCover != null) coverColour = roomCover.GetComponent<SpriteRenderer>().color;

        if (transform.parent == null) transform.parent = GameObject.Find("----Rooms----").transform;

        maxSpawners = 4;

        if (name.Contains("Spawn")) gameManager.GetComponent<GameManager>().currentRoomParent = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            enemyMax = (maxSpawners - destroyedSpawners) * 3;
            if (destroyedSpawners >= maxSpawners) //----WHEN ENEMYS HAVE BEEN DEFEATED
            {
                destroyedSpawners = 0;
                isRoomComplete = true;
                //REVEAL PUZZLE??
            }

            if (!updateDoors)
            {
                UpdateDoorObjects();
            }
            if (GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds))
            {
                gameManager.GetComponent<GameManager>().currentRoomParent = this.gameObject;
                if (roomCover != null && coverColour.a > 0.15f) coverColour.a -= 0.1f;
                if (!GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().hasPandorasBox)
                {
                    if (isRoomComplete)
                    {
                        if (this.name.Contains("Combat"))
                        {
                            permHandler.canSpawn = false;
                            GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().inCombat = false;
                        }
                        isRoomLocked = false;
                    }
                    else
                    {
                        if (isRoomLocked && this.name.Contains("Combat"))
                        {
                            permHandler.canSpawn = true;
                            GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().inCombat = true;
                        }
                        else isRoomLocked = true;
                    }
                }
                else
                {
                    isRoomLocked = false;
                    permHandler.canSpawn = true;
                }
            }
            else
            {
                if (roomCover != null && coverColour.a < 1f) coverColour.a += 0.1f;
            }
            if (roomCover != null) roomCover.GetComponent<SpriteRenderer>().color = coverColour;
        }
        else if (!gameManager.GetComponent<GameManager>().fillGrid)
        {
            if (GetComponent<SpriteRenderer>().bounds.Intersects(GameObject.Find("TestSprite").GetComponent<SpriteRenderer>().bounds))
            {
                GameObject.Find("RoomSpawner").GetComponent<ActiveRoomSpawner>().canSpawnHere = false;
            }
        }
    }
    void UpdateDoorObjects()
    {
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
