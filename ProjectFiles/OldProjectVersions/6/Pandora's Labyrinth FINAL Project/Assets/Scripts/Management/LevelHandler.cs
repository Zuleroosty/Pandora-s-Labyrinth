using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    Vector2 currentRoom;
    int currentRoomID;
    GameObject currentRoomParent;
    // Start is called before the first frame update
    void Start()
    {
        currentRoomParent = GameObject.Find("InitialSpawnRoomParent");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
