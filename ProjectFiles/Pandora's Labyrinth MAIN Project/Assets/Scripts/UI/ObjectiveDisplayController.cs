using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveDisplayController : MonoBehaviour
{
    public GameObject backBar, titleText, bodyText;
    public Color barColour, titleColour, bodyColour;
    bool display;
    int displayTimer, initialDelay;

    // Start is called before the first frame update
    void Start()
    {
        barColour = backBar.GetComponent<SpriteRenderer>().color;
        titleColour = titleText.GetComponent<TextMesh>().color;
        bodyColour = bodyText.GetComponent<TextMesh>().color;

        barColour.a = 0;
        titleColour.a = 0;
        bodyColour.a = 0;

        display = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame || GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Pause)
        {
            if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
            {
                if (initialDelay < 60) initialDelay++;
                if (initialDelay >= 60)
                {
                    if (display)
                    {
                        if (titleColour.a < 1)
                        {
                            if (barColour.a < 0.35f) barColour.a += 0.02f;
                            titleColour.a += 0.02f;
                            bodyColour.a += 0.02f;
                        }
                        else display = false;
                    }
                    else if (displayTimer < 120) displayTimer++;
                    if (displayTimer >= 120)
                    {
                        if (titleColour.a > 0)
                        {
                            if (barColour.a > 0) barColour.a -= 0.02f;
                            titleColour.a -= 0.02f;
                            bodyColour.a -= 0.02f;
                        }
                        else Destroy(gameObject);
                    }
                }
            }
        }
        else Destroy(gameObject);

        backBar.GetComponent<SpriteRenderer>().color = barColour;
        titleText.GetComponent<TextMesh>().color = titleColour;
        bodyText.GetComponent<TextMesh>().color = bodyColour;
    }
}
