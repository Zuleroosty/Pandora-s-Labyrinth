using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public bool disableButton, activated;
    public GameObject cameraObject, colSprite;
    SpriteRenderer cursor;
    Color thisColour;

    // Start is called before the first frame update
    void Start()
    {
        cursor = GameObject.Find("DisplayCursor").GetComponent<SpriteRenderer>();
        thisColour = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Pause)
        {
            if (!disableButton)
            {
                if (cursor.bounds.Intersects(colSprite.GetComponent<SpriteRenderer>().bounds))
                {
                    if (Input.GetKey(KeyCode.Mouse0) || Input.GetButtonDown("DropBombGlobal"))
                    {
                        thisColour.a = 0.5f;
                        if (!activated) activated = true;
                        transform.localScale = new Vector3(7.5f, 7.5f, 1);
                    }
                    else
                    {
                        thisColour.a = 0.6f;
                        transform.localScale = new Vector3(8.25f, 8.25f, 1);
                    }
                }
                else
                {
                    thisColour.a = 1f;
                    transform.localScale = new Vector3(8f, 8f, 1);
                }
            }
            else
            {
                thisColour = Color.red;
                if (cursor.bounds.Intersects(colSprite.GetComponent<SpriteRenderer>().bounds))
                {
                    thisColour.a = 0.2f;
                    transform.localScale = new Vector3(8.25f, 8.25f, 1);
                }
                else
                {
                    thisColour.a = 0.4f;
                    transform.localScale = new Vector3(8f, 8f, 1);
                }
            }
            GetComponent<SpriteRenderer>().color = thisColour;
            if (activated) ButtonFunction();
        }
        else activated = false;
    }
    void ButtonFunction()
    {
        GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState = GameManager.state.InGame;
    }
}
