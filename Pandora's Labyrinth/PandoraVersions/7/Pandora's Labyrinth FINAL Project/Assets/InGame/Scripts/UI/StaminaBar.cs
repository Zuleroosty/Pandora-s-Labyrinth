using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBar : MonoBehaviour
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
            scaleAdjuster.x = (targetObject.GetComponent<PlayerController>().stamina / targetObject.GetComponent<PlayerController>().maxStamina);
            transform.localScale = scaleAdjuster;
        }
    }
}
