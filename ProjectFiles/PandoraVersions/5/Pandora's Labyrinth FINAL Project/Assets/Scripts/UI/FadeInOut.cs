using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public bool fadeIn, fadeOut, isSprite;
    public int timer, delayTimer;
    public float fadeSpeed;
    Color thisColour;
    SpriteRenderer thisSprite;
    TextMesh thisTextMesh;

    // Start is called before the first frame update
    void Start()
    {
        if (fadeSpeed <= 0) fadeSpeed = 0.015f;
        if (isSprite)
        {
            thisSprite = GetComponent<SpriteRenderer>();
            thisColour = thisSprite.color;
            if (fadeIn)
            {
                thisColour.a = 0;
                thisSprite.color = thisColour;
            }
            if (fadeOut)
            {
                thisColour.a = 1;
                thisSprite.color = thisColour;
            }
        }
        else
        {
            thisTextMesh = GetComponent<TextMesh>();
            thisColour = thisTextMesh.color;
            if (fadeIn)
            {
                thisColour.a = 0;
                thisTextMesh.color = thisColour;
            }
            if (fadeOut)
            {
                thisColour.a = 1;
                thisTextMesh.color = thisColour;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.name.Contains("BlackScreen"))
        {
            if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Start)
            {
                if (delayTimer < 35) delayTimer++;
                if (delayTimer >= 35)
                {
                    if (thisSprite.color.a > 0)
                    {
                        thisColour.a -= fadeSpeed;
                        thisSprite.color = thisColour;
                    }
                }
            }
            if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Reset)
            {
                if (delayTimer < 35) delayTimer++;
                if (delayTimer >= 35)
                {
                    if (thisSprite.color.a < 1)
                    {
                        thisColour.a += fadeSpeed;
                        thisSprite.color = thisColour;
                    }
                    else if (thisSprite.color.a >= 1) GameObject.Find(">GameManager<").GetComponent<GameManager>().readyToStart = true;
                }
            }
        }
        else if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (delayTimer < 60) delayTimer++;
            if (delayTimer >= 60)
            {
                if (fadeIn) // APPEAR
                {
                    if (isSprite)
                    {
                        if (thisSprite.color.a < 1)
                        {
                            thisColour.a += fadeSpeed;
                            thisSprite.color = thisColour;
                        }
                        if (thisSprite.color.a >= 1) fadeIn = false;
                    }
                    else
                    {
                        if (thisTextMesh.color.a < 1)
                        {
                            thisColour.a += fadeSpeed;
                            thisTextMesh.color = thisColour;
                        }
                        if (thisTextMesh.color.a >= 1) fadeIn = false;
                    }
                }
                if (fadeOut) // DISAPPEAR
                {
                    if (isSprite)
                    {
                        if (thisSprite.color.a > 0)
                        {
                            thisColour.a -= fadeSpeed;
                            thisSprite.color = thisColour;
                        }
                        if (thisSprite.color.a <= 0) fadeOut = false;
                    }
                    else
                    {
                        if (thisTextMesh.color.a > 0)
                        {
                            thisColour.a -= fadeSpeed;
                            thisTextMesh.color = thisColour;
                        }
                        if (thisTextMesh.color.a <= 0) fadeOut = false;
                    }
                }
            }
        }
    }
}
