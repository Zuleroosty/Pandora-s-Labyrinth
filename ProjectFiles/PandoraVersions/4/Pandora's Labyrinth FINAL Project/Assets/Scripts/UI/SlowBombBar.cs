using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBombBar : MonoBehaviour
{
    public GameObject targetObject;
    public Vector3 scaleAdjuster, spawnScale;

    // Start is called before the first frame update
    void Start()
    {
        scaleAdjuster = targetObject.transform.localScale;
        spawnScale = scaleAdjuster;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject == null)
        {
            targetObject = GameObject.Find("----PlayerObjectParent----");
        }
        else
        {
            scaleAdjuster.y = targetObject.GetComponent<PlayerController>().burstTimer / 360;
            scaleAdjuster.x = 1;
            transform.localScale = scaleAdjuster;
        }
    }
}
