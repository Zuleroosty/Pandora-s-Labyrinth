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
    public bool fillGrid;

    // SHORTCUTS
    public UIHandler uiHandler;
    public PermissionsHandler permHandler;
    public LevelHandler lvlHandler;

    // ROOM SPECIFIC
    public GameObject currentRoomParent;

    // PLAYER SPECIFIC
    public GameObject playerObject;

    // GAME SYSTEM/SETTINGS
    public enum state {GenLevel, Start, InGame, Lose, Win, Reset}
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
        uiHandler = GetComponent<UIHandler>();
        permHandler = GetComponent<PermissionsHandler>();
        lvlHandler = GetComponent<LevelHandler>();

        playerObject = GameObject.Find("----PlayerObjectParent----");
        roomsMasterParent = GameObject.Find("----Rooms----");

        gameState = state.GenLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentRoomParent != null) permHandler.canMove = true;
        
        if (gameState == state.Lose || gameState == state.Win)
        {
            //
            //
            //
            //
            //
        }
        if (gameState == state.Start)
        {
            levelReset = false;
            if (Input.GetKeyDown(KeyCode.Return)) gameState = state.InGame;
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
                Destroy(playerObject);
                Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                playerObject = GameObject.Find("----PlayerObjectParent----");
                gameState = state.GenLevel;
            }
        }
    }

    public void NewNotification(string displayText)
    {
        GameObject notifyBar;
        activeNotifications++;
        notifyBar = Instantiate(notificationBar, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity).gameObject;
        notifyBar.GetComponent<DisplayNotifier>().displayText = displayText;
        if (GetComponent<NotificationHandler>().noti1 == null && activeNotifications < 2) GetComponent<NotificationHandler>().noti1 = notifyBar;
        else if (GetComponent<NotificationHandler>().noti2 == null && activeNotifications < 3) GetComponent<NotificationHandler>().noti2 = notifyBar;
        else if (GetComponent<NotificationHandler>().noti3 == null && activeNotifications < 4) GetComponent<NotificationHandler>().noti3 = notifyBar;
        else if (GetComponent<NotificationHandler>().noti4 == null && activeNotifications < 5) GetComponent<NotificationHandler>().noti4 = notifyBar;
        else if (GetComponent<NotificationHandler>().noti5 == null && activeNotifications < 6) GetComponent<NotificationHandler>().noti5 = notifyBar;
        else GetComponent<NotificationHandler>().ShiftUp(notifyBar);
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
