using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCollision : MonoBehaviour
{
    public GameObject pathParent, testObject;
    public int childID, childMax;
    float distanceX, distanceY, borderX, borderY;

    // Update is called once per frame
    void Update()
    {
        TestCollision();
        TestCollision();
    }
    void TestCollision()
    {
        if (childID < childMax && childMax == pathParent.transform.childCount)
        {
            testObject = pathParent.transform.GetChild(childID).gameObject;
            testObject.GetComponent<PathController>().lockLink = true;
            if (GetComponent<SpriteRenderer>().bounds.Intersects(testObject.GetComponent<SpriteRenderer>().bounds))
            {
                if (testObject.GetComponent<PathController>().nextLink != null) testObject.GetComponent<PathController>().nextLink.GetComponent<PathController>().prevLink = null;
                GrabNewDirection();
            }
            else testObject.GetComponent<PathController>().lockLink = false;
        }
        else
        {
            childID = -1;
            childMax = pathParent.transform.childCount;
        }
        childID++;
    }
    void GrabNewDirection()
    {
        distanceX = (testObject.GetComponent<PathController>().owner.GetComponent<SpriteRenderer>().bounds.max.x - testObject.GetComponent<PathController>().owner.GetComponent<SpriteRenderer>().bounds.min.x); // GET TEST OBJECT RADIUS (CENTER TO EDGE) 
        distanceY = (testObject.GetComponent<PathController>().owner.GetComponent<SpriteRenderer>().bounds.max.y - testObject.GetComponent<PathController>().owner.GetComponent<SpriteRenderer>().bounds.min.y);

        testObject.GetComponent<PathController>().forceDirection = true;

        if (testObject.transform.position.y > GetComponent<SpriteRenderer>().bounds.min.y - borderY && testObject.transform.position.y < GetComponent<SpriteRenderer>().bounds.max.y + borderY) // BETWEEN MIN & MAX Y BORDER - X OUTPUT
        {
            if (testObject.transform.position.x < transform.position.x) // --LEFT--
            {
                if (testObject.transform.position.y > transform.position.y)
                {
                    testObject.transform.position = new Vector3(GetComponent<SpriteRenderer>().bounds.min.x - distanceX, testObject.transform.position.y + distanceY, testObject.transform.position.z);
                    testObject.GetComponent<PathController>().dirForce = 1;
                }
                else
                {
                    testObject.transform.position = new Vector3(GetComponent<SpriteRenderer>().bounds.min.x - distanceX, testObject.transform.position.y - distanceY, testObject.transform.position.z);
                    testObject.GetComponent<PathController>().dirForce = 2;
                }
            }
            else // > transform.position.x // --RIGHT--
            {
                if (testObject.transform.position.y > transform.position.y)
                {
                    testObject.transform.position = new Vector3(GetComponent<SpriteRenderer>().bounds.max.x + distanceX, testObject.transform.position.y + distanceY, testObject.transform.position.z);
                    testObject.GetComponent<PathController>().dirForce = 1;
                }
                else
                {
                    testObject.transform.position = new Vector3(GetComponent<SpriteRenderer>().bounds.max.x + distanceX, testObject.transform.position.y - distanceY, testObject.transform.position.z);
                    testObject.GetComponent<PathController>().dirForce = 2;
                }
            }
        }
        else if (testObject.transform.position.x > GetComponent<SpriteRenderer>().bounds.min.x - borderX && testObject.transform.position.x < GetComponent<SpriteRenderer>().bounds.max.x + borderX) // BETWEEN MIN & MAX X BORDER - Y OUTPUT
        {
            if (testObject.transform.position.y < transform.position.y) // --DOWN--
            {
                if (testObject.transform.position.x > transform.position.x)
                {
                    testObject.transform.position = new Vector3(testObject.transform.position.x + distanceX, GetComponent<SpriteRenderer>().bounds.min.y - distanceY, testObject.transform.position.z);
                    testObject.GetComponent<PathController>().dirForce = 3;
                }
                else
                {
                    testObject.transform.position = new Vector3(testObject.transform.position.x - distanceX, GetComponent<SpriteRenderer>().bounds.min.y - distanceY, testObject.transform.position.z);
                    testObject.GetComponent<PathController>().dirForce = 4;
                }
            }
            else // > transform.position.y // --UP--
            {
                if (testObject.transform.position.x > transform.position.x)
                {
                    testObject.transform.position = new Vector3(testObject.transform.position.x + distanceX, GetComponent<SpriteRenderer>().bounds.max.y + distanceY, testObject.transform.position.z);
                    testObject.GetComponent<PathController>().dirForce = 3;
                }
                else
                {
                    testObject.transform.position = new Vector3(testObject.transform.position.x - distanceX, GetComponent<SpriteRenderer>().bounds.max.y + distanceY, testObject.transform.position.z);
                    testObject.GetComponent<PathController>().dirForce = 4;
                }
            }
        }
        else
        {
            if (testObject.transform.position.x < GetComponent<SpriteRenderer>().bounds.min.x - borderX && testObject.transform.position.y < GetComponent<SpriteRenderer>().bounds.min.y + borderY) // BOTTOM LEFT CORNER
            {
                testObject.transform.position = new Vector3(GetComponent<SpriteRenderer>().bounds.min.x - distanceX, GetComponent<SpriteRenderer>().bounds.min.y - distanceY, testObject.transform.position.z);
            }


            if (testObject.transform.position.x < GetComponent<SpriteRenderer>().bounds.min.x - borderX && testObject.transform.position.y > GetComponent<SpriteRenderer>().bounds.max.y + borderY) // TOP LEFT CORNER
            {
                testObject.transform.position = new Vector3(GetComponent<SpriteRenderer>().bounds.min.x - distanceX, GetComponent<SpriteRenderer>().bounds.max.y + distanceY, testObject.transform.position.z);
            }


            if (testObject.transform.position.x > GetComponent<SpriteRenderer>().bounds.max.x - borderX && testObject.transform.position.y < GetComponent<SpriteRenderer>().bounds.min.y + borderY) // BOTTOM RIGHT CORNER
            {
                testObject.transform.position = new Vector3(GetComponent<SpriteRenderer>().bounds.max.x + distanceX, GetComponent<SpriteRenderer>().bounds.min.y - distanceY, testObject.transform.position.z);
            }


            if (testObject.transform.position.x > GetComponent<SpriteRenderer>().bounds.max.x - borderX && testObject.transform.position.y > GetComponent<SpriteRenderer>().bounds.max.y + borderY) // TOP RIGHT CORNER
            {
                testObject.transform.position = new Vector3(GetComponent<SpriteRenderer>().bounds.max.x + distanceX, GetComponent<SpriteRenderer>().bounds.max.y + distanceY, testObject.transform.position.z);
            }
        }
    }
}
