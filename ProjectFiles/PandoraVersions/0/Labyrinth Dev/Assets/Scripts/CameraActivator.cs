using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraActivator : MonoBehaviour
{
    GameObject player, mainCam;
    CameraMovement camScript;
    GameManager gameManager;
    Color thisColour;
    SpriteRenderer playerSprite, thisSprite;
    public bool canUpdate;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerSprite = player.GetComponent<SpriteRenderer>();
        thisSprite = GetComponent<SpriteRenderer>();
        camScript = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x < thisSprite.bounds.max.x && player.transform.position.x > thisSprite.bounds.min.x && player.transform.position.y < thisSprite.transform.position.y)
            ///////creating function to switch bool if player is in room
            ///

        if (player.transform.position.x < thisSprite.bounds.max.x && player.transform.position.x > thisSprite.bounds.min.x && canUpdate)
        {
            gameManager.currentRoom = transform.position;
            camScript.roomCentre.y = transform.position.y;
            camScript.roomCentre.x = transform.position.x;
            canUpdate = false;
        }
        if (!playerSprite.bounds.Intersects(thisSprite.bounds))
        {
            canUpdate = true;
        }
    }
}
