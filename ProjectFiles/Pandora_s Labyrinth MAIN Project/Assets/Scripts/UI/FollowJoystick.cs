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
        if (GameObject.Find("JoystickInput") != null) GameObject.Find("JoystickInput").GetComponent<TextMesh>().text = "LX: " + Input.GetAxisRaw("LHorizontalCon").ToString() + "      LY :" + Input.GetAxisRaw("LVerticalCon").ToString() + "\nRX: " + Input.GetAxisRaw("RHorizontalCon").ToString() + "      RY :" + Input.GetAxisRaw("RVerticalCon").ToString();
        targetLocation = new Vector3(GameObject.Find("AimingArm").transform.position.x + (Input.GetAxisRaw("RHorizontalCon") * 50), GameObject.Find("AimingArm").transform.position.y + (Input.GetAxisRaw("RVerticalCon") * 50), GameObject.Find("AimingArm").transform.position.z);
        if (!GameObject.Find("DisplayCursor").GetComponent<CursorController>().followMouse)
        {
            if (Input.GetAxisRaw("RHorizontalCon") > 0.5f || Input.GetAxisRaw("RVerticalCon") > 0.5f || Input.GetAxisRaw("RHorizontalCon") < -0.5f || Input.GetAxisRaw("RVerticalCon") < -0.5f) transform.position = Vector3.Lerp(transform.position, targetLocation, 5 * Time.deltaTime);
        }
    }
}
