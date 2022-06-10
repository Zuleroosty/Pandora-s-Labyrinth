﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeAdjuster : MonoBehaviour
{
    public GameObject volumeBar;
    bool followMouse;
    Vector3 targetPos;
    float maxVolume, currentVolume;

    // Start is called before the first frame update
    void Start()
    {
        UpdateVolume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) || Input.GetButton("UseBoostGlobal"))
        {
            if ((GameObject.Find("DisplayCursor").GetComponent<SpriteRenderer>().bounds.Intersects(GetComponent<SpriteRenderer>().bounds) || GameObject.Find("DisplayCursor").GetComponent<SpriteRenderer>().bounds.Intersects(volumeBar.GetComponent<SpriteRenderer>().bounds)) && !followMouse)
            {
                followMouse = true;
            }
            else if (followMouse)
            {
                UpdateVolume();
                targetPos = new Vector3(GameObject.Find("DisplayCursor").transform.position.x, transform.position.y, transform.position.z);
                if (GameObject.Find("DisplayCursor").transform.position.x > transform.position.x && transform.position.x < transform.parent.GetComponent<SpriteRenderer>().bounds.max.x) transform.position = Vector3.Lerp(transform.position, targetPos, 3 * Time.deltaTime);
                if (GameObject.Find("DisplayCursor").transform.position.x < transform.position.x && transform.position.x > transform.parent.GetComponent<SpriteRenderer>().bounds.min.x) transform.position = Vector3.Lerp(transform.position, targetPos, 3 * Time.deltaTime);
            }
        }
        else if (followMouse)
        {
            followMouse = false;
            GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerFXHandler>().PlayThrowSpear();
        }
    }
    void UpdateVolume()
    {
        maxVolume = transform.parent.GetComponent<SpriteRenderer>().bounds.max.x - transform.parent.GetComponent<SpriteRenderer>().bounds.min.x;
        currentVolume = transform.position.x - transform.parent.GetComponent<SpriteRenderer>().bounds.min.x;
        GameObject.Find(">GameManager<").GetComponent<AudioHandler>().averageVolume = currentVolume / maxVolume;
    }
}