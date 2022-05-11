using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    public GameObject prevLink, nextLink, target, newLink, owner;
    public bool locationUpdated, lockLink, forceDirection;
    public Vector3 locationCheck;
    public int delayTimer, spawnTimer, dirForce; // ALLOWS FOR 'MORE' ACCURATE COLLISION // 1 = up 2 = down 3 = right 4 = left
    public string prevDirection;
    public float xDist, yDist;

    private void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
        transform.parent = GameObject.Find("----PathFinding----").transform;
        this.name = "PathLink";
        locationUpdated = false;
        lockLink = false;
        owner.GetComponent<PathMaster>().currentLinks++;
    }

    // Update is called once per frame
    void Update()
    {
        if (prevLink != null)
        {
            if (!lockLink)
            {
                if (delayTimer < 2 && nextLink == null) delayTimer++; // BASIC TIMER
                else
                {
                    delayTimer = 0; // TIMER RESET
                    if (spawnTimer < 3) spawnTimer++;
                    else
                    {
                        spawnTimer = 0;
                        if (target == null) target = GameObject.Find("Player"); // LOCATE FINAL DESTINATION
                        if (!this.GetComponent<SpriteRenderer>().bounds.Intersects(target.GetComponent<SpriteRenderer>().bounds)) CheckLocation(); // CONTINUE IF NOT COLLIDING WITH TARGET
                        if (this.GetComponent<SpriteRenderer>().bounds.Intersects(GameObject.Find("PathRadius").GetComponent<SpriteRenderer>().bounds)) owner.GetComponent<PathMaster>().connectingPlayerPath = this.gameObject; // SET AS CONNECTING PATH TO PLAYER RADIUS
                    }
                }
            }
        }
        else Destroy(gameObject); // DESTROY IF NO PREV LINK

    }
    public void RemoveOnContact() // DESTROY AND CONNECT NEXT LINK TO OWNER
    {
        if (prevLink != null)
        {
            if (nextLink != null) nextLink.GetComponent<PathController>().prevLink = owner;
            if (owner == prevLink) owner.GetComponent<PathMaster>().nextLink = nextLink.gameObject;
        }
        Destroy(gameObject);
    }
    void CheckLocation() // CHECK PLACEMENT IN 8 DIRECTIONS FROM PREV LINK LOCATION USING VECTOR3
    {
        if (!locationUpdated)
        {
            if (forceDirection)
            {
                switch (dirForce)
                {
                    case 1: // UP
                        locationCheck = new Vector3(0, 1.25f, 0);
                        break;
                    case 2: // DOWN
                        locationCheck = new Vector3(0, -1.25f, 0);
                        break;
                    case 3: // RIGHT
                        locationCheck = new Vector3(1.25f, 0, 0);
                        break;
                    case 4: // LEFT
                        locationCheck = new Vector3(-1.25f, 0, 0);
                        break;
                }
            }
            else
            {
                locationCheck = new Vector3(0, 0, 0);

                xDist = target.transform.position.x - owner.transform.position.x;
                yDist = target.transform.position.y - owner.transform.position.y;
                if (xDist < 0) xDist *= -1;
                if (yDist < 0) yDist *= -1;

                // MOVE TO DESIRED LOCATION
                if (xDist < yDist || yDist < 1)
                {
                    
                }
                else
                {
                    
                }
                if (target.transform.position.x > transform.position.x) locationCheck.x = 1.25f; // RIGHT
                if (target.transform.position.x < transform.position.x) locationCheck.x = -1.25f; // LEFT
                if (target.transform.position.y > transform.position.y) locationCheck.y = 1.25f; // UP
                if (target.transform.position.y < transform.position.y) locationCheck.y = -1.25f; // DOWN
            }
            transform.position = prevLink.transform.position + locationCheck; // MOVE TO NEW LOCATION AND CREATE NEW LINK
        }
        locationUpdated = true;
        forceDirection = false;
        if (owner.GetComponent<PathMaster>().currentLinks < 5) CreateNewLink();
    }
    void CreateNewLink()
    {
        nextLink = Instantiate(newLink, transform.position, Quaternion.identity);
        nextLink.GetComponent<PathController>().prevLink = this.gameObject;
        nextLink.GetComponent<PathController>().owner = owner;
    }
    private void OnDestroy()
    {
        owner.GetComponent<PathMaster>().currentLinks--;
    }
}
