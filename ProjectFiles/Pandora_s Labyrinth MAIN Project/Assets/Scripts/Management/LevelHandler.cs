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
            switch (playerScript.currentArmour)
            {
                case PlayerController.armour.lvl0:
                    playerArmourLvl = 0;
                    break;
                case PlayerController.armour.lvl1:
                    playerArmourLvl = 1;
                    break;
                case PlayerController.armour.lvl2:
                    playerArmourLvl = 2;
                    break;
                case PlayerController.armour.lvl3:
                    playerArmourLvl = 3;
                    break;
                case PlayerController.armour.lvl4:
                    playerArmourLvl = 4;
                    break;
            }
            switch (playerScript.currentSpear)
            {
                case PlayerController.spear.lvl0:
                    playerSpearLvl = 0;
                    break;
                case PlayerController.spear.lvl1:
                    playerSpearLvl = 1;
                    break;
                case PlayerController.spear.lvl2:
                    playerSpearLvl = 2;
                    break;
                case PlayerController.spear.lvl3:
                    playerSpearLvl = 3;
                    break;
                case PlayerController.spear.lvl4:
                    playerSpearLvl = 4;
                    break;
            }
        }
        else playerScript = GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>();
    }
}
