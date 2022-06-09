using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillGrid : MonoBehaviour
{
    public GameObject lruRoom, lrdRoom, lrudRoom, noRoom, newRoom;
    public bool isActive;
    public int childID, childMax, randNum, maxY, maxX;
    GameObject roomsMasterParent, roomParent;

    private void Start()
    {
        roomsMasterParent = GameObject.Find("----Rooms----");
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        maxY = GameObject.Find(">GameManager<").GetComponent<GameManager>().maxY;
        maxX = GameObject.Find(">GameManager<").GetComponent<GameManager>().maxX;
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Reset) isActive = true;
        else
        {
            if (transform.position.y < ((maxY * 21) * -1) || transform.position.x > maxX * 35) isActive = false;
            CheckRoomOverlap();
        }
    }
    void CheckRoomOverlap()
    {
        if (childID < childMax && childMax == roomsMasterParent.transform.childCount)
        {
            roomParent = roomsMasterParent.transform.GetChild(childID).gameObject;
            if (GetComponent<SpriteRenderer>().bounds.Intersects(roomParent.GetComponent<SpriteRenderer>().bounds))
            {
                isActive = false;
            }
            else if (childID >= (childMax - 1))
            {
                if (GameObject.Find(">GameManager<").GetComponent<GameManager>().fillGrid && isActive)
                {
                    randNum = Random.Range(1, 5);
                    if (randNum == 1)
                    {
                        newRoom = Instantiate(lrudRoom, transform.position, Quaternion.identity);
                    }
                    if (randNum == 2)
                    {
                        newRoom = Instantiate(lrdRoom, transform.position, Quaternion.identity);
                    }
                    if (randNum == 3)
                    {
                        newRoom = Instantiate(lruRoom, transform.position, Quaternion.identity);
                    }
                    if (randNum == 4)
                    {
                        newRoom = Instantiate(noRoom, transform.position, Quaternion.identity);
                    }
                    isActive = false;
                }
            }
        }
        else
        {
            childID = -1;
            childMax = roomsMasterParent.transform.childCount;
        }
        childID++;
    }
}
