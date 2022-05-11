using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoxPathEnd : MonoBehaviour
{
    public bool needNewPath;
    int delayTimer;

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<SpriteRenderer>().bounds.Intersects(GameObject.Find("Player").GetComponent<SpriteRenderer>().bounds))
        {
            transform.position = GameObject.Find("Player").transform.position;
            needNewPath = true;
        }
        if (needNewPath)
        {
            if (delayTimer < 2) delayTimer++;
            else
            {
                needNewPath = false;
                delayTimer = 0;
            }
        }
    }
}
