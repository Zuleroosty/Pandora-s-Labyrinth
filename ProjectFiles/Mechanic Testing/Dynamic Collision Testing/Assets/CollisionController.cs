using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public GameObject bObject;
    PlayerController objectScript;
    SpriteRenderer bObjectSprite, thisBounds;
    Vector3 pushBack;
    float plBounds, prBounds, puBounds, pdBounds, offsetX, offsetY;

    // Start is called before the first frame update
    void Start()
    {
        objectScript = bObject.GetComponent<PlayerController>();
        bObjectSprite = bObject.GetComponent<SpriteRenderer>();
        thisBounds = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update Offsets based on scale
        offsetX = (bObjectSprite.bounds.max.x - bObjectSprite.bounds.min.x) / 6;
        offsetY = (bObjectSprite.bounds.max.y - bObjectSprite.bounds.min.y) / 6;

        // Get Player Object Bounds
        plBounds = bObjectSprite.bounds.min.x;
        prBounds = bObjectSprite.bounds.max.x;
        puBounds = bObjectSprite.bounds.max.y;
        pdBounds = bObjectSprite.bounds.min.y;

        // Reset PushBack
        pushBack = new Vector3(0, 0);

        // Update PushBack / Collision
        //============================

        // Top/Bottom Edges
        if (prBounds > thisBounds.bounds.min.x + offsetX && plBounds < thisBounds.bounds.max.x - offsetX)
        {
            // Up Collision
            if (puBounds > thisBounds.bounds.min.y && pdBounds < thisBounds.bounds.min.y)
            {
                pushBack = new Vector3(0, (objectScript.speed + 0.02f) * -1);
                print("upCol");
            }

            // Down Collision
            else if (pdBounds < thisBounds.bounds.max.y && puBounds > thisBounds.bounds.min.y)
            {
                pushBack = new Vector3(0, objectScript.speed + 0.02f);
                print("downCol");
            }
        }
        // Left/Right Edges
        else if (puBounds > thisBounds.bounds.min.y - offsetY && pdBounds < thisBounds.bounds.max.y + offsetY)
        {
            // Left Collision
            if (plBounds < thisBounds.bounds.max.x && prBounds > thisBounds.bounds.max.x)
            {
                pushBack = new Vector3((objectScript.speed + 0.02f), 0);
                print("leftCol");
            }

            // Right Collision
            else if (prBounds > thisBounds.bounds.min.x && plBounds < thisBounds.bounds.min.x)
            {
                pushBack = new Vector3((objectScript.speed + 0.02f) * -1, 0);
                print("rightCol");
            }
        }

        //============================

        // Trigger Collision
        if (thisBounds.bounds.Intersects(bObjectSprite.bounds))
        {
            print("COLLIDING");
            objectScript.speed = 0.002f;
            bObject.transform.position += pushBack;
        }
        if (!thisBounds.bounds.Intersects(bObjectSprite.bounds))
        {
            objectScript.speed = 0.075f;
        }
    }
}
