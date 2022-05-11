using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public enum currentState {Correct, Incorrect};
    currentState objectState;
    public int currentColour, matchingColour;
    public Color thisColour;
    public bool canUpdate;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        currentColour = 0;

        thisColour = GetComponent<SpriteRenderer>().color;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (matchingColour == 0)
        { 
            if (this.name.Contains("2"))
            {
                matchingColour = GameObject.Find("GameManager").GetComponent<GameManager>().c2;
                matchingColour = GameObject.Find("GameManager").GetComponent<GameManager>().coloursTaken++;
                GameObject.Find("GameManager").GetComponent<GameManager>().c2Assigned = true;
            }
            if (this.name.Contains("3"))
            {
                matchingColour = GameObject.Find("GameManager").GetComponent<GameManager>().c3;
                matchingColour = GameObject.Find("GameManager").GetComponent<GameManager>().coloursTaken++;
                GameObject.Find("GameManager").GetComponent<GameManager>().c3Assigned = true;
            }
            if (this.name.Contains("4"))
            {
                matchingColour = GameObject.Find("GameManager").GetComponent<GameManager>().c4;
                matchingColour = GameObject.Find("GameManager").GetComponent<GameManager>().coloursTaken++;
                GameObject.Find("GameManager").GetComponent<GameManager>().c4Assigned = true;
            }
            if (this.name.Contains("1"))
            {
                matchingColour = GameObject.Find("GameManager").GetComponent<GameManager>().c1;
                matchingColour = GameObject.Find("GameManager").GetComponent<GameManager>().coloursTaken++;
                GameObject.Find("GameManager").GetComponent<GameManager>().c1Assigned = true;
            }
        }
        
        GetComponent<SpriteRenderer>().color = thisColour;

        if (GameObject.Find("GameManager").GetComponent<GameManager>().puzzleComplete == false)
        {
            if (!GetComponent<SpriteRenderer>().bounds.Intersects(player.GetComponent<SpriteRenderer>().bounds)) canUpdate = true;

            if (GetComponent<SpriteRenderer>().bounds.Intersects(player.GetComponent<SpriteRenderer>().bounds) && canUpdate)
            {
                canUpdate = false;
                if (currentColour == matchingColour)
                {
                    GameObject.Find("Square (" + GameObject.Find("GameManager").GetComponent<GameManager>().compRecieved + ")").GetComponent<SpriteRenderer>().color = Color.red;
                    GameObject.Find("GameManager").GetComponent<GameManager>().compRecieved--;
                }
                currentColour++;
                UpdateRandomState();
            }
            if (currentColour == matchingColour && objectState != currentState.Correct)
            {
                objectState = currentState.Correct;
                GameObject.Find("GameManager").GetComponent<GameManager>().compRecieved++;
                GameObject.Find("Square (" + GameObject.Find("GameManager").GetComponent<GameManager>().compRecieved + ")").GetComponent<SpriteRenderer>().color = Color.green;
            }
            if (currentColour != matchingColour) objectState = currentState.Incorrect;
        }
    }

    void UpdateRandomState()
    {
        objectState = currentState.Incorrect;
        if (currentColour == 6) currentColour = 1;

        switch (currentColour)
        {
            case 1:
                thisColour = Color.red;
                break;
            case 2:
                thisColour = Color.blue;
                break;
            case 3:
                thisColour = Color.yellow;
                break;
            case 4:
                thisColour = Color.cyan;
                break;
            case 5:
                thisColour = Color.black;
                break;
        }
    }
}
