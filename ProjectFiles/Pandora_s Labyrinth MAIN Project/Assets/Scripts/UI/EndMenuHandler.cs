using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenuHandler : MonoBehaviour
{
    public GameObject buttonText, titleText, spiderText, goblinText, scorpionText, minotaurText;
    public GameObject spearsText, bombsText, potionsText, discovText, enteredText, levelsCompText, playtimeText;
    public GameObject lvlText, xpText, healthDisplayText, healthCostText, speedDisplayText, speedCostText, damageDealtText, damageTakenText;
    GameManager gameManager;
    StatHandler statHandler;
    PlayerController player;
    bool updateText, updateSaveFile;

    private void Start()
    {
        gameManager = GameObject.Find(">GameManager<").GetComponent<GameManager>();
        statHandler = gameManager.GetComponent<StatHandler>();
        player = GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) player = GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>();
        else
        {
            if (gameManager.gameState == GameManager.state.Win || gameManager.gameState == GameManager.state.Lose || gameManager.gameState == GameManager.state.Quit)
            {
                GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = 7f;
                transform.localPosition = new Vector3(0, 0, 1.5f);

                
                // SAVE GAME & PLAYER STATS
                if (!updateSaveFile)
                {
                    switch (gameManager.gameState)
                    {
                        case GameManager.state.Win:
                            GameObject.Find(">GameManager<").GetComponent<StatHandler>().levelsCompleted++;
                            gameManager.GetComponent<SaveGameHandler>().SaveInfoToFile();
                            break;
                        case GameManager.state.Lose:
                            PlayerPrefs.DeleteAll();
                            break;
                        case GameManager.state.Quit:
                            PlayerPrefs.DeleteAll();
                            break;
                    }
                    updateSaveFile = true;
                }

                // UPDATE TEXT BASED ON END CONDITION
                if (!updateText)
                {
                    switch (gameManager.gameState)
                    {
                        case GameManager.state.Lose:
                            titleText.GetComponent<TextMesh>().text = "YOU DIED..";
                            buttonText.GetComponent<TextMesh>().text = "TRY AGAIN";
                            break;
                        case GameManager.state.Win:
                            titleText.GetComponent<TextMesh>().text = "LEVEL SURVIVED!";
                            buttonText.GetComponent<TextMesh>().text = "CONTINUE";
                            break;
                        case GameManager.state.Quit:
                            titleText.GetComponent<TextMesh>().text = "YOU COWARD!..";
                            buttonText.GetComponent<TextMesh>().text = "NEW GAME";
                            break;
                    }
                    updateText = true;
                }

                // MONSTERS KILLED
                spiderText.GetComponent<TextMesh>().text = "Spiders: " + statHandler.spidersKilled.ToString("0");
                goblinText.GetComponent<TextMesh>().text = "Goblins: " + statHandler.goblinsKilled.ToString("0");
                scorpionText.GetComponent<TextMesh>().text = "Scorpions: " + statHandler.scorpionsKilled.ToString("0");
                minotaurText.GetComponent<TextMesh>().text = "Minotaurs: " + statHandler.minotaursKilled.ToString("0");

                // PLAYER STATISTICS
                spearsText.GetComponent<TextMesh>().text = statHandler.spearsThrown.ToString("0") + " Spears Thrown";
                bombsText.GetComponent<TextMesh>().text = statHandler.bombsDropped.ToString("0") + " Bombs Dropped";
                potionsText.GetComponent<TextMesh>().text = statHandler.potionsDrank.ToString("0") + " Potions Drank";

                // DAMAGE STATS
                damageDealtText.GetComponent<TextMesh>().text = "Damage Dealt: " + statHandler.damageDealt.ToString("0");
                damageTakenText.GetComponent<TextMesh>().text = "Damage Taken: " + statHandler.damageTaken.ToString("0");

                // LEVEL STATS
                discovText.GetComponent<TextMesh>().text = statHandler.roomsDiscovered.ToString("0") + " Rooms Discovered";
                enteredText.GetComponent<TextMesh>().text = statHandler.roomsEntered.ToString("0") + " Rooms Entered";
                levelsCompText.GetComponent<TextMesh>().text = statHandler.levelsCompleted.ToString("0") + " Levels Completed";
                playtimeText.GetComponent<TextMesh>().text = statHandler.playtimeString + " Total PlayTime";

                // PLAYER CURRENT LEVEL/XP
                lvlText.GetComponent<TextMesh>().text = "LVL " + statHandler.playerLevel.ToString("0");
                xpText.GetComponent<TextMesh>().text = "" + statHandler.xpString;

                // DISPLAY CURRENT UPGRADABLE STATS WITH INCREASE AMOUNT
                healthDisplayText.GetComponent<TextMesh>().text = "Max Health : " + ((1 + player.healthIncrease) * 100) + "%";
                speedDisplayText.GetComponent<TextMesh>().text = "Max Speed : " + ((1 + player.speedIncrease) * 100) + "%";

                // DISPLAY AVAILABLE UPGRADES OR REQUIREMENTS
                if (player.level >= player.healthRequiredLevel) healthCostText.GetComponent<TextMesh>().text = "Cost: " + player.healthCost + " Gold";
                else healthCostText.GetComponent<TextMesh>().text = "Min LVL: " + player.healthRequiredLevel.ToString("0");
                if (player.level >= player.speedRequiredLevel) speedCostText.GetComponent<TextMesh>().text = "Cost: " + player.speedCost + " Gold";
                else speedCostText.GetComponent<TextMesh>().text = "Min LVL: " + player.speedRequiredLevel.ToString("0");

                // DISPLAY PLAYER SCORE
                GameObject.Find("ScoreText").GetComponent<TextMesh>().text = statHandler.playerScore.ToString();
            }
            else
            {
                transform.localPosition = new Vector3(-30, 0, 0);
                updateText = false;
                updateSaveFile = false;
            }
        }
    }
}
