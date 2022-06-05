using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHandler : MonoBehaviour
{
    public int currentNumber, correctNumber, blockID;

    Color thisColour;

    // Start is called before the first frame update
    void Start()
    {
        thisColour = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().gameState == GameManager.state.Primer)
        {
            correctNumber = Random.Range(0, 10);
        }
        if (GameObject.Find("GameManager").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (currentNumber == correctNumber)
            {
                thisColour.a = 0.75f;
                switch (blockID)
                {
                    case 0:
                        GameObject.Find("GameManager").GetComponent<GameManager>().c1 = true;
                        break;
                    case 1:
                        GameObject.Find("GameManager").GetComponent<GameManager>().c2 = true;
                        break;
                    case 2:
                        GameObject.Find("GameManager").GetComponent<GameManager>().c3 = true;
                        break;
                    case 3:
                        GameObject.Find("GameManager").GetComponent<GameManager>().c4 = true;
                        break;
                }
            }
            else
            {
                thisColour.a = 1f;
                switch (blockID)
                {
                    case 0:
                        GameObject.Find("GameManager").GetComponent<GameManager>().c1 = false;
                        break;
                    case 1:
                        GameObject.Find("GameManager").GetComponent<GameManager>().c2 = false;
                        break;
                    case 2:
                        GameObject.Find("GameManager").GetComponent<GameManager>().c3 = false;
                        break;
                    case 3:
                        GameObject.Find("GameManager").GetComponent<GameManager>().c4 = false;
                        break;
                }
            }

            GetComponent<SpriteRenderer>().color = thisColour;
        }
    }
}
