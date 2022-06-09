using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraActivator : MonoBehaviour
{
    GameObject player;
    CameraMovement camScript;
    GameManager gameManager;
    SpriteRenderer playerSprite, thisSprite;
    public bool canUpdate;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerCollider");
        playerSprite = player.GetComponent<SpriteRenderer>();
        thisSprite = GetComponent<SpriteRenderer>();
        camScript = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSprite.bounds.Intersects(thisSprite.bounds) && canUpdate)
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
