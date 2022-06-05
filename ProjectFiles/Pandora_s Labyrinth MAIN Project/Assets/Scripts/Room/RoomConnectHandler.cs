using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomConnectHandler : MonoBehaviour
{
    public GameObject north, south, east, west, roomParent, testRoom, thisRoom;
    public bool northTest, southTest, eastTest, westTest, testsComplete;
    public int childID, childMax, timer;

    private void Start()
    {
        roomParent = GameObject.Find("----Rooms----");
        thisRoom = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 4) timer++;
        if (timer >= 4)
        {
            timer = 0;
            if (!testsComplete && GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
            {
                if (northTest && southTest && eastTest && westTest)
                {
                    testsComplete = true;
                }
                else if (childID < childMax && childMax == roomParent.transform.childCount)
                {
                    testRoom = roomParent.transform.GetChild(childID).gameObject;
                    if (!northTest)
                    {
                        if (north.GetComponent<SpriteRenderer>().bounds.Intersects(testRoom.GetComponent<SpriteRenderer>().bounds)) // NORTH
                        {
                            if (testRoom.name.Contains("Empty") || testRoom.name.Contains("Spawn"))
                            {
                                thisRoom.GetComponent<UpdateRoomConnections>().northConnection = false;
                                northTest = true;
                            }
                            else if (thisRoom.name.Contains("U") && testRoom.name.Contains("D"))
                            {
                                testRoom.GetComponent<UpdateRoomConnections>().connectObject.GetComponent<RoomConnectHandler>().northTest = true;
                                thisRoom.GetComponent<UpdateRoomConnections>().northConnection = true;
                                testRoom.GetComponent<UpdateRoomConnections>().southConnection = true;
                                northTest = true;
                            }
                        }
                        else thisRoom.GetComponent<UpdateRoomConnections>().northConnection = false;
                    }
                    if (!southTest)
                    {
                        if (south.GetComponent<SpriteRenderer>().bounds.Intersects(testRoom.GetComponent<SpriteRenderer>().bounds)) // SOUTH
                        {
                            if (testRoom.name.Contains("Empty"))
                            {
                                thisRoom.GetComponent<UpdateRoomConnections>().southConnection = false;
                                southTest = true;
                            }
                            else if (thisRoom.name.Contains("D") && testRoom.name.Contains("U"))
                            {
                                testRoom.GetComponent<UpdateRoomConnections>().connectObject.GetComponent<RoomConnectHandler>().southTest = true;
                                thisRoom.GetComponent<UpdateRoomConnections>().southConnection = true;
                                testRoom.GetComponent<UpdateRoomConnections>().northConnection = true;
                                southTest = true;
                            }
                        }
                        else if (thisRoom.name.Contains("Objective"))
                        {
                            thisRoom.GetComponent<UpdateRoomConnections>().southConnection = true;
                            southTest = true;
                        }
                        else thisRoom.GetComponent<UpdateRoomConnections>().southConnection = false;
                    }
                    if (!eastTest)
                    {
                        if (east.GetComponent<SpriteRenderer>().bounds.Intersects(testRoom.GetComponent<SpriteRenderer>().bounds)) // EAST
                        {
                            if (testRoom.name.Contains("Empty") || testRoom.name.Contains("Spawn"))
                            {
                                thisRoom.GetComponent<UpdateRoomConnections>().eastConnection = false;
                                eastTest = true;
                            }
                            else if (thisRoom.name.Contains("R") && testRoom.name.Contains("L"))
                            {
                                testRoom.GetComponent<UpdateRoomConnections>().connectObject.GetComponent<RoomConnectHandler>().eastTest = true;
                                thisRoom.GetComponent<UpdateRoomConnections>().eastConnection = true;
                                testRoom.GetComponent<UpdateRoomConnections>().westConnection = true;
                                eastTest = true;
                            }
                        }
                        else thisRoom.GetComponent<UpdateRoomConnections>().eastConnection = false;
                    }
                    if (!westTest)
                    {
                        if (west.GetComponent<SpriteRenderer>().bounds.Intersects(testRoom.GetComponent<SpriteRenderer>().bounds)) // WEST
                        {
                            if (testRoom.name.Contains("Empty"))
                            {
                                thisRoom.GetComponent<UpdateRoomConnections>().westConnection = false;
                                westTest = true;
                            }
                            else if (testRoom.name.Contains("Spawn"))
                            {
                                thisRoom.GetComponent<UpdateRoomConnections>().westConnection = true;
                                westTest = true;
                            }
                            else if (thisRoom.name.Contains("L") && testRoom.name.Contains("R"))
                            {
                                testRoom.GetComponent<UpdateRoomConnections>().connectObject.GetComponent<RoomConnectHandler>().westTest = true;
                                thisRoom.GetComponent<UpdateRoomConnections>().westConnection = true;
                                testRoom.GetComponent<UpdateRoomConnections>().eastConnection = true;
                                westTest = true;
                            }
                        }
                        else thisRoom.GetComponent<UpdateRoomConnections>().westConnection = false;
                    }
                }
                else
                {
                    childID = -1;
                    childMax = roomParent.transform.childCount;
                }
                childID++;
            }
        }
    }
}
