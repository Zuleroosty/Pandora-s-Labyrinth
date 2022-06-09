using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XboxPadEXAMPLE : MonoBehaviour
{
    TextMesh refToGeneral;//Communication which Key is pressed
    TextMesh refToLeftThumb, refToRightThumb, refToLT, refToRT, refToDPAD, refToTriggers;//TextMeshes to communicate internal variable
    Transform refToPlayer, refToCircle;//references to player and circles
    float speed;//player speed
    int delayCount;//input delay to visualise pressed and held (otherwise display will only display held)
    bool delayState;//state that counts for delay
    bool xboxRotation, mouseCursor;//do we use Xbox Pad and do we have in-game cursor
    //Simulation variables can be removed if not used/planned
    bool delaySimulation;
    int simulationCount;
    //extra feature for testing purposes
    int refresh;
    string refreshMe;
    void Start()
    {
        speed = 0.05f;
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        refToPlayer = GameObject.Find("Player").transform;
        refToCircle = GameObject.Find("Circle").transform;
        refToGeneral = GameObject.Find("General").GetComponent<TextMesh>();
        refToLeftThumb = GameObject.Find("Left Thumb").GetComponent<TextMesh>();
        refToRightThumb = GameObject.Find("Right Thumb").GetComponent<TextMesh>();
        refToLT = GameObject.Find("LT").GetComponent<TextMesh>();
        refToRT = GameObject.Find("RT").GetComponent<TextMesh>();
        refToDPAD = GameObject.Find("DPAD").GetComponent<TextMesh>();
        refToTriggers = GameObject.Find("Triggers").GetComponent<TextMesh>();

    }

    void Update()
    {
        /*
         * For most basic Input on the gamepad you can use basic Input.GetKey(Down/Up) and KeyCode for each Joystick
         * Do note that after Joystick you have to either not define value (any) or do starting with 1 (first 1) to know which pad is being used
         * Despite being Valid Buttons 10 - 19 they are not present on Xbox pad but 10 and 11 works as Triggers on Nintendo Switch
         */
        //Pressed
        if (Input.GetKeyDown(KeyCode.Joystick1Button0)) refToGeneral.text = "A is pressed";
        if (Input.GetKeyDown(KeyCode.Joystick1Button1)) refToGeneral.text = "B is pressed";
        if (Input.GetKeyDown(KeyCode.Joystick1Button2)) refToGeneral.text = "X is pressed";
        if (Input.GetKeyDown(KeyCode.Joystick1Button3)) refToGeneral.text = "Y is pressed";
        if (Input.GetKeyDown(KeyCode.Joystick1Button4)) refToGeneral.text = "Left Bumper is pressed";
        if (Input.GetKeyDown(KeyCode.Joystick1Button5)) refToGeneral.text = "Right Bumper is pressed";
        if (Input.GetKeyDown(KeyCode.Joystick1Button6)) refToGeneral.text = "Back is pressed";
        if (Input.GetKeyDown(KeyCode.Joystick1Button7)) refToGeneral.text = "Start is pressed";
        if (Input.GetKeyDown(KeyCode.Joystick1Button8)) refToGeneral.text = "Left Stick is pressed";
        if (Input.GetKeyDown(KeyCode.Joystick1Button9)) refToGeneral.text = "Right Stick is pressed";
        //Those two shortcuts work only on Switch not on Xbox pad
        if (Input.GetKeyDown(KeyCode.Joystick1Button10)) refToGeneral.text = "Left Trigger is pressed";
        if (Input.GetKeyDown(KeyCode.Joystick1Button11)) refToGeneral.text = "Right Trigger is pressed";


        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.Joystick1Button6) || Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Joystick1Button8) || Input.GetKeyDown(KeyCode.Joystick1Button9) || Input.GetKeyDown(KeyCode.Joystick1Button10) || Input.GetKeyDown(KeyCode.Joystick1Button11))
        {//if you press any of the buttons down, activate delay state
            delayState = true;
        }

        //In delay state we just count frames, to show difference in display between Pressed and Held, it is not requierd in your projects.
        if (delayState) delayCount++;
        if (delayCount > 15)
        {
            delayCount = 0;
            delayState = false;
            if (Input.GetKey(KeyCode.Joystick1Button0)) refToGeneral.text = "A is held";
            if (Input.GetKey(KeyCode.Joystick1Button1)) refToGeneral.text = "B is held";
            if (Input.GetKey(KeyCode.Joystick1Button2)) refToGeneral.text = "X is held";
            if (Input.GetKey(KeyCode.Joystick1Button3)) refToGeneral.text = "Y is held";
            if (Input.GetKey(KeyCode.Joystick1Button4)) refToGeneral.text = "Left Bumper is held";
            if (Input.GetKey(KeyCode.Joystick1Button5)) refToGeneral.text = "Right Bumper is held";
            if (Input.GetKey(KeyCode.Joystick1Button6)) refToGeneral.text = "Back is held";
            if (Input.GetKey(KeyCode.Joystick1Button7)) refToGeneral.text = "Start is held";
            if (Input.GetKey(KeyCode.Joystick1Button8)) refToGeneral.text = "Left Stick is held";
            if (Input.GetKey(KeyCode.Joystick1Button9)) refToGeneral.text = "Right Stick is held";
            //Those two shortcuts work only on Switch not on Xbox pad
            if (Input.GetKey(KeyCode.Joystick1Button10)) refToGeneral.text = "Left Trigger is held";
            if (Input.GetKey(KeyCode.Joystick1Button11)) refToGeneral.text = "Right Trigger is held";
        }
        //Released
        if (Input.GetKeyUp(KeyCode.Joystick1Button0)) refToGeneral.text = "A is released";
        if (Input.GetKeyUp(KeyCode.Joystick1Button1)) refToGeneral.text = "B is released";
        if (Input.GetKeyUp(KeyCode.Joystick1Button2)) refToGeneral.text = "X is released";
        if (Input.GetKeyUp(KeyCode.Joystick1Button3)) refToGeneral.text = "Y is released";
        if (Input.GetKeyUp(KeyCode.Joystick1Button4)) refToGeneral.text = "Left Bumper is released";
        if (Input.GetKeyUp(KeyCode.Joystick1Button5)) refToGeneral.text = "Right Bumper is released";
        if (Input.GetKeyUp(KeyCode.Joystick1Button6)) refToGeneral.text = "Back is released";
        if (Input.GetKeyUp(KeyCode.Joystick1Button7)) refToGeneral.text = "Start is released";
        if (Input.GetKeyUp(KeyCode.Joystick1Button8)) refToGeneral.text = "Left Stick is released";
        if (Input.GetKeyUp(KeyCode.Joystick1Button9)) refToGeneral.text = "Right Stick is released";
        //Those two shortcuts work only on Switch not on Xbox pad
        if (Input.GetKeyUp(KeyCode.Joystick1Button10)) refToGeneral.text = "Left Trigger is released";
        if (Input.GetKeyUp(KeyCode.Joystick1Button11)) refToGeneral.text = "Right Trigger is released";

        /*
         * To be able to create truly multi-input code we will have to access Input Manager, to do so go to Unity and within Menus go through:
         * Edit > Project Settings > Input Manager > Axes
         * You will Start with array of circa 30 inputs like 1. Horizontal, or  3. Fire1, 8. Mouse Y or 16. Submit
         * Each of those is responsible for particular Input type/action and defines default inputs and any positve or negative alternatives
         * For example A and D can be used for move left and right, positive alternative (right) will be right arrow, while negative one (left) will be left arrow
         * 
         * Do note that Submit and Cancel are repeats of A and B but work for different keys on Keyboard (Space/Return and Escape).
         * 
         * To control those Inputs you have to use instead of Input.GetKey(Down/Up) > Input.GetButton(Down/Up) and provide string name of the Input you want to utilise
         */
        
        //This is repeat of Pressed/Held/Released logic presented above
        if (Input.GetButtonDown("Jump")) refToGeneral.text = "Jump is pressed";
        if (Input.GetButtonDown("Submit")) refToGeneral.text = "Submit is pressed";
        if (Input.GetButtonDown("Cancel")) refToGeneral.text = "Cancel is pressed";
        if (Input.GetButtonDown("Fire1")) refToGeneral.text = "Fire1 is pressed";
        if (Input.GetButtonDown("Fire2")) refToGeneral.text = "Fire2 is pressed";
        if (Input.GetButtonDown("Fire3")) refToGeneral.text = "Fire3 is pressed";

        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel") || Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire3")) delaySimulation = true;

        if (delaySimulation) simulationCount++;
        if (simulationCount > 15)
        {
            if (Input.GetButton("Jump")) refToGeneral.text = "Jump is held";
            if (Input.GetButton("Submit")) refToGeneral.text = "Submit is held";
            if (Input.GetButton("Cancel")) refToGeneral.text = "Cancel is held";
            if (Input.GetButton("Fire1")) refToGeneral.text = "Fire1 is held";
            if (Input.GetButton("Fire2")) refToGeneral.text = "Fire2 is held";
            if (Input.GetButton("Fire3")) refToGeneral.text = "Fire3 is held";
            simulationCount = 0;
            delaySimulation = false;
        }

        if (Input.GetButtonUp("Jump")) refToGeneral.text = "Jump is released";
        if (Input.GetButtonUp("Submit")) refToGeneral.text = "Submit is released";
        if (Input.GetButtonUp("Cancel")) refToGeneral.text = "Cancel is released";
        if (Input.GetButtonUp("Fire1")) refToGeneral.text = "Fire1 is released";
        if (Input.GetButtonUp("Fire2")) refToGeneral.text = "Fire2 is released";
        if (Input.GetButtonUp("Fire3")) refToGeneral.text = "Fire3 is released";

        /*
         * Some of the Inputs are not simple On/Off buttons but rather work on a basis of a float (0-1)
         * For those you have to use Input.GetAxis command and include Thumbsticks, as well as Triggers (that have to be manually defined, see next paragraph
         */

        //Horizontal by default covers AD/Left/Right Arrow and Left Thumbstick. It is a float <0 left >0 right
        if (Input.GetAxis("Horizontal") < 0) refToPlayer.position -= new Vector3(speed * -Input.GetAxis("Horizontal"), 0);
        if (Input.GetAxis("Horizontal") > 0) refToPlayer.position += new Vector3(speed * Input.GetAxis("Horizontal"), 0);
        //Vertical by default covers WS/Up/Down Arrow and Left Thumbstick. It is a float <0 down >0 up (It also has to be inverted in Input Manager)
        if (Input.GetAxis("Vertical") < 0) refToPlayer.position -= new Vector3(0, speed * -Input.GetAxis("Vertical"));
        if (Input.GetAxis("Vertical") > 0) refToPlayer.position += new Vector3(0, speed * Input.GetAxis("Vertical"));
        refToLeftThumb.text = "X: " + Input.GetAxis("Horizontal") + "\n Y: " + Input.GetAxis("Vertical");

        /*
         * It is possible to add additional Inputs to the Manager by either repeating current naming convention and just changing Type of Input and Axis/Joy Num
         * But you can also create your own one. In this example following Inputs were added after extending array of Inputs from 30 to 39:
         * For Right Thumbstick:
         * 31. Mouse X
         * 32. Mouse Y
         * For DPAD
         * 33. DPAD Horizontal
         * 34. DPAD Vertical
         * For Triggers
         * 35. Mouse ScrollWheel
         * 36. Left Trigger
         * 37. Right Trigger
         * For detection are we using Pad Thumbsticks or Mouse/Touchscreen
         * 38. XboxHText
         * 39. XboxVTest
         * 
         * Do note that their respective Types of Input, Axis, inversions and values are already setup, and you should recreate them 1 to 1
         */
        
        //This displays X and Y offsets of Right Thumbstick/Mouse
        refToRightThumb.text = "X: " + Input.GetAxis("Mouse X") + "\n Y: " + Input.GetAxis("Mouse Y");

        //D-Pad has to be custom made and set for 6th and 7th axis
        if (Input.GetAxis("DPAD Horizontal") < 0) refToPlayer.position -= new Vector3(speed * -Input.GetAxis("DPAD Horizontal"), 0);
        if (Input.GetAxis("DPAD Horizontal") > 0) refToPlayer.position += new Vector3(speed * Input.GetAxis("DPAD Horizontal"), 0);
        if (Input.GetAxis("DPAD Vertical") < 0) refToPlayer.position -= new Vector3(0, speed * -Input.GetAxis("DPAD Vertical"));
        if (Input.GetAxis("DPAD Vertical") > 0) refToPlayer.position += new Vector3(0, speed * Input.GetAxis("DPAD Vertical"));
        refToDPAD.text = "X: " + Input.GetAxis("DPAD Horizontal") + "\n Y: " + Input.GetAxis("DPAD Vertical");
        //Switch has a simpler Alternative for D-PAD using JoystickButtons 12-15, but they are in reverse order
        if (Input.GetKey(KeyCode.Joystick1Button12)) refToGeneral.text = "D-Pad Down";
        if (Input.GetKey(KeyCode.Joystick1Button13)) refToGeneral.text = "D-Pad Right";
        if (Input.GetKey(KeyCode.Joystick1Button14)) refToGeneral.text = "D-Pad Left";
        if (Input.GetKey(KeyCode.Joystick1Button15)) refToGeneral.text = "D-Pad Up";

        //Triggers are 9th and 10th axis, they are not on-off buttons but floats 0-1
        refToLT.text = "Left Trigger:\n" + Input.GetAxis("Left Trigger");
        refToRT.text = "Right Trigger:\n" + Input.GetAxis("Right Trigger");
        //Both Triggers are matched by Mouse Scroll wheel, and work as 3rd axis > Left Trigger (scroll up) 0 to 1 Right Trigger (scroll down) 0 to -1
        refToTriggers.text = "Both Triggers:\n" + Input.GetAxis("Mouse ScrollWheel");

        /* Right Thumbstick is traditionally used for aiming, thus it is referred to as Mouse - 4th and 5th Axis of Xbox pad
         *  Due to the fact that Thumbstick physical space is restricted (-1 to 1) while mouse is not (any pixel on screen) it is important to detect are we rotating using mouse or keyboard
         *  for which we need to create "virtual pad axis" that is of different naming convention then the one we use for generic Mouse/Pad control
         */

        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {//IF the mouse/right thumbstick is offset in either Horizontal (X) or Vertical (Y) manner
            if (Input.GetAxis("XboxHTest") != 0 || Input.GetAxis("XboxVTest") != 0) xboxRotation = true;//If it is XBOX Test value that is not 0, we know we use Pad
            else xboxRotation = false;//if those values are 0 we are using mouse

            if (mouseCursor)
            {//If mouse cursor is activated, mouse/right thumbstick Moves the circle as if it was a mouse cursor
                if (xboxRotation) refToCircle.position += new Vector3(speed * 10 * Input.GetAxis("Mouse X"), speed * 10 * Input.GetAxis("Mouse Y"));
                else refToCircle.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
            }
            else
            {//if mouse cursor is deactivated, mouse/right thumbstick movement will determine rotation of the object
                if (xboxRotation) refToPlayer.GetChild(0).rotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.Atan2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")) * Mathf.Rad2Deg) - 90));
                else refToPlayer.GetChild(0).rotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.Atan2(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - refToPlayer.position.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).x - refToPlayer.position.x) * Mathf.Rad2Deg) - 90));
            }
        }
        //We use scroll/triggers to either activate or deactivate the cursor control
        if (Input.GetAxis("Mouse ScrollWheel") > 0) mouseCursor = false;
        if (Input.GetAxis("Mouse ScrollWheel") < 0) mouseCursor = true;


        //This wraps Players movements
        if (refToPlayer.transform.position.x < -9.5) refToPlayer.transform.position += new Vector3(19, 0);
        if (refToPlayer.transform.position.x > 9.5) refToPlayer.transform.position -= new Vector3(19, 0);
        if (refToPlayer.transform.position.y < -5.5) refToPlayer.transform.position += new Vector3(0, 11);
        if (refToPlayer.transform.position.y > 5.5) refToPlayer.transform.position -= new Vector3(0, 11);
        //This clamps circles movements
        if (refToCircle.transform.position.x < -9) refToCircle.transform.position = new Vector3(-9, refToCircle.transform.position.y);
        if (refToCircle.transform.position.x > 9) refToCircle.transform.position = new Vector3(9, refToCircle.transform.position.y);
        if (refToCircle.transform.position.y < -5) refToCircle.transform.position = new Vector3(refToCircle.transform.position.x, -5);
        if (refToCircle.transform.position.y > 5) refToCircle.transform.position = new Vector3(refToCircle.transform.position.x, 5);

        //This is fancy refresh to ensure that the message is not stuck on last pressed Key
        if (refreshMe != refToGeneral.text)
        {
            refreshMe = refToGeneral.text;
            refresh = 0;
        }
        else if (refreshMe != "Xbox pad")
        {
            refresh++;
            if (refresh == 60)
            {
                refresh = 0;
                refToGeneral.text = "Xbox pad";
            }
        }
    }
}
