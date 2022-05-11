using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenuHandler : MonoBehaviour
{
    public GameObject buttonText, titleText, spiderText, goblinText, scorpionText, minotaurText;
    public GameObject spearsText, bombsText, potionsText, discovText, enteredText, levelsCompText, playtimeText;
    public GameObject lvlText, xpText, healthDisplayText, healthCostText, speedDisplayText, speedCostText;
    GameManager gameManager;
    StatHandler statHandler;
    PlayerController player;

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

                transform.localPosition = new Vector3(0, -1.75f, 1);

                switch (gameManager.gameState)
                {
                    case GameManager.state.Lose:
                        titleText.GetComponent<TextMesh>().text = "YOU DIED";
                        buttonText.GetComponent<TextMesh>().text = "TRY AGAIN";
                        break;
                    case GameManager.state.Win:
                        titleText.GetComponent<TextMesh>().text = "LEVEL SURVIVED";
                        buttonText.GetComponent<TextMesh>().text = "CONTINUE";
                        break;
                    case GameManager.state.Quit:
                        titleText.GetComponent<TextMesh>().text = "YOU COWARD!";
                        buttonText.GetComponent<TextMesh>().text = "NEW GAME";
                        break;
                }

                spiderText.GetComponent<TextMesh>().text = "Spiders: " + statHandler.spidersKilled.ToString();
                goblinText.GetComponent<TextMesh>().text = "Goblins: " + statHandler.goblinsKilled.ToString();
                scorpionText.GetComponent<TextMesh>().text = "Scorpions: " + statHandler.scorpionsKilled.ToString();
                minotaurText.GetComponent<TextMesh>().text = "Minotaurs: " + statHandler.minotaursKilled.ToString();

                spearsText.GetComponent<TextMesh>().text = "Spears Thrown: " + statHandler.spearsThrown.ToString();
                bombsText.GetComponent<TextMesh>().text = "Bombs Dropped: " + statHandler.bombsDropped.ToString();
                potionsText.GetComponent<TextMesh>().text = "Potions Drank: " + statHandler.potionsDrank.ToString();

                discovText.GetComponent<TextMesh>().text = "Rooms Discovered: " + statHandler.roomsDiscovered.ToString();
                enteredText.GetComponent<TextMesh>().text = "Rooms Entered: " + statHandler.roomsEntered.ToString();
                levelsCompText.GetComponent<TextMesh>().text = "Levels Completed: " + statHandler.levelsCompleted.ToString();
                playtimeText.GetComponent<TextMesh>().text = "Total PlayTime: " + statHandler.playtimeString;

                lvlText.GetComponent<TextMesh>().text = "LVL " + statHandler.playerLevel.ToString();
                xpText.GetComponent<TextMesh>().text = "" + statHandler.xpString;

                healthDisplayText.GetComponent<TextMesh>().text = "Max Health : " + ((1 + player.healthIncrease) * 100) + "%";
                speedDisplayText.GetComponent<TextMesh>().text = "Max Speed : " + ((1 + player.speedIncrease) * 100) + "%";
                if (player.level >= player.healthRequiredLevel) healthCostText.GetComponent<TextMesh>().text = "Cost: " + player.healthCost + " Gold";
                else healthCostText.GetComponent<TextMesh>().text = "Min LVL: " + player.healthRequiredLevel.ToString();
                if (player.level >= player.speedRequiredLevel) speedCostText.GetComponent<TextMesh>().text = "Cost: " + player.speedCost + " Gold";
                else speedCostText.GetComponent<TextMesh>().text = "Min LVL: " + player.speedRequiredLevel.ToString();
            }
            else
            {
                transform.localPosition = new Vector3(-30, 0, 0);
            }
        }
    }
}
