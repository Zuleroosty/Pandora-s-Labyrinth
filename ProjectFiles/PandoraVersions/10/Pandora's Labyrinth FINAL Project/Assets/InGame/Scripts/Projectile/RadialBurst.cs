using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialBurst : MonoBehaviour
{
    public Sprite bombStatic, bomb1, bomb2, bomb3, bombExplode, radialBurst;
    int displayTimer;
    bool explode;
    Color thisColour;

    private void Start()
    {
        transform.parent = GameObject.Find("----ProjectileParent----").transform;
        thisColour = GetComponent<SpriteRenderer>().color;
        thisColour.a = 1;
        transform.localScale = new Vector3(5, 5, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!explode)
        {
            displayTimer++;
            if (displayTimer > 0) GetComponent<SpriteRenderer>().sprite = bombStatic;
            if (displayTimer > 10) GetComponent<SpriteRenderer>().sprite = bomb1;
            if (displayTimer > 20) GetComponent<SpriteRenderer>().sprite = bomb2;
            if (displayTimer > 30) GetComponent<SpriteRenderer>().sprite = bomb3;
            if (displayTimer > 40) GetComponent<SpriteRenderer>().sprite = bombExplode;
            if (displayTimer >= 42)
            {
                explode = true;
                transform.localScale = new Vector3(0.02f, 0.02f, 1);
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = radialBurst;
            transform.localScale += new Vector3(1.5f, 1.5f, 0);
            thisColour = GetComponent<SpriteRenderer>().color;
            if (transform.localScale.x >= 7.5f)
            {
                if (thisColour.a > 0) thisColour.a -= 0.1f;
                else Destroy(gameObject);
            }
            else if (transform.localScale.x >= 5f)
            {
                thisColour.a -= 0.04f;
            }
            GetComponent<SpriteRenderer>().color = thisColour;
        }
    }
}
