using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIncreaseButton : MonoBehaviour
{
    public bool disableButton, activated, updateButton, hoverFX;
    public GameObject cameraObject, colSprite;
    PlayerController player;
    SpriteRenderer cursor;
    Color thisColour;
    float defaultScale;
    int delayTimer;

    // Start is called before the first frame update
    void Start()
    {
        cursor = GameObject.Find("DisplayCursor").GetComponent<SpriteRenderer>();
        player = GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>();
        thisColour = GetComponent<SpriteRenderer>().color;
        defaultScale = transform.localScale.x;
        player.speedCost = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) player = GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>();
        else
        {
            if (player.level < player.speedRequiredLevel || player.gold < player.speedCost) disableButton = true;
            else disableButton = false;
            if (!disableButton)
            {
                thisColour = Color.white;
                if (cursor.bounds.Intersects(colSprite.GetComponent<SpriteRenderer>().bounds))
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
                        transform.localScale = new Vector3(defaultScale * 0.9f, defaultScale * 0.9f, 1);
                        if (player.gold >= player.speedCost) UpdateButton();
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
                if (cursor.bounds.Intersects(colSprite.GetComponent<SpriteRenderer>().bounds))
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
    void UpdateButton()
    {
        player.gold -= player.speedCost;
        player.speedDefault *= (1 + 0.1f);
        player.speedIncrease += 0.1f;
        player.speedRequiredLevel *= 3;
        player.speedCost = player.speedRequiredLevel / 3;
        delayTimer = 0;
    }
}
