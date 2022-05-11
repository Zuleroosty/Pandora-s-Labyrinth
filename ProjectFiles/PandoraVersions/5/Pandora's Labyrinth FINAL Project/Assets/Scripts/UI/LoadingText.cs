using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingText : MonoBehaviour
{
    Color thisColour;
    TextMesh thisTextMesh;
    int textTimer;

    // Start is called before the first frame update
    void Start()
    {
        thisTextMesh = GetComponent<TextMesh>();
        thisColour = thisTextMesh.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState != GameManager.state.GenLevel)
        {
            thisTextMesh.text = "Labyrinth Loaded";
            if (thisColour.a > 0) thisColour.a -= 0.1f;
        }
        else
        {
            if (thisColour.a < 1) thisColour.a += 0.1f;
            if (textTimer < 40) textTimer++;
            if (textTimer >= 0) thisTextMesh.text = "Loading Labyrinth";
            if (textTimer >= 10) thisTextMesh.text = "Loading Labyrinth.";
            if (textTimer >= 20) thisTextMesh.text = "Loading Labyrinth..";
            if (textTimer >= 30) thisTextMesh.text = "Loading Labyrinth...";
            if (textTimer >= 40) textTimer = 0;
        }
        GetComponent<TextMesh>().color = thisColour;
    }
}
