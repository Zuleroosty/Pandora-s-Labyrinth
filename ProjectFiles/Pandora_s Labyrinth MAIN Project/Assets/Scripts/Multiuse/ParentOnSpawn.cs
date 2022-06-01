using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentOnSpawn : MonoBehaviour
{
    public bool isRoom;

    // Start is called before the first frame update
    void Start()
    {
        if (isRoom) transform.parent = GameObject.Find("----Rooms----").transform;
    }
}
