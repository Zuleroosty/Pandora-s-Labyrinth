using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startButton, quitButton, resetButtonLose, resetButtonWin, squ1, squ2, squ3, squ4;
    public bool puzzleComplete, c1, c2, c3, c4;
    public int frames, seconds, startSeconds, cc1, cc2, cc3, cc4;
    public enum state { Menu, Primer, InGame, Win, Lose }
    public state gameState;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

        gameState = state.Menu;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == state.Primer)
        {
            GameObject.Find("Main Camera").transform.position = new Vector3(0, 12, -10);

            if (startSeconds >= 0)
            {
                GameObject.Find("StartTitle").GetComponent<TextMesh>().text = "Game Starting In...\n" + startSeconds.ToString();
                if (frames < 60) frames++;
                if (frames >= 60)
                {
                    startSeconds--;
                    frames = 0;
                }
            }
            else gameState = state.InGame;
        }
        if (gameState == state.InGame)
        {
            if (frames < 60) frames++;
            if (frames >= 60)
            {
                frames = 0;
                seconds++;
            }
            if (seconds > 12 && !puzzleComplete) gameState = state.Lose;
            else if (puzzleComplete) gameState = state.Win;

            GameObject.Find("Main Camera").transform.position = new Vector3(0, 0, -10);
            GameObject.Find("Timer").GetComponent<TextMesh>().text = (12 - seconds).ToString();

            if (c1 && c2 && c3 && c4) puzzleComplete = true;

            if (c1) squ1.GetComponent<SpriteRenderer>().color = Color.green;
            else squ1.GetComponent<SpriteRenderer>().color = Color.red;
            if (c2) squ2.GetComponent<SpriteRenderer>().color = Color.green;
            else squ2.GetComponent<SpriteRenderer>().color = Color.red;
            if (c3) squ3.GetComponent<SpriteRenderer>().color = Color.green;
            else squ3.GetComponent<SpriteRenderer>().color = Color.red;
            if (c4) squ4.GetComponent<SpriteRenderer>().color = Color.green;
            else squ4.GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (gameState == state.Menu)
        {
            GameObject.Find("Main Camera").transform.position = new Vector3(-20, 0, -10);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameState = state.Primer;
                frames = 0;
                seconds = 0;
                startSeconds = 3;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Application.Quit();
            }
        }
        if (gameState == state.Win)
        {
            GameObject.Find("Main Camera").transform.position = new Vector3(0, -12, -10);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
        if (gameState == state.Lose)
        {
            GameObject.Find("Main Camera").transform.position = new Vector3(20, 0, -10);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
