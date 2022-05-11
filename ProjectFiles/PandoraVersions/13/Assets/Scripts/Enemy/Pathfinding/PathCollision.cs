using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCollision : MonoBehaviour
{
    public GameObject owningObject, collisionParent, collisionObject;
    PathHandler pathScript;
    int childID, childMax;

    // Start is called before the first frame update
    void Start()
    {
        owningObject = transform.parent.gameObject;
        pathScript = owningObject.GetComponent<PathHandler>();

        collisionParent = GameObject.Find("----LocalPathCollision----");
    }

    // Update is called once per frame
    void Update()
    {
        if (!pathScript.blockNorth && !pathScript.blockSouth && !pathScript.blockEast && !pathScript.blockWest)
        {
            TestCollision(); // DUPLICATE FOR ACCURACY
            TestCollision();
            TestCollision();
            TestCollision();
            TestCollision();
            TestCollision();
            TestCollision();
            TestCollision();
            TestCollision();
            TestCollision();
            TestCollision();
            TestCollision();
            TestCollision();
            TestCollision();
            TestCollision();
            TestCollision();
        }
    }
    void TestCollision()
    {
        if (childID < childMax && childMax == collisionParent.transform.childCount) 
        {
            collisionObject = collisionParent.transform.GetChild(childID).gameObject;
            pathScript.collisionObject = collisionObject;
            if (!collisionObject.GetComponent<CollisionManager>().disableCollision)
            {
                if (transform.position.x < collisionObject.GetComponent<SpriteRenderer>().bounds.max.x && transform.position.x > collisionObject.GetComponent<SpriteRenderer>().bounds.min.x && transform.position.y < collisionObject.GetComponent<SpriteRenderer>().bounds.max.y && transform.position.y > collisionObject.GetComponent<SpriteRenderer>().bounds.min.y)
                {
                    if (this.name.Contains("North"))
                    {
                        pathScript.blockNorth = true;
                        GetComponent<SpriteRenderer>().color = Color.red;
                        owningObject.transform.position -= new Vector3(0, 0.015f, 0);
                    }
                    if (this.name.Contains("South"))
                    {
                        pathScript.blockSouth = true;
                        GetComponent<SpriteRenderer>().color = Color.red;
                        owningObject.transform.position += new Vector3(0, 0.015f, 0);
                    }
                    if (this.name.Contains("East"))
                    {
                        pathScript.blockEast = true;
                        GetComponent<SpriteRenderer>().color = Color.red;
                        owningObject.transform.position -= new Vector3(0.015f, 0, 0);
                    }
                    if (this.name.Contains("West"))
                    {
                        pathScript.blockWest = true;
                        GetComponent<SpriteRenderer>().color = Color.red;
                        owningObject.transform.position += new Vector3(0.015f, 0, 0);
                    }
                }
                else
                {
                    if (this.name.Contains("North"))
                    {
                        pathScript.blockNorth = false;
                        GetComponent<SpriteRenderer>().color = Color.green;
                    }
                    if (this.name.Contains("South"))
                    {
                        pathScript.blockSouth = false;
                        GetComponent<SpriteRenderer>().color = Color.green;
                    }
                    if (this.name.Contains("East"))
                    {
                        pathScript.blockEast = false;
                        GetComponent<SpriteRenderer>().color = Color.green;
                    }
                    if (this.name.Contains("West"))
                    {
                        pathScript.blockWest = false;
                        GetComponent<SpriteRenderer>().color = Color.green;
                    }
                }
            }
        }
        else
        {
            childID = -1;
            childMax = collisionParent.transform.childCount;
        }
        childID++;
    }
}
