using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startButton, quitButton, resetButtonLose, resetButtonWin;
    public int compRequired, compRecieved, c1, c2, c3, c4, generatedColours, selectedColour, coloursTaken, frames, seconds;
    public bool puzzleComplete, c1Assigned, c2Assigned, c3Assigned, c4Assigned;
    public enum state { Menu, InGame, Win, Lose}
    public state gameState;

    // Start is called before the first frame update
    void Start()
    {
        compRequired = 4;
        gameState = state.Menu;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == state.Menu)
        {
            GameObject.Find("Main Camera").transform.position = new Vector3(-20, 0, -10);
            if (GameObject.Find("MouseCursor").GetComponent<SpriteRenderer>().bounds.Intersects(startButton.GetComponent<SpriteRenderer>().bounds) && Input.GetKeyDown(KeyCode.Mouse0))
            {
                gameState = state.InGame;
                frames = 0;
                seconds = 0;
            }
            if (GameObject.Find("MouseCursor").GetComponent<SpriteRenderer>().bounds.Intersects(quitButton.GetComponent<SpriteRenderer>().bounds) && Input.GetKeyDown(KeyCode.Mouse0))
            {
                Application.Quit();
            }
        }
        if (gameState == state.Win)
        {
            GameObject.Find("Main Camera").transform.position = new Vector3(0, -12, -10);
            if (GameObject.Find("MouseCursor").GetComponent<SpriteRenderer>().bounds.Intersects(resetButtonWin.GetComponent<SpriteRenderer>().bounds) && Input.GetKeyDown(KeyCode.Mouse0))
            {
                SceneManager.LoadScene(0);
            }
        }
        if (gameState == state.Lose)
        {
            GameObject.Find("Main Camera").transform.position = new Vector3(20, 0, -10);
            if (GameObject.Find("MouseCursor").GetComponent<SpriteRenderer>().bounds.Intersects(resetButtonLose.GetComponent<SpriteRenderer>().bounds) && Input.GetKeyDown(KeyCode.Mouse0))
            {
                SceneManager.LoadScene(0);
            }
        }
        if (gameState == state.InGame)
        {
            GameObject.Find("Main Camera").transform.position = new Vector3(0, 0, -10);
            GameObject.Find("Timer").GetComponent<TextMesh>().text = (12 - seconds).ToString();

            if (frames < 60) frames++;
            if (frames >= 60)
            {
                frames = 0;
                seconds++;
            }
            if (seconds > 12 && !puzzleComplete) gameState = state.Lose;
            else if (puzzleComplete) gameState = state.Win;

            // Generate Puzzle
            if (generatedColours < 4)
            {
                selectedColour = Random.Range(1, 6);
                switch (generatedColours)
                {
                    case 0:
                        if (selectedColour != c1 && selectedColour != c2 && selectedColour != c3 && selectedColour != c4)
                        {
                            c1 = selectedColour;
                            generatedColours++;
                        }
                        break;
                    case 1:
                        if (selectedColour != c1 && selectedColour != c2 && selectedColour != c3 && selectedColour != c4)
                        {
                            c2 = selectedColour;
                            generatedColours++;
                        }
                        break;
                    case 2:
                        if (selectedColour != c1 && selectedColour != c2 && selectedColour != c3 && selectedColour != c4)
                        {
                            c3 = selectedColour;
                            generatedColours++;
                        }
                        break;
                    case 3:
                        if (selectedColour != c1 && selectedColour != c2 && selectedColour != c3 && selectedColour != c4)
                        {
                            c4 = selectedColour;
                            generatedColours++;
                        }
                        break;
                }
            }
            else
            {
                if (compRecieved >= compRequired)
                {
                    puzzleComplete = true;
                }
            }
        }
    }
}
