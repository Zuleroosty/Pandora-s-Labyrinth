using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesManager : MonoBehaviour
{
    public Sprite spearBasic, spearBlue, spearGreen, spearRed;
    Color thisColour;

    // Start is called before the first frame update
    void Start()
    {
        thisColour = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void UpdateSprites()
    {
        switch (GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().currentArmour)
        {
            case PlayerController.armour.lvl1:

                break;
            case PlayerController.armour.lvl2:

                break;
            case PlayerController.armour.lvl3:

                break;
            case PlayerController.armour.lvl4:

                break;
        }
        switch (GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().currentSpear)
        {
            case PlayerController.spear.lvl1:

                break;
            case PlayerController.spear.lvl2:

                break;
            case PlayerController.spear.lvl3:

                break;
            case PlayerController.spear.lvl4:

                break;
        }
    }
}
