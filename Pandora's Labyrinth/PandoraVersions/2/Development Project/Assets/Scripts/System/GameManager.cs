using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector3 currentRoom;
    public string gameState; //PreGame In-Game EndGame
    public GameObject player, currentRoomParent;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PreGameSetup>().readyToPlay)
        {
            gameState = "In-Game";
        }
    }
}
