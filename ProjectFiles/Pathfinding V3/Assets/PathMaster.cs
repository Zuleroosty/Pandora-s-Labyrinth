using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMaster : MonoBehaviour
{
    public int pathLinks, newPathTimer, smoothMovement;
    public GameObject nextLink, targetObject, linkPrefab, playerRadius;
    public bool pathConnectToTarget, canMove;
    int pathTimer;

    private void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;

        smoothMovement = 6;

        CreateNewPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject == null) targetObject = GameObject.Find("Player");
        else
        {
            if (pathTimer < 120) pathTimer++;
            if (pathTimer >= 120)
            {
                pathTimer++;
                if (!pathConnectToTarget || nextLink == null)
                {
                    //if (pathLinks > 6) CreateNewPath(); // CREATE NEW PATH IF NOT CONNECTED TO TARGET
                }
            }
            if (nextLink != null && canMove) transform.position = Vector3.Lerp(transform.position, nextLink.transform.position, smoothMovement * Time.deltaTime); // FOLLOW PATH
        }
        if (targetObject.GetComponent<SpriteRenderer>().bounds.Intersects(playerRadius.GetComponent<SpriteRenderer>().bounds)) canMove = false;
        else canMove = true;
    }
    void CreateNewPath()
    {
        // CREATE NEW PATH
        if (nextLink != null) Destroy(nextLink);
        nextLink = Instantiate(linkPrefab, transform.position, Quaternion.identity);
        nextLink.GetComponent<PathController>().prevLink = this.gameObject;
        nextLink.GetComponent<PathController>().owningObject = this.gameObject;
    }
    public void GetNextLink(GameObject oldLink)
    {
        nextLink = oldLink.GetComponent<PathController>().nextLink;
        Destroy(oldLink);
    }
}
