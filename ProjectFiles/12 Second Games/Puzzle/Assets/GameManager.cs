using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startButton, quitButton, resetButtonLose, resetButtonWin, squ1, squ2, squ3, squ4, block1, block2, block3, block4;
    public bool puzzleComplete, b1, b2, b3, b4;
    public int frames, seconds, startSeconds, n1, n2, n3, n4, randNum, delay;
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
        if (n1 == n2 && n2 == n3 && n3 == n4)
        {
            randNum = Random.Range(0, 31);
            if (randNum > 15) n1 = Random.Range(5, 10);
            else n1 = Random.Range(1, 4);
            if (randNum > 15) n2 = Random.Range(1, 4);
            else n2 = Random.Range(5, 10);
            if (randNum > 15) n3 = Random.Range(5, 10);
            else n3 = Random.Range(1, 4);
            if (randNum > 15) n4 = Random.Range(1, 4);
            else n4 = Random.Range(5, 10); 
        }
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

            if (b1 && b2 && b3 && b4) puzzleComplete = true;

            if (block1.GetComponent<BlockHandler>().currentNumber == n1)
            {
                b1 = true;
                squ1.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                b1 = false;
                squ1.GetComponent<SpriteRenderer>().color = Color.red;
            }
            if (block2.GetComponent<BlockHandler>().currentNumber == n2)
            {
                b2 = true;
                squ2.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                b2 = false;
                squ2.GetComponent<SpriteRenderer>().color = Color.red;
            }
            if (block3.GetComponent<BlockHandler>().currentNumber == n3)
            {
                b3 = true;
                squ3.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                b3 = false;
                squ3.GetComponent<SpriteRenderer>().color = Color.red;
            }
            if (block4.GetComponent<BlockHandler>().currentNumber == n4)
            {
                b4 = true;
                squ4.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                b4 = false;
                squ4.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        if (gameState == state.Menu)
        {
            GameObject.Find("Main Camera").transform.position = new Vector3(-20, 0, -10);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                gameState = state.Primer;
                frames = 0;
                seconds = 0;
                startSeconds = 3;
            }
        }
        if (gameState == state.Win)
        {
            GameObject.Find("Main Camera").transform.position = new Vector3(0, -12, -10);
            if (delay < 60) delay++;
            if (delay >= 60 && Input.GetKeyDown(KeyCode.Mouse0)) SceneManager.LoadScene(0);
        }
        if (gameState == state.Lose)
        {
            GameObject.Find("Main Camera").transform.position = new Vector3(20, 0, -10);
            if (delay < 60) delay++;
            if (delay >= 60 && Input.GetKeyDown(KeyCode.Mouse0)) SceneManager.LoadScene(0);
        }
    }
}
