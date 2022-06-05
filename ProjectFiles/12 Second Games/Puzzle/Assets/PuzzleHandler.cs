using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleHandler : MonoBehaviour
{
    public int currentColour, colourCode;
    bool correctColour, playerTouching, gmUpdated;

    void Start()
    {
        colourCode = Random.Range(0, 6);
        currentColour = Random.Range(0, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().gameState == GameManager.state.Menu)
        {
            correctColour = false;
            playerTouching = false;
            if (this.name.Contains("1"))
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().cc1 = Random.Range(0, 5);
                currentColour = GameObject.Find("GameManager").GetComponent<GameManager>().cc1;
            }
            if (this.name.Contains("2"))
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().cc2 = Random.Range(0, 5);
                currentColour = GameObject.Find("GameManager").GetComponent<GameManager>().cc2;
            }
            if (this.name.Contains("3"))
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().cc3 = Random.Range(0, 5);
                currentColour = GameObject.Find("GameManager").GetComponent<GameManager>().cc3;
            }
            if (this.name.Contains("4"))
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().cc4 = Random.Range(0, 5);
                currentColour = GameObject.Find("GameManager").GetComponent<GameManager>().cc4;
            }
            currentColour = Random.Range(0, 5);
        }
        else if (!correctColour)
        {
            if (currentColour == colourCode)
            {
                correctColour = true;
                gmUpdated = false;
            }

            if (GetComponent<SpriteRenderer>().bounds.Intersects(GameObject.Find("Player").GetComponent<SpriteRenderer>().bounds))
            {
                // WHEN PLAYER TOUCHES THIS OBJECT INCREASE COLOUR COUNT W/GATE
                if (!playerTouching)
                {
                    playerTouching = true;
                    currentColour++;
                }
            }
            else playerTouching = false;
        }
        else if (!gmUpdated)
        {
            gmUpdated = true;
            if (!GameObject.Find("GameManager").GetComponent<GameManager>().c1) GameObject.Find("GameManager").GetComponent<GameManager>().c1 = true;
            else if (!GameObject.Find("GameManager").GetComponent<GameManager>().c2) GameObject.Find("GameManager").GetComponent<GameManager>().c2 = true;
            else if (!GameObject.Find("GameManager").GetComponent<GameManager>().c3) GameObject.Find("GameManager").GetComponent<GameManager>().c3 = true;
            else if (!GameObject.Find("GameManager").GetComponent<GameManager>().c4) GameObject.Find("GameManager").GetComponent<GameManager>().c4 = true;
        }
        switch (currentColour)
        {
            case 0:
                GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case 1:
                GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case 2:
                GetComponent<SpriteRenderer>().color = Color.magenta;
                break;
            case 3:
                GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            case 4:
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 5:
                currentColour = 0;
                break;
        }
    }
}
