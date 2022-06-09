using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public float averagePlayerLevel;
    public int currentPlayLevel;
    PlayerController playerScript;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find(">GameManager<").GetComponent<GameManager>();
        playerScript = GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>();
        if (currentPlayLevel < 1) currentPlayLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameState == GameManager.state.InGame)
        {
            averagePlayerLevel = (0.125f * playerScript.level) + (1 + (0.125f * playerScript.currentSpear));
            averagePlayerLevel /= 2;

            GameObject.Find("LvlText").GetComponent<TextMesh>().text = currentPlayLevel.ToString();
        }
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
        gameManager.playerObject.GetComponent<PlayerController>().ResetPlayerStats();
        gameManager.GetComponent<StatHandler>().ResetStats();
        gameManager.roomLevel = 0;
        gameManager.gameState = GameManager.state.Reset;
    }
}
