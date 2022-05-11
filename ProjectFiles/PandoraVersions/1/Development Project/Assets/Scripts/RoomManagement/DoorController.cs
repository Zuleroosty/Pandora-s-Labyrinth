using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject collisionBox;
    public float animSpeed;
    public bool isUnlocked, animFinished, forceUpdate;

    CollisionManager boxCollision;
    Vector3 spawnPos, spawnScale;
    Color thisColour;
    int animTimer;

    // Start is called before the first frame update
    void Start()
    {
        thisColour = GetComponent<SpriteRenderer>().color;
        boxCollision = GetComponent<CollisionManager>();
        spawnPos = transform.position;
        spawnScale = transform.localScale;

        animSpeed = 0.04f;
        animTimer = 25;

        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (forceUpdate)
        {
            forceUpdate = false;
            if (isUnlocked) Lock();
            else Unlock();
        }

        GetComponent<SpriteRenderer>().color = thisColour;

        if (isUnlocked && !animFinished)
        {
            if (animTimer > 0)
            {
                animTimer--;
                transform.localScale -= new Vector3(animSpeed / 100, animSpeed / 65);
                thisColour.a -= animSpeed;
                GetComponent<SpriteRenderer>().color = thisColour;
            }
            if (animTimer <= 0)
            {
                transform.position -= new Vector3(0, 0, 10);
                animFinished = true;
                if (boxCollision.enableCollision) boxCollision.enableCollision = false;
            }
        }
        if (!isUnlocked)
        {
            transform.position = spawnPos;
            transform.localScale = spawnScale;
            thisColour.a = 1;
            animFinished = false;
            animTimer = 25;
            if (!boxCollision.enableCollision) boxCollision.enableCollision = true;
        }
    }

    public void Unlock()
    {
        isUnlocked = true;
    }

    public void Lock()
    {
        isUnlocked = false;
    }
}
