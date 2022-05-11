using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCollision : MonoBehaviour
{
    public GameObject pathParent, pathObject, prevObject;
    public int childID, childMax;

    private void Start()
    {
        pathParent = GameObject.Find("----Pathfinding----");
    }
    // Update is called once per frame
    void Update()
    {
        TestCollision();
        TestCollision();
        TestCollision();
    }
    void TestCollision()
    {
        if (pathParent.transform.childCount > 0)
        {
            // ENEMY COLLISION
            if (childID < childMax && childMax == pathParent.transform.childCount)
            {
                pathObject = pathParent.transform.GetChild(childID).gameObject;
                if (GetComponent<SpriteRenderer>().bounds.Intersects(pathObject.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds))
                {
                    prevObject = pathObject.GetComponent<CreatePath>().prevObject;
                    prevObject.GetComponent<CreatePath>().isBlocked = true;
                }
            }
            else
            {
                childID = -1;
                childMax = pathParent.transform.childCount;
            }
            childID++;
        }
    }
}
