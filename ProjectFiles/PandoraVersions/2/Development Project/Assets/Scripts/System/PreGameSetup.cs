using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGameSetup : MonoBehaviour
{
    public bool readyToPlay;
    public Sprite crossHair, cursor;
    GameObject crossHair0;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<GameManager>().gameState = "PreGame";
        Application.targetFrameRate = 60;

        // Hide Cursor / Replace
        crossHair0 = new GameObject("Crosshair", typeof(SpriteRenderer), typeof(CursorController));
        crossHair0.GetComponent<SpriteRenderer>().sprite = crossHair;
        readyToPlay = true;
    }
}
