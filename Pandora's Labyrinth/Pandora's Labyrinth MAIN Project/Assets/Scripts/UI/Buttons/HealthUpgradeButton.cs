using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgradeButton : MonoBehaviour
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
        thisColour = GetComponent<SpriteRenderer>().color;
        player = GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>();
        defaultScale = transform.localScale.x;
        player.healthCost = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) player = GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>();
        else
        {
            if (player.level < player.healthRequiredLevel || player.gold < player.healthCost) disableButton = true;
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
                    if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetButtonDown("DropBombGlobal"))
                    {
                        GameObject.Find("ButtonOneShots").GetComponent<ButtonFX>().ButtonPress();
                        thisColour.a = 0.5f;
                        transform.localScale = new Vector3(defaultScale * 0.9f, defaultScale * 0.9f, 1);
                        if (player.gold >= player.healthCost) UpdateButton();
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
        player.gold -= player.healthCost;
        player.maxHealth *= (1 + 0.1f);
        player.healthIncrease += 0.1f;
        player.healthRequiredLevel *= 3;
        player.healthCost = player.healthRequiredLevel / 3;
        delayTimer = 0;
    }
}
