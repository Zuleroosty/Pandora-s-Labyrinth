using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerCamera, colliderPrefab, boatPrefab;
    public GameObject indicator1, indicator2, indicator3, indicator4;

    public enum state {Menu, Delay, InGame, Win, Lose}
    public state gameState;
    public Vector3 cameraPos;
    public int frameTimer, delaySeconds, gameSeconds, spawnTimer, spawnTimerMax, boatsSaved, boatTimer, boatTimerMax, boatsSpawned, maxBoats, requiredBoats;
    public float playerSpeed, objectOffset;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

        playerCamera = GameObject.Find("Main Camera").gameObject;

        playerSpeed = 20;
        objectOffset = 0;
        maxBoats = 9; // AMOUNT (+2 FOR SPACE AT START AND END OF GAME TIME)
        requiredBoats = 4; // AMOUNT REQUIRED TO WIN GAME
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == state.Menu) // ON MENU
        {
            cameraPos = new Vector3(-20, 0);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                delaySeconds = 3;
                frameTimer = 0;
                gameState = state.Delay;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Application.Quit();
            }
        }

        if (gameState == state.Delay) // DELAY BEFORE PLAY
        {
            cameraPos = new Vector3(0, 12);

            if (delaySeconds > 0)
            {
                GameObject.Find("TitleTextDelay").GetComponent<TextMesh>().text = "Game Starting In...\n" + delaySeconds.ToString();

                if (frameTimer < 60) frameTimer++;
                if (frameTimer >= 60)
                {
                    frameTimer = 0;
                    delaySeconds--;
                }
            }
            else
            {
                gameState = state.InGame;
                gameSeconds = 0;
                spawnTimerMax = Random.Range(20, 46);
                boatTimerMax = ((60 * 12) / 5);
            }
        }

        if (gameState == state.InGame) // DURING PLAY
        {
            cameraPos = new Vector3(0, 0);
            GameObject.Find("GameTimer").GetComponent<TextMesh>().text = (12 - gameSeconds).ToString();
            UpdateIndicators();

            if (gameSeconds < 12)
            {
                if (boatsSpawned < maxBoats) // WIN CONDITION - 3 BOATS SPAWN AT EQUAL TIMES - IF PLAYER GETS ALL THEN GAME WIN ELSE GAME LOSE
                {
                    if (boatTimer < boatTimerMax) boatTimer++;
                    if (boatTimer >= boatTimerMax)
                    {
                        boatTimer = 0;
                        boatTimerMax = ((60 * 12) / (maxBoats + 2));
                        Instantiate(boatPrefab, new Vector3(30, 0, 0), Quaternion.identity);
                    }
                }
                if (boatsSaved >= requiredBoats) gameState = state.Win;

                if (spawnTimer < spawnTimerMax) spawnTimer++; // RANDOMLY SPAWN COLLIDER OBJECTS ABOVE VIEWPORT
                if (spawnTimer >= spawnTimerMax)
                {
                    spawnTimer = 0;
                    spawnTimerMax = Random.Range(20, 46);
                    Instantiate(colliderPrefab, new Vector3(30, 0, 0), Quaternion.identity);
                }

                if (frameTimer < 60) frameTimer++; // GAME TIMER
                if (frameTimer >= 60)
                {
                    frameTimer = 0;
                    gameSeconds++;
                }
            }
            else gameState = state.Lose;
        }

        if (gameState == state.Win) // PLAYER WINS
        {
            cameraPos = new Vector3(0, -12);
            if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene(0);
        }

        if (gameState == state.Lose) // PLAYER LOSES
        {
            cameraPos = new Vector3(20, 0);
            if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene(0);
        }

        cameraPos.z = -10f;
        playerCamera.transform.position = cameraPos;
    }
    void UpdateIndicators()
    {
        if (boatsSaved >= 1) indicator1.GetComponent<SpriteRenderer>().color = Color.green;
        else indicator1.GetComponent<SpriteRenderer>().color = Color.red;
        if (boatsSaved >= 2) indicator2.GetComponent<SpriteRenderer>().color = Color.green;
        else indicator2.GetComponent<SpriteRenderer>().color = Color.red;
        if (boatsSaved >= 3) indicator3.GetComponent<SpriteRenderer>().color = Color.green;
        else indicator3.GetComponent<SpriteRenderer>().color = Color.red;
        if (boatsSaved >= 4) indicator4.GetComponent<SpriteRenderer>().color = Color.green;
        else indicator4.GetComponent<SpriteRenderer>().color = Color.red;
    }
}
