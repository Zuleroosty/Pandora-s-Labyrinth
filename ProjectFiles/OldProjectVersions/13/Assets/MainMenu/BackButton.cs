using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public bool disableButton, activated, hoverFX;
    public GameObject colSprite, controlsParent, controlsButton;
    SpriteRenderer cursor;
    Color thisColour, startColour;

    // Start is called before the first frame update
    void Start()
    {
        cursor = GameObject.Find("DisplayCursor").GetComponent<SpriteRenderer>();
        thisColour = GetComponent<SpriteRenderer>().color;
        startColour = thisColour;
    }

    // Update is called once per frame
    void Update()
    {
        if (!disableButton)
        {
            thisColour = startColour;
            if (cursor.bounds.Intersects(colSprite.GetComponent<SpriteRenderer>().bounds))
            {
                if (!hoverFX)
                {
                    GameObject.Find("ButtonOneShots").GetComponent<ButtonFX>().ButtonHover();
                    hoverFX = true;
                }
                if (Input.GetKey(KeyCode.Mouse0) || Input.GetButtonDown("UseBoostGlobal"))
                {
                    GameObject.Find("ButtonOneShots").GetComponent<ButtonFX>().ButtonPress();
                    thisColour.a = 0.5f;
                    controlsParent.transform.localPosition = new Vector3(-45, 0, -9);
                    transform.localScale = new Vector3(0.06f * 0.9f, 0.30f * 0.9f, 1);
                    disableButton = true;
                    controlsButton.GetComponent<ControlsButton>().disableButton = false;
                }
                else
                {
                    thisColour.a = 0.6f;
                    transform.localScale = new Vector3(0.06f * 1.1f, 0.30f * 1.1f, 1);
                }
            }
            else
            {
                hoverFX = false;
                thisColour.a = 1f;
                transform.localScale = new Vector3(0.06f, 0.30f, 1);
            }
        }
        else
        {
            thisColour = Color.red;
            if (cursor.bounds.Intersects(colSprite.GetComponent<SpriteRenderer>().bounds))
            {
                thisColour.a = 0.2f;
                transform.localScale = new Vector3(0.06f * 1.1f, 0.30f * 1.1f, 1);
            }
            else
            {
                thisColour.a = 0.4f;
                transform.localScale = new Vector3(0.06f, 0.30f, 1);
            }
        }
        GetComponent<SpriteRenderer>().color = thisColour;
    }
}
