using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    public bool disableButton;
    SpriteRenderer cursor;
    Color thisColour;

    // Start is called before the first frame update
    void Start()
    {
        cursor = GameObject.Find("DisplayCursor").GetComponent<SpriteRenderer>();
        thisColour = GetComponent<SpriteRenderer>().color;

        disableButton = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!disableButton)
        {
            if (cursor.bounds.Intersects(GetComponent<SpriteRenderer>().bounds))
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    thisColour.a = 0.4f;
                    transform.localScale = new Vector3(14.5f, 14.5f, 1);
                    // BUTTON EVENT
                }
                else
                {
                    thisColour.a = 0.6f;
                    transform.localScale = new Vector3(15.25f, 15.25f, 1);
                }
            }
            else
            {
                thisColour.a = 1f;
                transform.localScale = new Vector3(15f, 15f, 1);
            }
        }
        else
        {
            thisColour = Color.red;
            if (cursor.bounds.Intersects(GetComponent<SpriteRenderer>().bounds))
            {
                thisColour.a = 0.2f;
                transform.localScale = new Vector3(15.25f, 15.25f, 1);
            }
            else
            {
                thisColour.a = 0.4f;
                transform.localScale = new Vector3(15f, 15f, 1);
            }
        }
        GetComponent<SpriteRenderer>().color = thisColour;
    }
}
