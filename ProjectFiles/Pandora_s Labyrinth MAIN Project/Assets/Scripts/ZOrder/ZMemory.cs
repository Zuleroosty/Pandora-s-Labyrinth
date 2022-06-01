using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZMemory : MonoBehaviour
{
    public GameObject thisSprite, thisCollider, collidingObject;
    public float expectedZ;
    public bool inFrontOfPlayer, inFrontOfObject;

    // Update is called once per frame
    void Update()
    {
        if (collidingObject != null)
        {
            if (thisCollider.GetComponent<SpriteRenderer>().bounds.Intersects(collidingObject.GetComponent<ObjectZRelation>().thisCollider.GetComponent<SpriteRenderer>().bounds))
            {
                if (thisSprite.GetComponent<SpriteRenderer>().bounds.min.y < collidingObject.GetComponent<ObjectZRelation>().thisSprite.GetComponent<SpriteRenderer>().bounds.min.y) inFrontOfObject = true;
                else inFrontOfObject = false;
            }
            else collidingObject = null;
        }
        if (!this.name.Contains("Player"))
        {
            if (thisSprite.GetComponent<SpriteRenderer>().bounds.min.y < GameObject.Find("ZCollision").GetComponent<SpriteRenderer>().bounds.min.y) inFrontOfPlayer = true;
            else inFrontOfPlayer = false;
        }

        UpdateZ();

        thisSprite.transform.position = new Vector3(thisSprite.transform.position.x, thisSprite.transform.position.y, expectedZ);
    }
    void UpdateZ()
    {
        if (!this.name.Contains("Player"))
        {
            if (!inFrontOfObject && !inFrontOfPlayer) expectedZ = 2.25f;
            if (!inFrontOfObject && inFrontOfPlayer) expectedZ = 0.75f;
            if (inFrontOfObject && !inFrontOfPlayer) expectedZ = -0.75f;
            if (inFrontOfObject && inFrontOfPlayer) expectedZ = -2.25f;
        }
        else
        {
            if (inFrontOfObject) expectedZ = -1.5f;
            else expectedZ = 1.5f;
        }
    }
}
