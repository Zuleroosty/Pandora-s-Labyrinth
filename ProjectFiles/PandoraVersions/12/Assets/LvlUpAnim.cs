using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlUpAnim : MonoBehaviour
{
    Color thisColour;

    // Start is called before the first frame update
    void Start()
    {
        thisColour = GetComponent<SpriteRenderer>().color;
        thisColour.a = 0.5f;

        transform.parent = GameObject.Find("PlayerSprite").transform;
        transform.localPosition = new Vector3(0, 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < 8) transform.localScale += new Vector3(0.25f, 0.25f, 0);
        if (thisColour.a > 0) thisColour.a -= 0.015f;
        else Destroy(gameObject);

        GetComponent<SpriteRenderer>().color = thisColour;
    }
}
