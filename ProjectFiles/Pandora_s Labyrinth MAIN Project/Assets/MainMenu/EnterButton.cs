using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterButton : MonoBehaviour
{
    public bool disableButton, activated, hideSkipText, hoverFX, quickStart;
    GameObject cameraObject;
    SpriteRenderer cursor;
    Color thisColour, startColour;
    float speed, defaultScale;
    Vector3 storyEndPoint;

    // Start is called before the first frame update
    void Start()
    {
        cursor = GameObject.Find("DisplayCursor").GetComponent<SpriteRenderer>();
        cameraObject = GameObject.Find("Main Camera");
        thisColour = GetComponent<SpriteRenderer>().color;
        startColour = thisColour;
        speed = 0.2f;
        storyEndPoint = new Vector3(0, 0, -10);
        storyEndPoint.x = -85;
        GameObject.Find("SkipText").transform.localScale = new Vector3(0, 0, 1);
        defaultScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        cameraObject.transform.position = new Vector3(cameraObject.transform.position.x, cameraObject.transform.position.y, -10);
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Menu)
        {
            // CHECK IF SAVEGAME HAS BEEN LOADED
            if (GameObject.Find(">GameManager<").GetComponent<GameManager>().saveGameLoaded)
            {
                transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = "Continue";
                quickStart = true;
            }

            if (!disableButton) // ENABLED
            {
                thisColour = startColour;
                if (cursor.bounds.Intersects(GetComponent<SpriteRenderer>().bounds))
                {
                    if (!hoverFX) // PLAY FX ON HOVER
                    {
                        GameObject.Find("ButtonOneShots").GetComponent<ButtonFX>().ButtonHover();
                        hoverFX = true;
                    }
                    if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetButtonDown("DropBombGlobal")) // WHEN PRESSED
                    {
                        GameObject.Find("ButtonOneShots").GetComponent<ButtonFX>().ButtonPress();
                        thisColour.a = 0.5f;
                        if (!activated) activated = true;
                        GameObject.Find(">GameManager<").GetComponent<AudioHandler>().storyScreen = true;
                        transform.localScale = new Vector3(defaultScale * 0.9f, defaultScale * 0.9f, 1);
                    }
                    else // WHEN NOT PRESSED OR HOVERING
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
            else // DISABLED
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

            if (activated) // ON BUTTON PRESS
            {
                if (quickStart) cameraObject.transform.position = new Vector3(0, 0, -10); // IF SAVEGAME AVALIABLE
                StartGame();
            }
        }
        else
        {
            GameObject.Find("SkipText").transform.localScale = new Vector3(0, 0, 0);
            activated = false;
            hideSkipText = false;
        }
    }
    void StartGame()
    {
        if (cameraObject.transform.position.x < 0) cameraObject.transform.position += new Vector3(speed, 0, 0);
        else
        {
            activated = false;
            cameraObject.transform.position = new Vector3(0, 0, -10);
            GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState = GameManager.state.Start;
        }
        if (cameraObject.transform.position.x < -229.4f) speed = 2f;
        else
        {
            if (cameraObject.transform.position.x < -90)
            {
                if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("DropBombGlobal")) && !hideSkipText)
                {
                    if (GameObject.Find(">GameManager<").GetComponent<AudioSource>().isPlaying) GameObject.Find(">GameManager<").GetComponent<AudioSource>().Stop();
                    cameraObject.transform.position = new Vector3(-55, 0, -10);
                    hideSkipText = true;
                }
                else speed = 0.0825f;
            }
            else
            {
                if (cameraObject.transform.position.x < 0) speed = 1.25f;
                hideSkipText = true;
            }
            if (!hideSkipText) GameObject.Find("SkipText").transform.localScale = new Vector3(1, 1, 1);
            else GameObject.Find("SkipText").transform.localScale = new Vector3(0, 0, 1);
        }
    }
}
