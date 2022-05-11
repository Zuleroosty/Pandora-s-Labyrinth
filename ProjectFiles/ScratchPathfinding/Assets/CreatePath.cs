using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePath : MonoBehaviour
{
    public GameObject nextObject, prevObject, checkObject, targetObject, newPathObject;
    public bool isColliding, dirUpdated, dirConfirmed, isBlocked, isFirst;
    public Vector3 testDirection;
    int timer;

    private void Start()
    {
        targetObject = GameObject.Find("EndSquare");
        transform.parent = GameObject.Find("----Pathfinding----").transform;
        this.name = "NewPath";

        GetComponent<RotateTo>().enabled = false;

        dirUpdated = false;
        isBlocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 5) timer++;
        else
        {
            timer = 0;
            if (isBlocked)
            {
                if (nextObject != null) Destroy(nextObject);
                prevObject.GetComponent<CreatePath>().BlockedDirectionCol();
                isBlocked = false;
                dirUpdated = true;
            }
            else if (nextObject == null)
            {
                if (!dirUpdated)
                {
                    testDirection = new Vector3(0, 0, 0);
                    if (targetObject.transform.position.x > transform.position.x + 0.5f) testDirection.x = 0.5f; // RIGHT
                    else if (targetObject.transform.position.x < transform.position.x - 0.5f) testDirection.x = -0.5f; // LEFT
                    if (targetObject.transform.position.y > transform.position.y + 0.5f) testDirection.y = 1.5f; // UP
                    else if (targetObject.transform.position.y < transform.position.y - 0.5f) testDirection.y = -1.5f; // DOWN
                    dirUpdated = true;
                }
                else if (!GetComponent<SpriteRenderer>().bounds.Intersects(GameObject.Find("B").GetComponent<SpriteRenderer>().bounds))
                {
                    nextObject = Instantiate(newPathObject, checkObject.transform.position, Quaternion.identity);
                    nextObject.GetComponent<CreatePath>().prevObject = this.gameObject;
                    GetComponent<RotateTo>().enabled = true;
                    GetComponent<RotateTo>().target = nextObject;
                }
                checkObject.transform.localPosition = testDirection;
            }
            if (GameObject.Find("A").GetComponent<EnemyAI>().startObject == null || prevObject == null) Destroy(gameObject);
        }
    }
    public void BlockedDirectionCol()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (nextObject != null) Destroy(nextObject);
        if (checkObject.transform.position.x < transform.position.x) // LEFT
        {
            if (checkObject.transform.position.y > transform.position.y) // UP
            {
                testDirection.y = 3.75f;
                testDirection.x = 0;
            }
            else if (checkObject.transform.position.y < transform.position.y) // DOWN
            {
                testDirection.y = -3.75f;
                testDirection.x = 0;
            }
        }
        else if (checkObject.transform.position.y < transform.position.y) // DOWN
        {
            if (checkObject.transform.position.x < transform.position.x) // RIGHT
            {
                testDirection.y = 0;
                testDirection.x = 0.75f;
            }
            else if (checkObject.transform.position.x > transform.position.x) // LEFT
            {
                testDirection.y = 0;
                testDirection.x = -0.75f;
            }
        }
        else if (checkObject.transform.position.x > transform.position.x) // RIGHT
        {
            if (checkObject.transform.position.y > transform.position.y) // UP
            {
                testDirection.y = 3.75f;
                testDirection.x = 0;
            }
            else if (checkObject.transform.position.y < transform.position.y) // DOWN
            {
                testDirection.y = -3.75f;
                testDirection.x = 0;
            }
        }
        else if (checkObject.transform.position.y > transform.position.y) // UP
        {
            if (checkObject.transform.position.x < transform.position.x) // RIGHT
            {
                testDirection.y = 0;
                testDirection.x = 0.75f;
            }
            else if (checkObject.transform.position.x > transform.position.x) // LEFT
            {
                testDirection.y = 0;
                testDirection.x = -0.75f;
            }
        }
        checkObject.transform.localPosition = testDirection;
    }
}
