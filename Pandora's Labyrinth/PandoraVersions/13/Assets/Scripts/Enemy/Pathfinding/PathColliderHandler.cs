using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathColliderHandler : MonoBehaviour
{
    public bool forceNorth, forceSouth, forceEast, forceWest;
    int timer;

    private void Update()
    {
        if (timer < 5) timer++;
        if (timer >= 5)
        {
            if (transform.position.x < GameObject.Find("----PlayerObjectParent----").transform.position.x + 15 && transform.position.x > GameObject.Find("----PlayerObjectParent----").transform.position.x - 15 && transform.position.y < GameObject.Find("----PlayerObjectParent----").transform.position.y + 8 && transform.position.y > GameObject.Find("----PlayerObjectParent----").transform.position.y - 8)
            {
                transform.parent = GameObject.Find("----LocalPathCollision----").transform;
            }
            else transform.parent = GameObject.Find("----PathCollision----").transform;
        }
    }
}
