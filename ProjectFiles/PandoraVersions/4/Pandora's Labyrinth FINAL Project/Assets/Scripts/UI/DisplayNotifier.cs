using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayNotifier : MonoBehaviour
{
    public string displayText;
    public int notificationID;
    public Vector3 spawnPos;
    public float displayTime, boxPosX, boxPosY;
    public bool fadeIn, fadeOut, hasID, removeNotiCount;

    private void Start()
    {
        transform.parent = GameObject.Find("Main Camera").transform;
        transform.localPosition = new Vector3(22, 0, 5);
    }

    void FixedUpdate()
    {
        if (notificationID > 0 && !hasID)
        {
            boxPosY = (notificationID * 1.2f) * -1;
            transform.localPosition = new Vector3(22, boxPosY, 5);
            spawnPos = transform.localPosition;
            DisplayNotification();
        }
        if (hasID)
        {
            boxPosY = (notificationID * 1.2f) * -1;
            if (boxPosY > transform.localPosition.y) transform.localPosition += new Vector3(0, 0.1f, 0);
            if (fadeIn && transform.localPosition.x > boxPosX) transform.localPosition -= new Vector3(0.5f, 0, 0);
            if (fadeIn && transform.localPosition.x <= boxPosX)
            {
                if (displayTime < 120) displayTime++;
                if (displayTime >= 120)
                {
                    if (!removeNotiCount) GameObject.Find(">GameManager<").GetComponent<GameManager>().activeNotifications--;
                    GameObject.Find(">GameManager<").GetComponent<NotificationHandler>().noti1 = null;
                    fadeIn = false;
                    fadeOut = true;
                }
            }
            if (fadeOut && transform.localPosition.x < spawnPos.x) transform.localPosition += new Vector3(0.5f, 0, 0);
            if (fadeOut && transform.localPosition.x >= spawnPos.x)
            {
                Destroy(gameObject);
            }
        }
    }

    private void DisplayNotification()
    {
        hasID = true;
        boxPosX = 12;
        fadeIn = true;
    }
}
