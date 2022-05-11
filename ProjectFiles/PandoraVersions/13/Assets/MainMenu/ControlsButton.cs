using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsButton : MonoBehaviour
{
    public bool disableButton, hoverFX;
    public GameObject controlsParent, backButtonObject, startButton, quitButton;
    SpriteRenderer cursor;
    Color thisColour, startColour;
    float defaultScale;

    // Start is called before the first frame update
    void Start()
    {
        cursor = GameObject.Find("DisplayCursor").GetComponent<SpriteRenderer>();
        thisColour = GetComponent<SpriteRenderer>().color;
        startColour = thisColour;
        defaultScale = transform.localScale.x;
        controlsParent.transform.localPosition = new Vector3(-45, 0, -9);
    }

    // Update is called once per frame
    void Update()
    {
        startButton.GetComponent<EnterButton>().disableButton = disableButton;
        quitButton.GetComponent<ExitButton>().disableButton = disableButton;
        if (!disableButton)
        {
            thisColour = startColour;
            if (cursor.bounds.Intersects(GetComponent<SpriteRenderer>().bounds))
            {
                if (!hoverFX)
                {
                    GameObject.Find("ButtonOneShots").GetComponent<ButtonFX>().ButtonHover();
                    hoverFX = true;
                }
                if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetButtonDown("UseBoostGlobal"))
                {
                    GameObject.Find("ButtonOneShots").GetComponent<ButtonFX>().ButtonPress();
                    thisColour.a = 0.5f;
                    controlsParent.transform.localPosition = new Vector3(0, 0, -9);
                    transform.localScale = new Vector3(defaultScale * 0.9f, defaultScale * 0.9f, 1);
                    disableButton = true;
                    backButtonObject.GetComponent<BackButton>().disableButton = false;
                }
                else
                {
                    thisColour.a = 1f;
                    transform.localScale = new Vector3(defaultScale * 1.1f, defaultScale * 1.1f, 1);
                }
            }
            else
            {
                hoverFX = false;
                thisColour.a = 0.8f;
                transform.localScale = new Vector3(defaultScale, defaultScale, 1);
            }
        }
        else
        {
            thisColour = Color.red;
            if (cursor.bounds.Intersects(GetComponent<SpriteRenderer>().bounds))
            {
                thisColour.a = 0.5f;
                transform.localScale = new Vector3(defaultScale * 1.1f, defaultScale * 1.1f, 1);
            }
            else
            {
                thisColour.a = 0.4f;
                transform.localScale = new Vector3(defaultScale, defaultScale, 1);
            }
        }
        GetComponent<SpriteRenderer>().color = thisColour;
    }
}
