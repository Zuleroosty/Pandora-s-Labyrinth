using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTo : MonoBehaviour
{
    public GameObject target;
    public Vector3 targetPos;
    float angle, targetAngle;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (target != null) targetPos = target.transform.position;
            angle = Mathf.Atan2(targetPos.y - transform.position.y, targetPos.x - transform.position.x) * Mathf.Rad2Deg;
            if (!name.Contains("AimingArm")) transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            else
            {
                if (GameObject.Find("DisplayCursor").GetComponent<CursorController>().followMouse)
                {
                    target = GameObject.Find("DisplayCursor");
                    transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));
                }
                else
                {
                    target = GameObject.Find("ControllerAimBox");
                    transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle + 8));
                }
            }
        }
    }
}
