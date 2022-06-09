using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    public GameObject linkPrefab, prevLink, nextLink, owningObject, targetObject;
    public GameObject lCol, rCol, uCol, dCol; // COLLIDING OBJECTS
    public GameObject testLeft, testRight, testUp, testDown, collisionParent, collisionObject;
    public int childID, childMax, frameRate, currentStage; // 1 = get location / 2 = spawn at location / 3 = dormant
    int updateDelay;
    public Vector3 newDirection;
    bool acceptCustomLocation, canDestroy, isActive;

    public enum direction {Up, Down, Left, Right, Empty, New}
    public direction testDirection;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = GameObject.Find("----PathLinks----").transform;
        collisionParent = GameObject.Find("----PathCollision----");
        owningObject.GetComponent<PathMaster>().pathLinks++;
        this.name = "PathLink"; // REMOVE (CLONE) FROM NAME

        // FORCE RESET IN CASE OF DUPE BUG
        uCol = null;
        rCol = null;
        lCol = null;
        dCol = null;

        if (frameRate <= 0) frameRate = 1; // CONTROLS HOW OFTEN COLLISION AND CODE IS RUN - DEFAULT 5 FRAMES
        currentStage = 1;

        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        TestCollision();
        TestCollision();

        // ON TIMER \/
        if (updateDelay < frameRate) updateDelay++;
        else
        {
            updateDelay = 0;
            if (targetObject == null) targetObject = owningObject.GetComponent<PathMaster>().targetObject; // UPDATE TARGET IF MISSING
            if (!GetComponent<SpriteRenderer>().bounds.Intersects(targetObject.GetComponent<SpriteRenderer>().bounds)) // STOP RUNNING CODE (FOR EFFECIENCY)
            {
                // RUN BASELINE
                switch (currentStage)
                {
                    case 1:
                        GetNewLocation();
                        break;
                    // STAGE 2 SINGLE FIRE ONLY
                    case 2:
                        if (owningObject.GetComponent<PathMaster>().pathLinks < 4) SpawnNextLink();
                        break;
                    case 3:
                        if (GetComponent<SpriteRenderer>().bounds.Intersects(owningObject.GetComponent<SpriteRenderer>().bounds) && nextLink != null)
                        {
                            nextLink.GetComponent<PathController>().prevLink = owningObject;
                            owningObject.GetComponent<PathMaster>().GetNextLink(this.gameObject);
                        }
                        else if (prevLink == null || nextLink == null) Destroy(gameObject);
                        break;
                }
            }
        }
    }

    void GetNewLocation() // 1
    {
        newDirection = new Vector3(0, 0, 0); // RESET ON CALL - REUSEABLE
        testDirection = direction.Empty;

        if (dCol != null || uCol != null || lCol != null || rCol != null)
        {
            if (dCol != null && targetObject.transform.position.y < transform.position.y) // CHECK DOWN COL
            {
                if (dCol.GetComponent<PathCollision>().noForce)
                {
                    if (targetObject.transform.position.x > transform.position.x) testDirection = direction.Right;
                    else if (targetObject.transform.position.x < transform.position.x) testDirection = direction.Left;
                }
                else
                {
                    if (dCol.GetComponent<PathCollision>().forceLeft) testDirection = direction.Left; // UPDATE IF FORCE LEFT/RIGHT
                    else if (dCol.GetComponent<PathCollision>().forceRight) testDirection = direction.Right; // UPDATE IF FORCE LEFT/RIGHT
                    else
                    {
                        if (targetObject.transform.position.x > transform.position.x) testDirection = direction.Right;
                        else if (targetObject.transform.position.x < transform.position.x) testDirection = direction.Left;
                    }
                }
            }
            else dCol = null;
            if (uCol != null && targetObject.transform.position.y > transform.position.y) // CHECK UP COL
            {
                if (uCol.GetComponent<PathCollision>().noForce)
                {
                    if (targetObject.transform.position.x > transform.position.x) testDirection = direction.Right;
                    else if (targetObject.transform.position.x < transform.position.x) testDirection = direction.Left;
                }
                else
                {
                    if (uCol.GetComponent<PathCollision>().forceLeft) testDirection = direction.Left; // UPDATE IF FORCE LEFT/RIGHT
                    else if (uCol.GetComponent<PathCollision>().forceRight) testDirection = direction.Right; // UPDATE IF FORCE LEFT/RIGHT
                    else
                    {
                        if (targetObject.transform.position.x > transform.position.x) testDirection = direction.Right;
                        else if (targetObject.transform.position.x < transform.position.x) testDirection = direction.Left;
                    }
                }
            }
            else uCol = null;
            if (lCol != null && targetObject.transform.position.x < transform.position.x) // CHECK LEFT COL -----
            {
                if (lCol.GetComponent<PathCollision>().noForce)
                {
                    if (targetObject.transform.position.y > transform.position.y) testDirection = direction.Up;
                    else if (targetObject.transform.position.y < transform.position.y) testDirection = direction.Down;
                }
                else
                {
                    if (lCol.GetComponent<PathCollision>().forceUp) testDirection = direction.Up; // UPDATE IF FORCE UP/DOWN
                    else if (lCol.GetComponent<PathCollision>().forceDown) testDirection = direction.Down; // UPDATE IF FORCE UP/DOWN
                    else
                    {
                        if (targetObject.transform.position.y > transform.position.y) testDirection = direction.Up;
                        else if (targetObject.transform.position.y < transform.position.y) testDirection = direction.Down;
                    }
                }
            }
            else lCol = null;
            if (rCol != null && targetObject.transform.position.x > transform.position.x) // CHECK RIGHT COL -----
            {
                if (rCol.GetComponent<PathCollision>().noForce)
                {
                    if (targetObject.transform.position.y > transform.position.y) testDirection = direction.Up;
                    else if (targetObject.transform.position.y < transform.position.y) testDirection = direction.Down;
                }
                else
                {
                    if (rCol.GetComponent<PathCollision>().forceUp) testDirection = direction.Up; // UPDATE IF FORCE UP/DOWN
                    else if (rCol.GetComponent<PathCollision>().forceDown) testDirection = direction.Down; // UPDATE IF FORCE UP/DOWN
                    else
                    {
                        if (targetObject.transform.position.y > transform.position.y) testDirection = direction.Up;
                        else if (targetObject.transform.position.y < transform.position.y) testDirection = direction.Down;
                    }
                }
            }
            else rCol = null;
            if (testDirection != direction.Empty) currentStage = 2;
        }
        if (dCol == null && uCol == null && lCol == null && rCol == null)
        {
            newDirection = new Vector3(0, 0, 0);
            // CHECK LEFT/RIGHT
            if (targetObject.transform.position.x > transform.position.x) newDirection.x = 0.5f;
            else newDirection.x = -0.5f;
            // CHECK UP/DOWN
            if (targetObject.transform.position.y > transform.position.y) newDirection.y = 0.5f;
            else newDirection.y = -0.5f;

            acceptCustomLocation = true;
            if (newDirection.x > 0 && lCol != null) acceptCustomLocation = false;
            if (newDirection.x < 0 && rCol != null) acceptCustomLocation = false;
            if (newDirection.y > 0 && uCol != null) acceptCustomLocation = false;
            if (newDirection.y < 0 && dCol != null) acceptCustomLocation = false;
            if (acceptCustomLocation)
            {
                testDirection = direction.New;
                currentStage = 2;
            }
            else currentStage = 1;
        }
    }
    void SpawnNextLink() // 2
    {
        switch (testDirection)
        {
            case direction.Up:
                if (uCol != null) currentStage = 1;
                else nextLink = Instantiate(linkPrefab, testUp.transform.position, Quaternion.identity);
                break;
            case direction.Down:
                if (dCol != null) currentStage = 1;
                else nextLink = Instantiate(linkPrefab, testDown.transform.position, Quaternion.identity);
                break;
            case direction.Left:
                if (lCol != null) currentStage = 1;
                else nextLink = Instantiate(linkPrefab, testLeft.transform.position, Quaternion.identity);
                break;
            case direction.Right:
                if (rCol != null) currentStage = 1;
                else nextLink = Instantiate(linkPrefab, testRight.transform.position, Quaternion.identity);
                break;
            case direction.New:
                nextLink = Instantiate(linkPrefab, transform.position + newDirection, Quaternion.identity);
                break;
        }
        if (nextLink != null)
        {
            // PASS ON REQUIRED INFO TO NEXT LINK
            nextLink.GetComponent<PathController>().prevLink = this.gameObject;
            nextLink.GetComponent<PathController>().owningObject = owningObject;
            currentStage = 3; // STOP SCRIPT FROM UPDATING
        }
        else currentStage = 1;
    }
    void TestCollision()
    {
        if (childID < childMax && childMax == collisionParent.transform.childCount)
        {
            collisionObject = collisionParent.transform.GetChild(childID).gameObject; // GET COLLISION OBJECT
            TestCollider();
        }
        else // RESET WHEN CHECKING LAST CHILD OF PARENT
        {
            childID = -1;
            childMax = collisionParent.transform.childCount;
        }
        childID++;
    }
    void TestCollider()
    {
        // LEFT
        if (testLeft.GetComponent<SpriteRenderer>().bounds.Intersects(collisionObject.GetComponent<SpriteRenderer>().bounds)) // TEST COLLISION WITH THIS OBJECT
        {
            lCol = collisionObject;
        }

        // RIGHT
        if (testRight.GetComponent<SpriteRenderer>().bounds.Intersects(collisionObject.GetComponent<SpriteRenderer>().bounds)) // TEST COLLISION WITH THIS OBJECT
        {
            rCol = collisionObject;
        }

        // UP
        if (testUp.GetComponent<SpriteRenderer>().bounds.Intersects(collisionObject.GetComponent<SpriteRenderer>().bounds)) // TEST COLLISION WITH THIS OBJECT
        {
            uCol = collisionObject;
        }

        // DOWN
        if (testDown.GetComponent<SpriteRenderer>().bounds.Intersects(collisionObject.GetComponent<SpriteRenderer>().bounds)) // TEST COLLISION WITH THIS OBJECT
        {
            dCol = collisionObject;
        }
    }
    private void OnDestroy()
    {
        owningObject.GetComponent<PathMaster>().pathLinks--;
    }
}
