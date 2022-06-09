using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float expectedX, expectedY, xSpeed, ySpeed;
    Vector3 expectedPos;
    private void Start()
    {
        xSpeed = 0.115f;
        ySpeed = 0.04f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) && expectedX < 7.75f) expectedX += xSpeed;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) && expectedX > -7.75f) expectedX -= xSpeed;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && expectedY < -1f) expectedY += ySpeed * 1.15f;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) && expectedY > -3.5f) expectedY -= ySpeed;
        }
        else ResetPosition();

        expectedPos.x = expectedX;
        expectedPos.y = expectedY;
        expectedPos.z = 0;
        transform.position = Vector3.Lerp(transform.position, expectedPos, GameObject.Find("GameManager").GetComponent<GameManager>().playerSpeed * Time.deltaTime);
    }
    public void ResetPosition()
    {
        transform.position = new Vector3(0, -2, 0);
        expectedPos = transform.position;
        expectedX = expectedPos.x;
        expectedY = expectedPos.y;
    }
}
