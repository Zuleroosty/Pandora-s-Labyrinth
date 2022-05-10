using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningController : MonoBehaviour
{

    public string gridPrint, row1, row2, row3, row4, row5, puzzleID, combatID, spawnID, pandoraID, emptyID;
    public int maxGridSpace, currentGridSpace, maxX, maxY, currentX, currentY, roomSelector, randomOffset, createTimer, puzzleRooms, combatRooms, spawnRooms, pandoraRooms, emptyRooms;
    public Vector3 currentRoom;
    public enum roomType { Puzzle, Combat, Spawn, Pandora, Empty };
    public roomType roomName;

    // Start is called before the first frame update
    void Start()
    { 
        // Room IDs
        spawnID = "1";
        puzzleID = "2";
        combatID = "3";
        pandoraID = "4";
        emptyID = "5";

        //Grid Size
        maxX = 10;
        maxY = 5;
        currentX = 1;
        currentY = 1;

        maxGridSpace = maxX * maxY;

        // Room Stats
        gridPrint = row1 + "\n" + row2 + "\n" + row3 + "\n" + row4 + "\n" + row5;
    }

    // Update is called once per frame
    void Update()
    {
        currentRoom = new Vector3(currentX, currentY);

        // Create Grid
        if (currentX <= maxX && currentY <= maxY)
        {
            if (createTimer < 5)
            {
                createTimer++;
            }
            if (createTimer >= 5)
            {
                createTimer = 0;

                // Runs every second.

                if (currentX <= maxX) SpawnNewRoom();
            }
        }
    }

    void SpawnNewRoom()
    {
        if (currentGridSpace > maxGridSpace)
        {
            print("Grid Complete.\nPuzzle Rooms: " + puzzleRooms + "\nCombat Rooms: " + combatRooms + "\nSpawn Rooms: " + spawnRooms + "\nPandora's Room: " + pandoraRooms + "\nHallways: " + emptyRooms);
        }
        if (currentGridSpace <= maxGridSpace)
        {
            roomSelector = Random.Range(0, 15);
            randomOffset = Random.Range(0, 20);
            if (randomOffset > 8)
            {
                if (roomSelector >= 4 && roomSelector < 6)
                {
                    currentGridSpace++;
                    // Skip X
                    switch (currentY)
                    {
                        case 1:
                            row1 = row1 + "0";
                            ChangeRowColumn();
                            break;
                        case 2:
                            row2 = row2 + "0";
                            ChangeRowColumn();
                            break;
                        case 3:
                            row3 = row3 + "0";
                            ChangeRowColumn();
                            break;
                        case 4:
                            row4 = row4 + "0";
                            ChangeRowColumn();
                            break;
                        case 5:
                            row5 = row5 + "0";
                            ChangeRowColumn();
                            break;
                    }
                }
            }
            if (randomOffset < 8)
            {
                // Select Room 1
                if (roomSelector >= 4 && roomSelector < 6 && spawnRooms < 1)
                {
                    if (roomSelector >= 4 && roomSelector < 6 && spawnRooms >= 1)
                    {
                        SpawnNewRoom();
                    }
                    if (currentX > 2 && currentX < 7 && currentY >= 2 && currentY <= 4)
                    {
                        spawnRooms++;
                        currentGridSpace++;
                        switch (currentY)
                        {
                            case 1:
                                row1 = row1 + spawnID;
                                ChangeRowColumn();
                                break;
                            case 2:
                                row2 = row2 + spawnID;
                                ChangeRowColumn();
                                break;
                            case 3:
                                row3 = row3 + spawnID;
                                ChangeRowColumn();
                                break;
                            case 4:
                                row4 = row4 + spawnID;
                                ChangeRowColumn();
                                break;
                            case 5:
                                row5 = row5 + spawnID;
                                ChangeRowColumn();
                                break;
                        }
                    }
                    else
                    {
                        SpawnNewRoom();
                    }
                }
                // Select Room 2
                if (roomSelector >= 8 && roomSelector <= 10)
                {
                    if (puzzleRooms <= 14)
                    {
                        puzzleRooms++;
                        currentGridSpace++;
                        switch (currentY)
                        {
                            case 1:
                                row1 = row1 + puzzleID;
                                ChangeRowColumn();
                                break;
                            case 2:
                                row2 = row2 + puzzleID;
                                ChangeRowColumn();
                                break;
                            case 3:
                                row3 = row3 + puzzleID;
                                ChangeRowColumn();
                                break;
                            case 4:
                                row4 = row4 + puzzleID;
                                ChangeRowColumn();
                                break;
                            case 5:
                                row5 = row5 + puzzleID;
                                ChangeRowColumn();
                                break;
                        }
                    }
                    if (puzzleRooms > 14)
                    {
                        SpawnNewRoom();
                    }
                }

                // Select Room 3
                if (roomSelector >= 13 && roomSelector < 15)
                {
                    if (combatRooms <= 10)
                    {
                        combatRooms++;
                        currentGridSpace++;
                        switch (currentY)
                        {
                            case 1:
                                row1 = row1 + combatID;
                                ChangeRowColumn();

                                break;
                            case 2:
                                row2 = row2 + combatID;
                                ChangeRowColumn();

                                break;
                            case 3:
                                row3 = row3 + combatID;
                                ChangeRowColumn();

                                break;
                            case 4:
                                row4 = row4 + combatID;
                                ChangeRowColumn();

                                break;
                            case 5:
                                row5 = row5 + combatID;
                                ChangeRowColumn();

                                break;
                        }
                    }
                    if (combatRooms > 10)
                    {
                        SpawnNewRoom();
                    }
                }
                // Select Room 4
                if (roomSelector > 2 && roomSelector < 4)
                {
                    if (pandoraRooms < 1)
                    {
                        pandoraRooms++;
                        currentGridSpace++;
                        switch (currentY)
                        {
                            case 1:
                                row1 = row1 + pandoraID;
                                ChangeRowColumn();

                                break;
                            case 2:
                                row2 = row2 + pandoraID;
                                ChangeRowColumn();

                                break;
                            case 3:
                                row3 = row3 + pandoraID;
                                ChangeRowColumn();

                                break;
                            case 4:
                                row4 = row4 + pandoraID;
                                ChangeRowColumn();

                                break;
                            case 5:
                                row5 = row5 + pandoraID;
                                ChangeRowColumn();

                                break;
                        }
                    }
                    if (pandoraRooms >= 1)
                    {
                        SpawnNewRoom();
                    }
                }

                // Select Room 5
                if (roomSelector == 1 || roomSelector == 7)
                {
                    if (emptyRooms <= 8)
                    {
                        emptyRooms++;
                        currentGridSpace++;
                        switch (currentY)
                        {
                            case 1:
                                row1 = row1 + emptyID;
                                ChangeRowColumn();

                                break;
                            case 2:
                                row2 = row2 + emptyID;
                                ChangeRowColumn();

                                break;
                            case 3:
                                row3 = row3 + emptyID;
                                ChangeRowColumn();

                                break;
                            case 4:
                                row4 = row4 + emptyID;
                                ChangeRowColumn();

                                break;
                            case 5:
                                row5 = row5 + emptyID;
                                ChangeRowColumn();

                                break;
                        }
                    }
                    if (emptyRooms > 8)
                    {
                        SpawnNewRoom();
                    }
                }
            }
        }
        void ChangeRowColumn()
        {
            roomSelector = Random.Range(0, 7);
            currentX++;
            if (currentY <= maxY && currentX >= maxX)
            {
                //Copy to currentY >= MaxY
                currentX = 1;
                currentY++;
            }
        }
    }
}
