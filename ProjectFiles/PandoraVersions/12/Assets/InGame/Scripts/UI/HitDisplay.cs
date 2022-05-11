using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDisplay : MonoBehaviour
{
    public bool pulseIn, pulseOut;
    Color thisColour;
    int delay;


    // Start is called before the first frame update
    void Start()
    {
        thisColour = GetComponent<SpriteRenderer>().color;
        thisColour.a = 0;
        GetComponent<SpriteRenderer>().color = thisColour;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize > 7) transform.localScale = new Vector3(2, 2, 1);
        else transform.localScale = new Vector3(1.8f, 1.8f, 1);

        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (pulseIn)
            {
                if (thisColour.a < 0.5f) thisColour.a += 0.005f;
                else
                {
                    pulseIn = false;
                    delay = 0;
                    pulseOut = true;
                }
            }
            else if (pulseOut)
            {
                if (delay < 10) delay++;
                else
                {
                    if (thisColour.a > 0f) thisColour.a -= 0.005f;
                    else pulseOut = false;
                }
            }
        }
        else
        {
            thisColour.a = 0;
            pulseIn = false;
            pulseOut = false;
        }
        GetComponent<SpriteRenderer>().color = thisColour;
    }
}
