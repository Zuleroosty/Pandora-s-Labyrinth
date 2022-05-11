using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    GameObject gameManager;
    PermissionsHandler permHandler;
    bool isRoomComplete, isRoomLocked;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find(">GameManager<");
        permHandler = gameManager.GetComponent<PermissionsHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds))
        {
            if (isRoomComplete)
            {
                permHandler.canSpawn = false;
            }
            else
            {
                if (isRoomLocked)
                {

                }
            }
        }
    }
}
