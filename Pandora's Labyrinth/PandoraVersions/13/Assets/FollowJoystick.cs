using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowJoystick : MonoBehaviour
{ 
    Vector3 targetLocation;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("JoystickInput") != null) GameObject.Find("JoystickInput").GetComponent<TextMesh>().text = "LX: " + Input.GetAxis("LHorizontalCon").ToString() + "      LY :" + Input.GetAxis("LVerticalCon").ToString() + "\nRX: " + Input.GetAxis("RHorizontalCon").ToString() + "      RY :" + Input.GetAxis("RVerticalCon").ToString();
        targetLocation = new Vector3(GameObject.Find("AimingArm").transform.position.x + (Input.GetAxis("RHorizontalCon") * 50), GameObject.Find("AimingArm").transform.position.y + (Input.GetAxis("RVerticalCon") * 50), GameObject.Find("AimingArm").transform.position.z);
        if (!GameObject.Find("DisplayCursor").GetComponent<CursorController>().followMouse)
        {
            if (Input.GetAxis("RHorizontalCon") > 0.5f || Input.GetAxis("RVerticalCon") > 0.5f || Input.GetAxis("RHorizontalCon") < -0.5f || Input.GetAxis("RVerticalCon") < -0.5f) transform.position = Vector3.Lerp(transform.position, targetLocation, 5 * Time.deltaTime);
        }
    }
}
