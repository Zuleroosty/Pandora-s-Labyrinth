using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    GameObject player;
    PlayerController playerCon;
    float joyConOffSet;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("----PlayerObjectParent----");
        playerCon = player.GetComponent<PlayerController>();
        joyConOffSet = 0.6f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCon == null)
        {
            player = GameObject.Find("----PlayerObjectParent----");
            playerCon = player.GetComponent<PlayerController>();
        }
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            Movement();
            Interactions();
            Combat();
        }
    }
    private void Movement()
    {
        if (Input.GetAxis("LHorizontalKey") != 0 || Input.GetAxis("LVerticalKey") != 0) // KEYBOARD
        {
            playerCon.InputMovement(Input.GetAxis("LHorizontalKey") * 0.8f, Input.GetAxis("LVerticalKey") * 0.8f);
            playerCon.isMoving = true;
        }
        else if (Input.GetAxis("LHorizontalCon") != 0 || Input.GetAxis("LVerticalCon") != 0) // CONTROLLER
        {
            playerCon.InputMovement(Input.GetAxis("LHorizontalCon") * joyConOffSet, Input.GetAxis("LVerticalCon") * joyConOffSet);
            playerCon.isMoving = true;
        }
        else playerCon.isMoving = false;
        if (Input.GetButton("UseBoostGlobal")) playerCon.UseBoost();
        
    }
    private void Interactions()
    {
        if (Input.GetButtonDown("UsePotionGlobal")) playerCon.UsePotion();
        if (Input.GetButtonDown("DropBombGlobal")) playerCon.DropBomb();
    }
    private void Combat()
    {
        if (Input.GetButton("ShootSingleGlobal")) playerCon.SingleShot();
        if (Input.GetButtonDown("ShootTripleGlobal")) playerCon.TripleShot();
    }
}
