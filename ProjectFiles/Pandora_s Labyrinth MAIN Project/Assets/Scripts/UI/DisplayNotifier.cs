using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayNotifier : MonoBehaviour
{
    public string displayText;
    public Vector3 spawnPos;
    public float displayTime, boxPosX, boxPosY, travelDist;
    public int childID, childMax, thisID, displayTimeTotal;
    bool fadeOut;

    private void Start()
    {
        transform.parent = GameObject.Find("----NotificationLocation----").transform;
        childMax = transform.parent.childCount;
        thisID = transform.parent.childCount;
        boxPosY = (thisID * 1.5f);
        spawnPos = transform.localPosition;
        travelDist = -1.6f;
        displayTimeTotal = 60;
        transform.localScale = new Vector3(0.065f, 0.065f, 1);
    }

    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (!transform.parent.name.Contains("KillBox"))
            {
                UpdateID();
                boxPosY = (thisID * -1.5f); // UPDATE LOCATION BASED ON ID
                if (boxPosY < transform.localPosition.y) transform.localPosition -= new Vector3(0, 0.1f, 0);
                if (boxPosY > transform.localPosition.y) transform.localPosition += new Vector3(0, 0.1f, 0);
            }
            if (!fadeOut)
            {
                if (transform.localPosition.x > travelDist) transform.localPosition -= new Vector3(0.085f, 0, 0);
                else fadeOut = true;
            }
            else if (thisID == 0)
            {
                if (GameObject.Find("----NotificationLocation----").transform.childCount > 5) displayTime = displayTimeTotal;
                if (displayTime < displayTimeTotal) displayTime++;
                if (displayTime >= displayTimeTotal)
                {
                    if (!transform.parent.name.Contains("KillBox")) transform.parent = GameObject.Find("----NotiKillBox----").transform;
                    if (transform.localPosition.x < 0) transform.localPosition += new Vector3(0.035f, 0, 0);
                    else Destroy(gameObject);
                }
            }
            if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Reset) Destroy(gameObject);
        }
        else Destroy(gameObject);
    }
    void UpdateID()
    {
        if (childID < childMax && childMax == GameObject.Find("----NotificationLocation----").transform.childCount)
        {
            if (GameObject.Find("----NotificationLocation----").transform.GetChild(childID).gameObject == this.gameObject)
            {
                thisID = childID - 1;
            }
        }
        else
        {
            childID = -1;
            childMax = GameObject.Find("----NotificationLocation----").transform.childCount;
        }
        childID++;
    }
}
