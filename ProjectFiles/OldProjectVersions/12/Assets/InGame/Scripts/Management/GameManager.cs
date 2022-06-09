using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // PREFABS
    public GameObject lvlUpNotification, potionNotification, bombNotification, goldNotification, armourNotification, spearNotification, newNotificationObject;
    public GameObject ammoDrop, healthDrop;
    public GameObject initialRoomPrefab, playerPrefab, roomSpawner, newSpawner;

    // GRID SPAWNING
    public bool fillGrid, spawnNextLvl, quickStart;

    // SHORTCUTS
    public PermissionsHandler permHandler;
    public LevelHandler lvlHandler;

    // ROOM SPECIFIC
    public GameObject currentRoomParent;

    // PLAYER SPECIFIC
    public GameObject playerObject;

    // GAME SYSTEM/SETTINGS
    public enum state {GenLevel, Menu, Start, InGame, Lose, Win, Reset, Pause, Quit}
    public state gameState;

    // RESET VARIABLES
    public bool levelReset, readyToStart;
    int childID, childMax;
    GameObject roomsMasterParent, roomParent;

    // UI VARIABLES
    public int activeNotifications;
    public string newNotificationText;
    public bool isGamePaused;

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
            if (gameState == state.Win)
            {
                //lvlHandler.NewLevel();
            }
            if (gameState == state.Start)
            {
                gameState = state.InGame;
                levelReset = false;
                spawnNextLvl = false;
            }
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
                    Destroy(GameObject.Find("----PlayerObjectParent----"));
                    Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                }
                else playerObject.GetComponent<PlayerController>().StartNewLevel();
            }
        }
        if (gameState == state.Start) playerObject = GameObject.Find("----PlayerObjectParent----");

        // GAME PAUSE FUNCTION
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameState == state.InGame || gameState == state.Pause)
            {
                if (gameState == state.InGame) gameState = state.Pause;
                else if (gameState == state.Pause) gameState = state.InGame;
            }
        }
        if (gameState == state.Pause) isGamePaused = true;
        else isGamePaused = false;
    }

    public void NewNotification(string displayText, int type) // 0 = lvlup 1 = health potion 2 = bomb 3 = gold 4 = spear 5 = armour
    {
        GameObject notifyBar;
        switch (type)
        {
            case 0:
                newNotificationObject = lvlUpNotification;
                break;
            case 1:
                newNotificationObject = potionNotification;
                break;
            case 2:
                newNotificationObject = bombNotification;
                break;
            case 3:
                newNotificationObject = goldNotification;
                break;
            case 4:
                newNotificationObject = spearNotification;
                break;
            case 5:
                newNotificationObject = armourNotification;
                break;
        }
        notifyBar = Instantiate(newNotificationObject, new Vector3(GameObject.Find("----NotificationLocation----").transform.position.x, GameObject.Find("----NotificationLocation----").transform.position.y, GameObject.Find("----NotificationLocation----").transform.position.z), Quaternion.identity).gameObject;
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
