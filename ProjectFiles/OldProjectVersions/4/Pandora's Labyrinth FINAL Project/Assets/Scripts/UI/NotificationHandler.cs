using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationHandler : MonoBehaviour
{
    public GameObject noti1, noti2, noti3, noti4, noti5;

    private void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().activeNotifications > 0)
        {
            if (noti1 == null) ShiftUp(null);
            else if (noti1.GetComponent<DisplayNotifier>().notificationID != 1) noti1.GetComponent<DisplayNotifier>().notificationID = 1;
            else if (noti2.GetComponent<DisplayNotifier>().notificationID != 2) noti2.GetComponent<DisplayNotifier>().notificationID = 2;
            else if (noti3.GetComponent<DisplayNotifier>().notificationID != 3) noti3.GetComponent<DisplayNotifier>().notificationID = 3;
            else if (noti4.GetComponent<DisplayNotifier>().notificationID != 4) noti4.GetComponent<DisplayNotifier>().notificationID = 4;
            else if (noti5.GetComponent<DisplayNotifier>().notificationID != 5) noti5.GetComponent<DisplayNotifier>().notificationID = 5;
        }
    }

    public void ShiftUp(GameObject newNotificationObject)
    {
        if (noti1 != null)
        {
            if (GameObject.Find(">GameManager<").GetComponent<GameManager>().activeNotifications > 5)
            {
                noti1.GetComponent<DisplayNotifier>().fadeIn = false;
                noti1.GetComponent<DisplayNotifier>().fadeOut = true;
                noti1.GetComponent<DisplayNotifier>().removeNotiCount = true;
                GameObject.Find(">GameManager<").GetComponent<GameManager>().activeNotifications--;
            }
            noti1 = null;
        }
        if (noti2 != null)
        {
            noti1 = noti2;
            noti1.GetComponent<DisplayNotifier>().notificationID = 1;
            noti2 = null;
        }
        if (noti3 != null)
        {
            noti2 = noti3;
            noti2.GetComponent<DisplayNotifier>().notificationID = 2;
            noti3 = null;
        }
        if (noti4 != null)
        {
            noti3 = noti4;
            noti3.GetComponent<DisplayNotifier>().notificationID = 3;
            noti4 = null;
        }
        if (noti5 != null)
        {
            noti4 = noti5;
            noti4.GetComponent<DisplayNotifier>().notificationID = 4;
            noti5 = null;
        }
        if (newNotificationObject.gameObject != null)
        {
            noti5 = newNotificationObject;
            noti5.GetComponent<DisplayNotifier>().notificationID = 5;
        }
    }
}
