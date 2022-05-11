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
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            // FLIP CHARACTER
            if (GameObject.Find("ControllerAimPoint").transform.position.x > GameObject.Find("----PlayerObjectParent----").transform.position.x) WalkRight();
            else if (GameObject.Find("ControllerAimPoint").transform.position.x < GameObject.Find("----PlayerObjectParent----").transform.position.x) WalkLeft();

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
