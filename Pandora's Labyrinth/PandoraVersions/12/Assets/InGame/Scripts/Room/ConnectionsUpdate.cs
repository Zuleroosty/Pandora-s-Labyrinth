using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionsUpdate : MonoBehaviour
{
    public bool conLeft, conRight, conUp, conDown;
    public GameObject testObject, testRoom, roomParent,lRoomObject, rRoomObject, uRoomObject, dRoomObject;
    public int currentStage, childID, childMax, updateTimer;
    public bool hasUpdated;

    // Start is called before the first frame update
    void Start()
    {
        testObject = transform.GetChild(0).gameObject;
        roomParent = GameObject.Find("----Rooms----");

        childID = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasUpdated && GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState != GameManager.state.GenLevel)
        {
            if (currentStage < 4)
            {
                childMax = roomParent.transform.childCount;
                if (updateTimer < 3) updateTimer++;
                if (updateTimer >= 3)
                {
                    updateTimer = 0;
                    childID++;
                    CollisionTest();
                    switch (currentStage)
                    {
                        case 0:
                            testObject.transform.position = transform.position + new Vector3(0, -21, 0); // DOWN
                            break;
                        case 1:
                            testObject.transform.position = transform.position + new Vector3(35, 0, 0); // RIGHT
                            break;
                        case 2:
                            testObject.transform.position = transform.position + new Vector3(0, 21, 0); // UP
                            break;
                        case 3:
                            testObject.transform.position = transform.position + new Vector3(-35, 0, 0); // LEFT
                            break;
                    }
                }
            }
            else // AFTER CHECK IS COMPLETE - UPDATE ROOM & CONNECTIONS
            {
                if (conLeft && transform.parent.name.Contains("L"))
                {
                    if (!lRoomObject.name.Contains("Spawn")) lRoomObject.GetComponent<UpdateRoomConnections>().EnsureRightExit();
                    transform.parent.GetComponent<UpdateRoomConnections>().EnsureLeftExit();
                }
                else transform.parent.GetComponent<UpdateRoomConnections>().BlockLeftExit();

                if (conRight && transform.parent.name.Contains("R"))
                {
                    rRoomObject.GetComponent<UpdateRoomConnections>().EnsureLeftExit();
                    transform.parent.GetComponent<UpdateRoomConnections>().EnsureRightExit();
                }
                else transform.parent.GetComponent<UpdateRoomConnections>().BlockRightExit();

                if (conUp && transform.parent.name.Contains("U"))
                {
                    uRoomObject.GetComponent<UpdateRoomConnections>().EnsureBottomExit();
                    transform.parent.GetComponent<UpdateRoomConnections>().EnsureTopExit();
                }
                else transform.parent.GetComponent<UpdateRoomConnections>().BlockTopExit();

                if (conDown && transform.parent.name.Contains("D"))
                {
                    dRoomObject.GetComponent<UpdateRoomConnections>().EnsureTopExit();
                    transform.parent.GetComponent<UpdateRoomConnections>().EnsureBottomExit();
                }
                else transform.parent.GetComponent<UpdateRoomConnections>().BlockBottomExit();

                Destroy(gameObject, 1);
            }
        }
    }
    void CollisionTest()
    {
        if (childID < childMax && childMax == roomParent.transform.childCount)
        {
            testRoom = roomParent.transform.GetChild(childID).gameObject;
            if (testObject.GetComponent<SpriteRenderer>().bounds.Intersects(testRoom.GetComponent<SpriteRenderer>().bounds))
            {
                switch (currentStage)
                {
                    case 0:
                        dRoomObject = testRoom;
                        if (dRoomObject.name.Contains("Empty")) conDown = false; // IGNORE EMPTY ROOMS
                        else
                        {
                            if (dRoomObject.name.Contains("U")) conDown = true;
                            else conDown = false;
                        }
                        currentStage++;
                        childID = -1;
                        break;
                    case 1:
                        rRoomObject = testRoom;
                        if (rRoomObject.name.Contains("Empty")) conRight = false; // IGNORE EMPTY ROOMS
                        else
                        {
                            if (rRoomObject.name.Contains("L")) conRight = true;
                            else conRight = false;
                        }
                        currentStage++;
                        childID = -1;
                        break;
                    case 2:
                        uRoomObject = testRoom;
                        if (uRoomObject.name.Contains("Spawn")) conUp = false; // REMOVE UP/DOWN CONNECTION TO SPAWN ROOM
                        else if (uRoomObject.name.Contains("Empty")) conUp = false; // IGNORE EMPTY ROOMS
                        else
                        {
                            if (uRoomObject.name.Contains("D")) conUp = true;
                            else conUp = false;
                        }
                        currentStage++;
                        childID = -1;
                        break;
                    case 3:
                        lRoomObject = testRoom;
                        if (lRoomObject.name.Contains("Empty")) conLeft = false; // IGNORE EMPTY ROOMS
                        else
                        {
                            if (lRoomObject.name.Contains("R")) conLeft = true;
                            else conLeft = false;
                        }
                        currentStage++;
                        childID = -1;
                        break;
                }
            }
        }
        else
        {
            currentStage++;
            childID = -1;
        }
    }
}
