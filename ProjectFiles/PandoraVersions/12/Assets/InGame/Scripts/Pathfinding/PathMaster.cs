using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMaster : MonoBehaviour
{
    public int pathLinks, newPathTimer, smoothMovement;
    public GameObject nextLink, targetObject, linkPrefab, playerRadius, zObject, objectMaster;
    public bool pathConnectToTarget, canMove, fast, slow, ranged;
    int pathTimer, frameCheck;
    float rangedRange;
    Vector3 nextLocation;

    private void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;

        if (fast) smoothMovement = 8;
        if (slow) smoothMovement = 5;
        if (ranged) smoothMovement = 3;
        rangedRange = 5;

        CreateNewPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (targetObject == null) targetObject = GameObject.Find("PCollision");
            if (pathTimer < 120) pathTimer++;
            if (pathTimer >= 120)
            {
                pathTimer = 0;
                //if (!pathConnectToTarget) CreateNewPath();
            }
            if (frameCheck < 2) frameCheck++;
            if (frameCheck >= 2)
            {
                frameCheck = 0;
                if (GetComponent<SpriteRenderer>().bounds.Intersects(nextLink.GetComponent<SpriteRenderer>().bounds))
                {
                    if (nextLink.GetComponent<PathController>().currentStage == 3) GetNextLink(nextLink);
                }
            }
            if (ranged)
            {
                if (!targetObject.GetComponent<SpriteRenderer>().bounds.Intersects(playerRadius.GetComponent<SpriteRenderer>().bounds) && nextLink != null)
                {
                    canMove = true;
                    if (nextLink == null) CreateNewPath();
                }
                else
                {
                    canMove = false;
                }
            }
            else
            {
                if (pathLinks > 1 && nextLink != null) canMove = true;
                else canMove = false;
            }
            if (canMove)
            {
                nextLocation = new Vector3(nextLink.transform.position.x, nextLink.transform.position.y + 0.2f, zObject.transform.position.z);
                objectMaster.transform.position = Vector3.Lerp(objectMaster.transform.position, nextLocation, smoothMovement * Time.deltaTime); // FOLLOW PATH
            }
        }
    }
    void CreateNewPath()
    {
        // CREATE NEW PATH
        nextLink = Instantiate(linkPrefab, transform.position, Quaternion.identity);
        nextLink.GetComponent<PathController>().prevLink = this.gameObject;
        nextLink.GetComponent<PathController>().owningObject = this.gameObject;
    }
    public void GetNextLink(GameObject oldLink)
    {
        if (pathLinks > 1)
        {
            nextLink.GetComponent<PathController>().prevLink = null;
            nextLink = nextLink.GetComponent<PathController>().nextLink;
            nextLink.GetComponent<PathController>().prevLink = this.gameObject;
        }
    }
}
