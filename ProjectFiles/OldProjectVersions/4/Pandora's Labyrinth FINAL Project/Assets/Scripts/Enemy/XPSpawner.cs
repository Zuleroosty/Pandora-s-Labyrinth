﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPSpawner : MonoBehaviour
{
    public GameObject xpObject;
    public int xpMax;
    int xpCount;

    // Update is called once per frame
    void Update()
    {
        if (xpCount < xpMax)
        {
            xpCount++;
            Instantiate(xpObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        }
        if (xpCount >= xpMax) Destroy(gameObject);
    }
}