﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPPickup : MonoBehaviour
{
    public bool hasCollected;
    int xpAmount;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent.parent = GameObject.Find("----Loot----").transform;
        xpAmount = Random.Range(5, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame || GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Pause)
        {
            if (hasCollected)
            {
                if (this.GetComponent<SpriteRenderer>().bounds.Intersects(GameObject.Find("AttractionBar").GetComponent<SpriteRenderer>().bounds))
                {
                    GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().xp += xpAmount;
                    GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerFXHandler>().PlayXPPickup();
                    Destroy(transform.parent.gameObject);
                }
            }
            else
            {
                if (GameObject.Find("ZCollision").GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds) && transform.position.y < GameObject.Find("PCollision").transform.position.y + 0.75f)
                {
                    hasCollected = true;
                }
            }
        }
        else Destroy(gameObject);
    }
}