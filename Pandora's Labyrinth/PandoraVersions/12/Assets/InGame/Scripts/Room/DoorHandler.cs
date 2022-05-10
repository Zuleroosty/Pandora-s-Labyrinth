using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public bool isLocked;
    Color thisColour;

    // Start is called before the first frame update
    void Start()
    {
        thisColour = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (!transform.parent.parent.name.Contains("Spawn")) isLocked = transform.parent.parent.GetComponent<RoomHandler>().isRoomLocked;
            else if (this.gameObject != transform.parent.parent.GetComponent<SpawnRoomHandler>().exitDoor)
            {
                isLocked = false;
            }
            else if (GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().hasPandorasBox) isLocked = false;
            else isLocked = true;

            if (isLocked)
            {
                transform.GetChild(0).GetComponent<CollisionManager>().disableCollision = false;
                if (thisColour.a < 1) thisColour.a += 0.25f;
            }
            else
            {
                transform.GetChild(0).GetComponent<CollisionManager>().disableCollision = true;
                if (thisColour.a > 0) thisColour.a -= 0.05f;
            }

            GetComponent<SpriteRenderer>().color = thisColour;
        }
    }
}
