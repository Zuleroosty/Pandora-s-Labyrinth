using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // PREFABS
    public GameObject notificationBar;
    public GameObject ammoDrop, healthDrop;
    public GameObject initialRoomPrefab, playerPrefab, roomSpawner, newSpawner;

    // GRID SPAWNING
    public bool fillGrid, spawnNextLvl;

    // SHORTCUTS
    public PermissionsHandler permHandler;
    public LevelHandler lvlHandler;

    // ROOM SPECIFIC
    public GameObject currentRoomParent;

    // PLAYER SPECIFIC
    public GameObject playerObject;

    // GAME SYSTEM/SETTINGS
    public enum state {GenLevel, Menu, Start, InGame, Lose, Win, Reset, Pause}
    public state gameState;

    // RESET VARIABLES
    public bool levelReset, readyToStart;
    int childID, childMax;
    GameObject roomsMasterParent, roomParent;

    // UI VARIABLES
    public int activeNotifications;
    public string newNotificationText;

    // Start is called before the first frame update
    void Start()
    {
        permHandler = GetComponent<PermissionsHandler>();
        lvlHandler = GetComponent<LevelHandler>();

        playerObject = GameObject.Find("----PlayerObjectParent----");
        roomsMasterParent = GameObject.Find("----Rooms----");

        gameState = state.GenLevel;

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentRoomParent != null) permHandler.canMove = true;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameState == state.Lose)
            {
                gameState = state.Reset;
            }
            else if (gameState == state.Win)
            {
                print("EndLvl");
                lvlHandler.NewLevel();
            }
            if (gameState == state.Start)
            {
                gameState = state.InGame;
                levelReset = false;
                spawnNextLvl = false;
            }
        }
        if (gameState == state.Win && playerObject.GetComponent<PlayerController>().hasPandorasBox)
        {
            spawnNextLvl = true;
        }
        if (gameState == state.Reset && readyToStart)
        {
            if (roomsMasterParent.transform.childCount - 1 >= 0) RemoveALLRooms();
            else if (roomsMasterParent.transform.childCount - 1 < 0) levelReset = true;
            if (levelReset)
            {
                readyToStart = false;
                fillGrid = false;
                newSpawner = Instantiate(roomSpawner, new Vector3(0, 0, 0), Quaternion.identity);
                newSpawner.name = "RoomSpawner";
                gameState = state.GenLevel;
                if (!spawnNextLvl)
                {
                    Destroy(playerObject);
                    Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    playerObject = GameObject.Find("----PlayerObjectParent----");
                }
                else playerObject.GetComponent<PlayerController>().StartNewLevel();
            }
        }
    }

    public void NewNotification(string displayText)
    {
        GameObject notifyBar;
        notifyBar = Instantiate(notificationBar, new Vector3(GameObject.Find("Main Camera").transform.position.x + 50, GameObject.Find("Main Camera").transform.position.y, 0), Quaternion.identity).gameObject;
        notifyBar.GetComponent<DisplayNotifier>().displayText = displayText;
    }
    void RemoveALLRooms()
    {
        if (childID < childMax && childMax == roomsMasterParent.transform.childCount)
        {
            roomParent = roomsMasterParent.transform.GetChild(childID).gameObject;
            if (roomParent != null) Destroy(roomParent.gameObject);
        }
        else
        {
            childID = -1;
            childMax = roomsMasterParent.transform.childCount;
        }
        childID++;
    }
}
