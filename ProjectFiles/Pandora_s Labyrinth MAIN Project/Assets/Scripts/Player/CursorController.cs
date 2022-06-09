using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Sprite mouse, controller;
    public Vector3 mouseWorldPosition, memPos, targetPos;
    public bool followMouse;
    float targetX, targetY, angle;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 10;
        followMouse = true;
        transform.localPosition = new Vector3(0, 0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.visible) Cursor.visible = false;
        memPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
        mouseWorldPosition = transform.position;


        if (Input.GetKey(KeyCode.Mouse0)) followMouse = true;
        else
        {
            if (Input.GetAxis("RHorizontalCon") != 0 || Input.GetAxis("RVerticalCon") != 0) followMouse = false;
            else if (Input.GetAxis("LHorizontalCon") != 0 || Input.GetAxis("LVerticalCon") != 0) followMouse = false;
        }


        if (followMouse)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = mouse;
        }
        else
        {
            if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
            {
                transform.position = new Vector3(GameObject.Find("ControllerAimPoint").transform.position.x, GameObject.Find("ControllerAimPoint").transform.position.y, 10);
                targetPos = GameObject.Find("SpearSprite").transform.position;

                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = controller;

                // ROTATE AWAY FROM PLAYER OBJECT
                angle = Mathf.Atan2(targetPos.y - transform.position.y, targetPos.x - transform.position.x) * Mathf.Rad2Deg;
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));
            }
            else
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = mouse;
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                if (Input.GetAxis("LHorizontalCon") != 0 || Input.GetAxis("LVerticalCon") != 0)
                {
                    targetPos = new Vector3(0, 0, 10);

                    if (Input.GetAxis("LHorizontalCon") > 0 && transform.localPosition.x < 18) targetPos = new Vector3(Input.GetAxis("LHorizontalCon") * 0.35f, targetPos.y, 10);
                    if (Input.GetAxis("LHorizontalCon") < 0 && transform.localPosition.x > -18) targetPos = new Vector3(Input.GetAxis("LHorizontalCon") * 0.35f, targetPos.y, 10);
                    if (Input.GetAxis("LVerticalCon") > 0 && transform.localPosition.y < 10f) targetPos = new Vector3(targetPos.x, Input.GetAxis("LVerticalCon") * 0.35f, 10);
                    if (Input.GetAxis("LVerticalCon") < 0 && transform.localPosition.y > -10f) targetPos = new Vector3(targetPos.x, Input.GetAxis("LVerticalCon") * 0.35f, 10);

                    targetPos = transform.localPosition + targetPos;
                    targetPos.z = 10;
                    transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, 20 * Time.deltaTime);
                }
            }
        }
    }
}
