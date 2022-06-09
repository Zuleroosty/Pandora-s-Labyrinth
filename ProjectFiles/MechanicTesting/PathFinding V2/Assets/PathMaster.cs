using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMaster : MonoBehaviour
{
    public GameObject nextLink, newLink, connectingPlayerPath;
    public int currentLinks;
    float moveSpeed;
    int newPathTimer;

    private void Start()
    {
        moveSpeed = 0.05f;    
    }

    // Update is called once per frame
    void Update()
    {
        if (connectingPlayerPath == null)
        {
            if (newPathTimer < 120) newPathTimer++;
            if (newPathTimer >= 120 || nextLink == null)
            {
                newPathTimer = 0;
                if (nextLink != null) Destroy(nextLink.gameObject);
                CreatePath();
            }
        }
        else if (!connectingPlayerPath.GetComponent<SpriteRenderer>().bounds.Intersects(GameObject.Find("PathRadius").GetComponent<SpriteRenderer>().bounds)) connectingPlayerPath = null;

        if (nextLink != null)
        {
            if (currentLinks > 0)
            {
                if (nextLink.transform.position.x < transform.position.x) transform.position -= new Vector3(moveSpeed, 0, 0);
                if (nextLink.transform.position.x > transform.position.x) transform.position += new Vector3(moveSpeed, 0, 0);
                if (nextLink.transform.position.y < transform.position.y) transform.position -= new Vector3(0, moveSpeed, 0);
                if (nextLink.transform.position.y > transform.position.y) transform.position += new Vector3(0, moveSpeed, 0);
            }
            if (this.GetComponent<SpriteRenderer>().bounds.Intersects(nextLink.GetComponent<SpriteRenderer>().bounds) && currentLinks > 2) nextLink.GetComponent<PathController>().RemoveOnContact();
        }
    }
    
    void CreatePath()
    {
        nextLink = Instantiate(newLink, transform.position, Quaternion.identity).gameObject;
        nextLink.GetComponent<PathController>().prevLink = this.gameObject;
        nextLink.GetComponent<PathController>().owner = this.gameObject;
    }
}
