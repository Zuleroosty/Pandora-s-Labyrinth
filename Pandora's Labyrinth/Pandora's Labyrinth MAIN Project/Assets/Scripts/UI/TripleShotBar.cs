﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotBar : MonoBehaviour
{
    public GameObject targetObject;
    public Vector3 scaleAdjuster, spawnScale;

    // Start is called before the first frame update
    void Start()
    {
        targetObject = GameObject.Find("----PlayerObjectParent----");
        scaleAdjuster = targetObject.transform.localScale;
        spawnScale = scaleAdjuster;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (targetObject == null)
            {
                targetObject = GameObject.Find("----PlayerObjectParent----");
            }
            else
            {
                scaleAdjuster.y = targetObject.GetComponent<PlayerController>().powerShotCooldown / 240;
                scaleAdjuster.x = 1;
                transform.localScale = scaleAdjuster;
            }
        }
    }
}
