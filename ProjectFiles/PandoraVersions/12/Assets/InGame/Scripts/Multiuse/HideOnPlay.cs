using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnPlay : MonoBehaviour
{
    public bool ignore;
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
    private void Update()
    {
        if (name.Contains("PCollision")) transform.localPosition = new Vector3(0, -0.72f, 0);
        if (!ignore)
        {
            thisColour.a = 0;
        }
        else
        {
            thisColour.a = 1;
        }

        thisSprite.color = thisColour;
    }
}
