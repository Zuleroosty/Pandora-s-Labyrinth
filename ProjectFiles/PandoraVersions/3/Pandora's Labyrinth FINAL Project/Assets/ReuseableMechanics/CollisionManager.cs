using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public GameObject playerObject;
    PlayerController playerScript;
    SpriteRenderer playerSprite, thisSprite;
    Vector3 pushBack;
    float plBounds, prBounds, puBounds, pdBounds, playerSizeX, playerSizeY, offSet;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = playerObject.GetComponent<PlayerController>();
        playerSprite = playerObject.GetComponent<SpriteRenderer>();
        thisSprite = GetComponent<SpriteRenderer>();
        offSet = 0.09f;
    }

    // Update is called once per frame
    void Update()
    {
        // Reset pushback
        pushBack = new Vector3(0, 0);

        // Update size of player
        playerSizeX = playerSprite.bounds.max.x - playerSprite.bounds.min.x;
        playerSizeY = playerSprite.bounds.max.y - playerSprite.bounds.min.y;

        // Set player bounds
        plBounds = playerObject.transform.position.x - (playerSizeX / 2);
        prBounds = playerObject.transform.position.x + (playerSizeX / 2);
        pdBounds = playerObject.transform.position.y - (playerSizeY / 2);
        puBounds = playerObject.transform.position.y + (playerSizeY / 2);

        //Determine player relative position
        if (playerObject.transform.position.x > thisSprite.bounds.max.x - offSet)
        {
            print("right");
            if (plBounds < thisSprite.bounds.max.x && plBounds > thisSprite.bounds.min.x) // Right
            {
                pushBack = new Vector3((playerScript.speed + 0.02f), 0);
                print("rightCol");
            }
        }
        if (playerObject.transform.position.x < thisSprite.bounds.min.x + offSet)
        {
            print("left");
            if (prBounds < thisSprite.bounds.max.x && prBounds > thisSprite.bounds.min.x) // Left
            {
                pushBack = new Vector3((playerScript.speed + 0.02f) * -1, 0);
                print("leftCol");
            }
        }
        if (playerObject.transform.position.y > thisSprite.bounds.max.y - offSet)
        {
            print("up");
            if (pdBounds < thisSprite.bounds.max.y && pdBounds > thisSprite.bounds.min.y) // Up
            {
                pushBack = new Vector3(0, (playerScript.speed + 0.02f));
                print("upCol");
            }
        }
        if (playerObject.transform.position.y < thisSprite.bounds.min.y + offSet)
        {
            print("down");
            if (puBounds < thisSprite.bounds.max.y && puBounds > thisSprite.bounds.min.y) // Down
            {
                pushBack = new Vector3(0, (playerScript.speed + 0.02f) * -1);
                print("downCol");
            }
        }

        // Collision Trigger
        if (thisSprite.bounds.Intersects(playerSprite.bounds))
        {
            print("COLLIDING");
            playerObject.transform.position += pushBack;
        }
    }
}
