using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float animSpeed;
    public bool isUnlocked, animFinished, forceUpdate;

    Vector3 spawnPos, spawnScale;
    Color thisColour;
    int animTimer;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        thisColour = GetComponent<SpriteRenderer>().color;
        spawnPos = transform.position;
        spawnScale = transform.localScale;

        animSpeed = 0.02f;
        animTimer = 45;

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
                transform.localScale -= new Vector3(animSpeed * 1.8f, animSpeed * 1.4f);
                thisColour.a -= (animSpeed * 6);
                GetComponent<SpriteRenderer>().color = thisColour;
            }
            if (animTimer <= 0)
            {
                transform.position -= new Vector3(0, 0, 10);
                animFinished = true;
                GetComponent<CollisionManager>().enableCollision = false;
            }
        }
        if (!isUnlocked)
        {
            transform.position = spawnPos;
            transform.localScale = spawnScale;
            thisColour.a = 1;
            animFinished = false;
            animTimer = 45;
            GetComponent<CollisionManager>().enableCollision = true;
        }
    }

    public void Unlock()
    {
        thisColour = Color.green;
        isUnlocked = true;
    }

    public void Lock()
    {
        thisColour = Color.red;
        isUnlocked = false;
    }
}
