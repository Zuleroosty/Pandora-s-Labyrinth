using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateRoomConnections : MonoBehaviour
{
    public GameObject connectObject;
    public bool northConnection, southConnection, eastConnection, westConnection;

    public Sprite topBlocked, bottomBlocked, topOpen, bottomOpen;
    public Color leftBlockC, rightBlockC;
    public GameObject topSpriteObject, bottomSpriteObject, leftBlock, rightBlock;
    public GameObject topCol, topProCol, bottomCol, leftCol, rightCol;
    public bool deleteW, deleteE;

    int timer ,timeMax;

    private void Start()
    {
        leftBlockC = leftBlock.GetComponent<SpriteRenderer>().color;
        rightBlockC = rightBlock.GetComponent<SpriteRenderer>().color;

        BlockTopExit();
        BlockBottomExit();
        BlockRightExit();
        BlockLeftExit();

        timeMax = Random.Range(0,5);
    }

    private void Update()
    {
        if (timer < timeMax) timer++;
        if (timer >= timeMax)
        {
            timer = 0;
            timeMax = Random.Range(0, 5);
        }
        if (leftBlock != null) leftBlock.GetComponent<SpriteRenderer>().color = leftBlockC;
        if (rightBlock != null) rightBlock.GetComponent<SpriteRenderer>().color = rightBlockC;

        if (northConnection && topCol != null && topProCol != null) EnsureTopExit();
        if ((southConnection || this.name.Contains("Objective")) && bottomCol != null) EnsureBottomExit();
        if ((eastConnection || this.name.Contains("Spawn")) && rightCol != null) EnsureRightExit();
        if (westConnection && leftCol != null) EnsureLeftExit();

        if (deleteW && leftCol != null) Destroy(leftCol.gameObject);
        if (deleteE && rightCol != null) Destroy(rightCol.gameObject);
    }

    public void BlockTopExit()
    {
        if (topCol.GetComponent<CollisionManager>().disableCollision) topCol.GetComponent<CollisionManager>().disableCollision = false;
        if (topProCol.GetComponent<CollisionManager>().disableCollision) topProCol.GetComponent<CollisionManager>().disableCollision = false;
        if (!topSpriteObject.GetComponent<SpriteRenderer>().sprite.name.Contains("topBlocked")) topSpriteObject.GetComponent<SpriteRenderer>().sprite = topBlocked;
    }
    public void BlockBottomExit()
    {
        if (bottomCol.GetComponent<CollisionManager>().disableCollision) bottomCol.GetComponent<CollisionManager>().disableCollision = false;
        if (!bottomSpriteObject.GetComponent<SpriteRenderer>().sprite.name.Contains("bottomBlocked")) bottomSpriteObject.GetComponent<SpriteRenderer>().sprite = bottomBlocked;
    }
    public void BlockRightExit()
    {
        if (rightCol.GetComponent<CollisionManager>().disableCollision) rightCol.GetComponent<CollisionManager>().disableCollision = false;
        if (rightBlockC.a < 1) rightBlockC.a = 1;
    }
    public void BlockLeftExit()
    {
        if (leftCol.GetComponent<CollisionManager>().disableCollision) leftCol.GetComponent<CollisionManager>().disableCollision = false;
        if (leftBlockC.a < 1) leftBlockC.a = 1;
    }

    //------------------------------------------------------------------------

    public void EnsureTopExit()
    {
        Destroy(topCol.gameObject);
        Destroy(topProCol.gameObject);
        topSpriteObject.GetComponent<SpriteRenderer>().sprite = topOpen;
    }
    public void EnsureBottomExit()
    {
        Destroy(bottomCol.gameObject);
        bottomSpriteObject.GetComponent<SpriteRenderer>().sprite = bottomOpen;
    }
    public void EnsureRightExit()
    {
        deleteE = true;
        Destroy(rightBlock.gameObject);
    }
    public void EnsureLeftExit()
    {
        deleteW = true;
        Destroy(leftBlock.gameObject);
    }
}
