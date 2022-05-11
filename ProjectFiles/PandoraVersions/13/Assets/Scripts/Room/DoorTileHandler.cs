using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTileHandler : MonoBehaviour
{
    public Sprite Arrow, Block;
    public GameObject doorObject;

    // Update is called once per frame
    void Update()
    {
        if (doorObject.GetComponent<DoorHandler>().isLocked)GetComponent<SpriteRenderer>().sprite = Block;
        else GetComponent<SpriteRenderer>().sprite = Arrow;
    }
}
