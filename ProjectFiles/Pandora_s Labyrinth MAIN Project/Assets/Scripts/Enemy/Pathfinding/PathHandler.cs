using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathHandler : MonoBehaviour
{
    public GameObject target, owner, nextLink, prevLink, prefab, playerRadius;
    public GameObject north, south, east, west, collisionObject, pathParent;
    public bool blockNorth, blockSouth, blockEast, blockWest, followX;
    int timer;
    float xDist, yDist;

    public enum stage {GetDirection, AdjustDirection, SpawnNewLink, SequenceComplete}
    public stage currentStage;
    public enum direction {North, South, East, West}
    public direction desiredDirection;

    // Start is called before the first frame update
    void Start()
    {
        this.name = "Path:" + owner.name;
        transform.parent = owner.GetComponent<PathMaster>().pathParent.transform;
        target = owner.GetComponent<PathMaster>().target;
        playerRadius = owner.GetComponent<PathMaster>().playerRadius;
        currentStage = stage.GetDirection;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        blockNorth = false;
        blockSouth = false;
        blockEast = false;
        blockWest = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (prevLink == null || owner == null)
            {
                Destroy(gameObject);
            }
            else if (timer < 3)
            {
                timer = 0;
                switch (currentStage)
                {
                    case stage.GetDirection:
                        GetDirection();
                        break;
                    case stage.AdjustDirection:
                        AdjustDirection();
                        break;
                    case stage.SpawnNewLink:
                        if (!GetComponent<SpriteRenderer>().bounds.Intersects(target.GetComponent<SpriteRenderer>().bounds)) SpawnNextLink();
                        break;
                    case stage.SequenceComplete:
                        if (nextLink != null && GetComponent<SpriteRenderer>().bounds.Intersects(owner.GetComponent<SpriteRenderer>().bounds))
                        {
                            owner.GetComponent<PathMaster>().nextLink = nextLink;
                            nextLink.GetComponent<PathHandler>().prevLink = owner;
                            Destroy(gameObject);
                        }
                        break;
                }
            }
            else timer++;
        }
    }
    public void ForceDestroy()
    {
        Destroy(gameObject);
    }
    void SpawnNextLink()
    {
        if (nextLink == null)
        {
            switch (desiredDirection)
            {
                case direction.North:
                    nextLink = Instantiate(prefab, north.transform.position, Quaternion.identity);
                    break;
                case direction.South:
                    nextLink = Instantiate(prefab, south.transform.position, Quaternion.identity);
                    break;
                case direction.East:
                    nextLink = Instantiate(prefab, east.transform.position, Quaternion.identity);
                    break;
                case direction.West:
                    nextLink = Instantiate(prefab, west.transform.position, Quaternion.identity);
                    break;
            }

            if (nextLink != null)
            {
                nextLink.GetComponent<PathHandler>().owner = owner;
                nextLink.GetComponent<PathHandler>().prevLink = this.gameObject;
            }
            owner.GetComponent<PathMaster>().totalLinks++;
            currentStage = stage.SequenceComplete;
        }
    }
    void GetDirection()
    {
        xDist = transform.position.x - target.transform.position.x;
        yDist = transform.position.y - target.transform.position.y;
        if (xDist < 0) xDist *= -1;
        if (yDist < 0) yDist *= -1;
        if (xDist > yDist) followX = true;
        else followX = false;

        if (followX)
        {
            if (target.transform.position.x > transform.position.x) desiredDirection = direction.East;
            else if (target.transform.position.x < transform.position.x) desiredDirection = direction.West;
        }
        else
        {
            if (target.transform.position.y > transform.position.y) desiredDirection = direction.North;
            else if (target.transform.position.y < transform.position.y) desiredDirection = direction.South;
        }
        if (blockNorth || blockSouth || blockEast || blockWest) currentStage = stage.AdjustDirection;
        else currentStage = stage.SpawnNewLink;
    }
    void AdjustDirection()
    {
        if (blockNorth && desiredDirection == direction.North)
        {
            if (collisionObject.GetComponent<PathColliderHandler>().forceSouth) desiredDirection = direction.South;
            else if (collisionObject.GetComponent<PathColliderHandler>().forceEast) desiredDirection = direction.East;
            else if (collisionObject.GetComponent<PathColliderHandler>().forceWest) desiredDirection = direction.West;
            else
            {
                if (target.transform.position.x > collisionObject.transform.position.x) desiredDirection = direction.East;
                else desiredDirection = direction.West;
            }
        }
        else if (blockSouth && desiredDirection == direction.South)
        {
            if (collisionObject.GetComponent<PathColliderHandler>().forceNorth) desiredDirection = direction.North;
            else if (collisionObject.GetComponent<PathColliderHandler>().forceEast) desiredDirection = direction.East;
            else if (collisionObject.GetComponent<PathColliderHandler>().forceWest) desiredDirection = direction.West;
            else
            {
                if (target.transform.position.x > collisionObject.transform.position.x) desiredDirection = direction.East;
                else desiredDirection = direction.West;
            }
        }
        else if (blockEast && desiredDirection == direction.East)
        {
            if (collisionObject.GetComponent<PathColliderHandler>().forceNorth) desiredDirection = direction.North;
            else if (collisionObject.GetComponent<PathColliderHandler>().forceSouth) desiredDirection = direction.South;
            else if (collisionObject.GetComponent<PathColliderHandler>().forceWest) desiredDirection = direction.West;
            else
            {
                if (target.transform.position.y > collisionObject.transform.position.y) desiredDirection = direction.North;
                else desiredDirection = direction.South;
            }
        }
        else if (blockWest && desiredDirection == direction.West)
        {
            if (collisionObject.GetComponent<PathColliderHandler>().forceNorth) desiredDirection = direction.North;
            else if (collisionObject.GetComponent<PathColliderHandler>().forceSouth) desiredDirection = direction.South;
            else if (collisionObject.GetComponent<PathColliderHandler>().forceEast) desiredDirection = direction.East;
            else
            {
                if (target.transform.position.y > collisionObject.transform.position.y) desiredDirection = direction.North;
                else desiredDirection = direction.South;
            }
        }
        currentStage = stage.SpawnNewLink;
    }
}
