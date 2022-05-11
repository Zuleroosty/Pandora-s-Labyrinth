using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnPlay : MonoBehaviour
{
    Color thisColour;
    SpriteRenderer thisSprite;

    // Start is called before the first frame update
    void Start()
    {
        thisSprite = GetComponent<SpriteRenderer>();
        thisColour = thisSprite.color;
        thisColour.a = 0;
        thisSprite.color = thisColour;
    }
}
