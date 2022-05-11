using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public int camSensX, camSensY;

    public Vector3 roomCentre, playerCentre, positionUpdate;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        camSensX = 15;
        camSensY = 15;
    }

    // Update is called once per frame
    void Update()
    {
        playerCentre = player.transform.position;

        positionUpdate.x = roomCentre.x + ((playerCentre.x - roomCentre.x) / 100) * camSensX;
        positionUpdate.y = roomCentre.y + ((playerCentre.y - roomCentre.y) / 100) * camSensY;
        transform.position = new Vector3(positionUpdate.x, positionUpdate.y, -10);
    }
}
