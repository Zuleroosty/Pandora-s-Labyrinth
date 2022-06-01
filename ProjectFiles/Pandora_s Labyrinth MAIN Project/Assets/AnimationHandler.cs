using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    Animator thisAnimator;

    // Start is called before the first frame update
    void Start()
    {
        thisAnimator = GetComponent<Animator>();
        thisAnimator.ResetTrigger("isMoving");
        thisAnimator.ResetTrigger("walkLeft");
        thisAnimator.ResetTrigger("walkRight");
        thisAnimator.SetTrigger("isIdle");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            // FLIP CHARACTER
            if (GameObject.Find("DisplayCursor").GetComponent<CursorController>().followMouse)
            {
                if (GameObject.Find("DisplayCursor").transform.position.x > transform.position.x - 0.15f) WalkRight();
                else if (GameObject.Find("DisplayCursor").transform.position.x < transform.position.x + 0.15f) WalkLeft();
            }
            else
            {
                if (GameObject.Find("ControllerAimPoint").transform.position.x > transform.position.x + 0.15f) WalkRight();
                else if (GameObject.Find("ControllerAimPoint").transform.position.x < transform.position.x - 0.15f) WalkLeft();
            }

            // SWITCH WALK/IDLE ANIM
            if (GetComponent<PlayerController>().velocity != new Vector3(0, 0, 0))
            {
                thisAnimator.ResetTrigger("isIdle");
                thisAnimator.SetTrigger("isMoving");
            }
            else
            {
                thisAnimator.ResetTrigger("isMoving");
                thisAnimator.SetTrigger("isIdle");
            }
        }
        else
        {
            thisAnimator.ResetTrigger("isIdle");
            thisAnimator.ResetTrigger("isMoving");
            thisAnimator.ResetTrigger("walkLeft");
            thisAnimator.ResetTrigger("walkRight");
        }
    }
    void WalkLeft()
    {
        thisAnimator.ResetTrigger("walkRight");
        thisAnimator.SetTrigger("walkLeft");
    }
    void WalkRight()
    {
        thisAnimator.ResetTrigger("walkLeft");
        thisAnimator.SetTrigger("walkRight");
    }

}
