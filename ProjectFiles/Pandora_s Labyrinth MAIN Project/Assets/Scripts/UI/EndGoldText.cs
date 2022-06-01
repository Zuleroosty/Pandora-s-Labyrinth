using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoldText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("----PlayerObjectParent----") != null) GetComponent<TextMesh>().text = "x" + GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().gold;
    }
}
