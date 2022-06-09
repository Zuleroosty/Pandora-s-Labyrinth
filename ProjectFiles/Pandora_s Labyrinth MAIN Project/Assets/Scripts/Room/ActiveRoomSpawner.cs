using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRoomSpawner : MonoBehaviour
{
    public GameObject testObject, lrRoom, lruRoom, lrdRoom, lrudRoom, newRoom, spawnRoom, prevRoom;
    Vector3 testRight, testLeft, testDown;
    public bool canSpawnHere, testingLocation, canContinue;
    public bool failedDown, failedRight, failedLeft;
    public int maxRooms, currentX, currentY, maxX, maxY, randNum, delay, dirNum, interval, downCounter;

    private void Start()
    {
        testRight = new Vector3(35, 0, 0);
        testLeft = new Vector3(-35, 0, 0);
        testDown = new Vector3(0, -21, 0);

        currentX = 0;
        currentY = 0;

        interval = 3; // UPDATE EVERY X FRAMES

        if (maxX == 0) maxX = 4;
        if (maxY == 0) maxY = 5;

        maxRooms = maxX * maxY - 2;

        dirNum = 1;
        CheckLocation();
    }
    private void Update()
    {
        if (GameObject.Find("SpawnRoomParent(Clone)") == null) Instantiate(spawnRoom, new Vector3(0, 0, 0), Quaternion.identity);
        if (delay < interval) delay++;
        if (delay >= interval)
        {
            delay = 0;
            if (testingLocation)
            {
                if (canSpawnHere)
                {
                    switch (dirNum)
                    {
                        case 1:
                            currentX++;
                            transform.position += new Vector3(35, 0, 0);
                            break;
                        case 2:
                            currentX--;
                            transform.position += new Vector3(-35, 0, 0);
                            break;
                        case 3:
                            currentY++;
                            transform.position += new Vector3(0, -21, 0);
                            break;
                    }
                    SpawnRoom();
                }
                else SetFailed();
            }
            else
            {
                if (failedRight && failedLeft && failedDown)
                {
                    SpawnPandoraRoom();
                    Destroy(gameObject);
                }
                else ChooseDirection();
            }
        }
    }

    void SetFailed()
    {
        switch (dirNum)
        {
            case 1:
                failedRight = true;
                break;
            case 2:
                failedLeft = true;
                break;
            case 3:
                failedDown = true;
                break;
        }
        testingLocation = false;
        canContinue = true;
    }
    void SpawnPandoraRoom()
    {
        Destroy(newRoom.gameObject);
        if (newRoom == null)
        {
            newRoom = Instantiate(lrRoom, transform.position, Quaternion.identity);
            newRoom.transform.parent = GameObject.Find("----Rooms----").transform;
            newRoom.name = "ObjectiveLRD";
            GameObject.Find(">GameManager<").GetComponent<GameManager>().fillGrid = true;
            if (GameObject.Find(">GameManager<").GetComponent<GameManager>().spawnNextLvl) GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState = GameManager.state.Start;
            else if (GameObject.Find(">GameManager<").GetComponent<GameManager>().quickStart)
            {
                GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState = GameManager.state.Start;
                GameObject.Find(">GameManager<").GetComponent<GameManager>().quickStart = false;
            }
            else GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState = GameManager.state.Menu;
            Destroy(gameObject);
        }
    }
    void SpawnRoom()
    {
        canContinue = true;
        testingLocation = false;
        failedDown = false;
        failedLeft = false;
        failedRight = false;

        if (dirNum == 3) downCounter++;

        // decide room based on previous room location
        if (downCounter > 0 && prevRoom != null)
        {
            Destroy(prevRoom.gameObject);
            prevRoom = Instantiate(lrudRoom, new Vector3(transform.position.x, transform.position.y + 21, transform.position.z), Quaternion.identity);
            newRoom = Instantiate(lrudRoom, transform.position, Quaternion.identity);
        }
        else
        {
            SpawnRandomRoom();
        }

        transform.position = newRoom.transform.position;
        prevRoom = newRoom;
    }
    void CheckLocation()
    {
        delay = 0;
        canSpawnHere = true;
        testingLocation = true;
        if (dirNum == 1 || dirNum == 2) downCounter = 0;
        if (dirNum == 1 && failedRight)
        {
            if (currentX > 0) dirNum = 2;
            else if (currentY < maxY) dirNum = 3;
            else if (currentX == 0) SpawnPandoraRoom();
        }
        else if (dirNum == 2 && failedLeft)
        {
            if (currentX < maxX) dirNum = 1;
            else if (currentY < maxY) dirNum = 3;
            else if (currentY == maxY) SpawnPandoraRoom();
        }
        switch (dirNum)
        {
            case 1:
                testObject.transform.localPosition = testRight;
                break;
            case 2:
                testObject.transform.localPosition = testLeft;
                break;
            case 3:
                if (currentY < maxY) testObject.transform.localPosition = testDown;
                else ChooseDirection();
                break;
        }
    }
    void ChooseDirection()
    {
        if (!failedLeft || !failedRight)
        {
            randNum = Random.Range(1, 4); // 1 - right / 2 - left / 3 - down
            if (randNum == 1)
            {
                if (currentX >= maxX) SetFailed();
                else
                {
                    dirNum = 1;
                    CheckLocation();
                }
            }
            if (randNum == 2)
            {
                if (currentX <= 0) SetFailed();
                else
                {
                    dirNum = 2;
                    CheckLocation();
                }
            }
            if (randNum == 3)
            {
                if (currentY >= maxY) SetFailed();
                else
                {
                    dirNum = 3;
                    CheckLocation();
                }
            }
        }
        else if (currentY < maxY)
        {
            dirNum = 3;
            CheckLocation();
        }
        else SpawnPandoraRoom();
    }
    void SpawnRandomRoom()
    {
        randNum = Random.Range(0, 4);
        if (randNum == 0)
        {
            newRoom = Instantiate(lrRoom, transform.position, Quaternion.identity);
        }
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
    }
}
