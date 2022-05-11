using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoomHandler : MonoBehaviour
{
    public GameObject gameManager, door1, door2, door3, exitDoor;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find(">GameManager<");
        transform.parent = GameObject.Find("----Rooms----").transform;

        gameManager.GetComponent<GameManager>().currentRoomParent = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // SHOW/HIDE ROOM
        if (gameManager.GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds))
            {
                gameManager.GetComponent<GameManager>().currentRoomParent = this.gameObject;
            }
            if (GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().hasPandorasBox) exitDoor.GetComponent<DoorHandler>().isLocked = false;
            else exitDoor.GetComponent<DoorHandler>().isLocked = true;
            door1.GetComponent<DoorHandler>().isLocked = false;
            door2.GetComponent<DoorHandler>().isLocked = false;
            door3.GetComponent<DoorHandler>().isLocked = false;
        }
        if (gameManager.GetComponent<GameManager>().gameState == GameManager.state.GenLevel)
        {
            if (GetComponent<SpriteRenderer>().bounds.Intersects(GameObject.Find("TestSprite").GetComponent<SpriteRenderer>().bounds))
            {
                GameObject.Find("RoomSpawner").GetComponent<ActiveRoomSpawner>().canSpawnHere = false;
            }
        }
    }
}
