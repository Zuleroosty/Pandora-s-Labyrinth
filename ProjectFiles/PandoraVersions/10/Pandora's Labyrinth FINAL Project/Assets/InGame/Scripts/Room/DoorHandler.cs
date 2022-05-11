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
        isLocked = transform.parent.parent.GetComponent<RoomHandler>().isRoomLocked;
        if (isLocked)
        {
            GetComponent<CollisionManager>().disableCollision = false;
            if (thisColour.a < 1) thisColour.a += 0.25f;
        }
        else
        {
            GetComponent<CollisionManager>().disableCollision = true;
             if (thisColour.a > 0) thisColour.a -= 0.05f;
        }

        GetComponent<SpriteRenderer>().color = thisColour;
    }
}
