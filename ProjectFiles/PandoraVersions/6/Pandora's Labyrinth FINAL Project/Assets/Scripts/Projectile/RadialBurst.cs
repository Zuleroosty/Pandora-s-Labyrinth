using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialBurst : MonoBehaviour
{
    Color thisColour;

    private void Start()
    {
        transform.parent = GameObject.Find("----ProjectileParent----").transform;
        transform.localScale = new Vector3(0.01f, 0.01f, 1);
        thisColour = GetComponent<SpriteRenderer>().color;
        thisColour.a = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.localScale.x >= 15f)
        {
            if (thisColour.a > 0) thisColour.a -= 0.1f;
            else Destroy(gameObject);
        }
        else if (transform.localScale.x >= 8f)
        {
            thisColour.a -= 0.04f;
        }
        GetComponent<SpriteRenderer>().color = thisColour;
        transform.localScale += new Vector3(1, 1, 0);
    }
}
