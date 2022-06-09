using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public bool disableButton;
    SpriteRenderer cursor;
    Color thisColour;
    float defaultScale;

    // Start is called before the first frame update
    void Start()
    {
        cursor = GameObject.Find("DisplayCursor").GetComponent<SpriteRenderer>();
        thisColour = GetComponent<SpriteRenderer>().color;
        defaultScale = transform.localScale.x;
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
                    thisColour.a = 0.5f;
                    transform.localScale = new Vector3(defaultScale * 0.9f, defaultScale * 0.9f, 1);
                }
                else
                {
                    thisColour.a = 1f;
                    transform.localScale = new Vector3(defaultScale * 1.1f, defaultScale * 1.1f, 1);
                }
            }
            else
            {
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
