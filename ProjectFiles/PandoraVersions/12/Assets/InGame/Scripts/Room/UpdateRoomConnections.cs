using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateRoomConnections : MonoBehaviour
{
    public Sprite topBlocked, bottomBlocked, topOpen, bottomOpen;
    public GameObject topSpriteObject, bottomSpriteObject, leftBlock, rightBlock;
    public GameObject topCol, topProCol, bottomCol, leftCol, rightCol;

    public void BlockTopExit()
    {
        if (name.Contains("U"))
        {
            topCol.GetComponent<CollisionManager>().disableCollision = false;
            topProCol.GetComponent<CollisionManager>().disableCollision = false;
            topSpriteObject.GetComponent<SpriteRenderer>().sprite = topBlocked;
        }
    }
    public void BlockBottomExit()
    {
        if (name.Contains("D"))
        {
            bottomCol.GetComponent<CollisionManager>().disableCollision = false;
            bottomSpriteObject.GetComponent<SpriteRenderer>().sprite = bottomBlocked;
        }
    }
    public void BlockLeftExit()
    {
        if (name.Contains("L"))
        {
            leftCol.GetComponent<CollisionManager>().disableCollision = false;
            leftBlock.GetComponent<HideOnPlay>().ignore = true;
        }
    }
    public void BlockRightExit()
    {
        if (name.Contains("R"))
        {
            rightCol.GetComponent<CollisionManager>().disableCollision = false;
            rightBlock.GetComponent<HideOnPlay>().ignore = true;
        }
    }

    // FORCE ESIT CONNECTIONS BETWEEN ROOMS

    public void EnsureBottomExit()
    {
        if (name.Contains("D"))
        {
            bottomCol.GetComponent<CollisionManager>().disableCollision = true;
            bottomSpriteObject.GetComponent<SpriteRenderer>().sprite = bottomOpen;
        }
    }
    public void EnsureTopExit()
    {
        if (name.Contains("U"))
        {
            topCol.GetComponent<CollisionManager>().disableCollision = true;
            topProCol.GetComponent<CollisionManager>().disableCollision = true;
            topSpriteObject.GetComponent<SpriteRenderer>().sprite = topOpen;
        }
    }
    public void EnsureRightExit()
    {
        if (name.Contains("R"))
        {
            rightCol.GetComponent<CollisionManager>().disableCollision = true;
            rightBlock.GetComponent<HideOnPlay>().ignore = false;
        }
    }
    public void EnsureLeftExit()
    {
        if (name.Contains("L"))
        {
            leftCol.GetComponent<CollisionManager>().disableCollision = true;
            leftBlock.GetComponent<HideOnPlay>().ignore = false;
        }
    }
}
