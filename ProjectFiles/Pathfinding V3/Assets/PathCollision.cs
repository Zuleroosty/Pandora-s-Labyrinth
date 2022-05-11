using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCollision : MonoBehaviour
{
    public bool forceLeft, forceUp, noForce, forceRight, forceDown;
    // PRIMARILY FOR INFO STORAGE

    private void Start()
    {
        transform.parent = GameObject.Find("----PathCollision----").transform;
        if (noForce)
        {
            forceLeft = false;
            forceUp = false;
        }
    }
}
