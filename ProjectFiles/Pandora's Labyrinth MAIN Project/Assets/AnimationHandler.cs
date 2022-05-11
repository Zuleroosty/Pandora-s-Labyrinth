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
        thisAnimator.ResetTrigger("isIdle");
        thisAnimator.ResetTrigger("isMoving");
        thisAnimator.ResetTrigger("walkLeft");
        thisAnimator.ResetTrigger("walkRight");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            // FLIP CHARACTER
            if (GameObject.Find("ControllerAimPoint").transform.position.x > GameObject.Find("----PlayerObjectParent----").transform.position.x + 0.05f) WalkRight();
            else if (GameObject.Find("ControllerAimPoint").transform.position.x < GameObject.Find("----PlayerObjectParent----").transform.position.x - 0.05f) WalkLeft();

            // SWITCH WALK/IDLE ANIM
            if (GetComponent<PlayerController>().velocity.x != 0.000f || GetComponent<PlayerController>().velocity.y != 0.000f)
            {
                thisAnimator.SetTrigger("isMoving");
                thisAnimator.ResetTrigger("isIdle");
            }
            else
            {
                thisAnimator.SetTrigger("isIdle");
                thisAnimator.ResetTrigger("isMoving");
            }
        }
    }
    void WalkLeft()
    {
        thisAnimator.SetTrigger("walkLeft");
        thisAnimator.ResetTrigger("walkRight");
    }
    void WalkRight()
    {
        thisAnimator.SetTrigger("walkRight");
        thisAnimator.ResetTrigger("walkLeft");
    }

}
