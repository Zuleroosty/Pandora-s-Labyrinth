using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public float averagePlayerLevel;
    public int currentPlayLevel;
    public int playerArmourLvl, playerSpearLvl;
    public int levelPlayTime, totalPlayTime;
    PlayerController playerScript;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find(">GameManager<").GetComponent<GameManager>();
        if (currentPlayLevel < 1) currentPlayLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStats();
        averagePlayerLevel = playerSpearLvl + playerArmourLvl * (1 + (0.125f * playerScript.level));
        averagePlayerLevel /= 2;
        if (GameObject.Find("LevelText") != null) GameObject.Find("LevelText").GetComponent<TextMesh>().text = "Level: " + currentPlayLevel;
    }
    public void NewLevel()
    {
        currentPlayLevel++;
        gameManager.spawnNextLvl = true;
        gameManager.gameState = GameManager.state.Reset;
    }
    public void NewGame()
    {
        currentPlayLevel = 1;
        gameManager.spawnNextLvl = false;
        gameManager.quickStart = true;
        gameManager.gameState = GameManager.state.Reset;
        GetComponent<StatHandler>().ResetStats();
    }
    void UpdateStats()
    {
        if (playerScript != null)
        {
            playerSpearLvl = playerScript.currentSpear;
        }
        else playerScript = GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>();
    }
}
