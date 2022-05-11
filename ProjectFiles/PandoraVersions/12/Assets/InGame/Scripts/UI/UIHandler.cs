using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    GameObject pauseMenu, endMenu;
    GameManager gameManager;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find(">GameManager<").GetComponent<GameManager>();
        playerController = GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>();
        pauseMenu = GameObject.Find(">>----PauseMenu----<<");
        endMenu = GameObject.Find(">>----EndMenu----<<");
    }

    // Update is called once per frame
    void Update()
    {
        // PAUSE MENU
        if (gameManager.isGamePaused) // ON PAUSE
        {
            if (pauseMenu.gameObject.activeInHierarchy == false) pauseMenu.gameObject.SetActive(true);
        }
        else // ON RESUME
        {
            if (pauseMenu.gameObject.activeInHierarchy == true) pauseMenu.gameObject.SetActive(false);
        }

        // END MENU
        if (gameManager.gameState == GameManager.state.Lose || gameManager.gameState == GameManager.state.Win || gameManager.gameState == GameManager.state.Quit) // ON SHOW
        {
            if (endMenu.gameObject.activeInHierarchy == false) endMenu.gameObject.SetActive(true);
        }
        else // ON HIDE
        {
            if (endMenu.gameObject.activeInHierarchy == true) endMenu.gameObject.SetActive(false);
        }

        // UPDATE VISIBLE PLAYER STATS
        if (gameManager.gameState == GameManager.state.InGame && playerController != null)
        {
            GameObject.Find("LvlText").GetComponent<TextMesh>().text = playerController.level.ToString();
        }
    }
}
