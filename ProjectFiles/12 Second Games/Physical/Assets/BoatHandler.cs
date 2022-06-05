using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatHandler : MonoBehaviour
{
    GameObject boatsParent;
    bool hasCollided;
    float speed, scale;
    Color thisColour;
    int childID, childMax;

    // Start is called before the first frame update
    void Start()
    {
        boatsParent = GameObject.Find("BoatObjectsParent");
        transform.parent = GameObject.Find("FallingObjectsParent").transform;
        speed = Random.Range(0.040f, 0.066f);
        transform.position = new Vector3(Random.Range(-7.00f, 8.01f), 10, 0);
        scale = Random.Range(0.55f, 0.75f);
        transform.localScale = new Vector3(scale * 0.65f, scale, 1);
        thisColour = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        { 
            DestroyOnOverLap();

            if (transform.position.y < -5) transform.position = new Vector3(Random.Range(-7.00f, 8.01f), 5, 0);
            transform.position -= new Vector3(0, speed + GameObject.Find("GameManager").GetComponent<GameManager>().objectOffset, 0);

            if (GetComponent<SpriteRenderer>().bounds.Intersects(GameObject.Find("PlayerObject").GetComponent<SpriteRenderer>().bounds) && !hasCollided)
            {
                hasCollided = true;
                GameObject.Find("GameManager").GetComponent<GameManager>().boatsSaved++;
            }
            else if (hasCollided)
            {
                if (thisColour.a > 0) thisColour.a -= 0.075f;
                else Destroy(gameObject);
                GetComponent<SpriteRenderer>().color = thisColour;
            }
        }
        else Destroy(gameObject);
    }
    void DestroyOnOverLap()
    {
        if (childID < childMax && childMax == boatsParent.transform.childCount)
        {
            if (GetComponent<SpriteRenderer>().bounds.Intersects(boatsParent.transform.GetChild(childID).gameObject.GetComponent<SpriteRenderer>().bounds))
            {
                Destroy(gameObject);
            }
        }
        else
        {
            childID = -1;
            childMax = boatsParent.transform.childCount;
        }
        childID++;
    }
}
