using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationHandler : MonoBehaviour
{
    int childID, childMax;

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount > 0)
        {
            UpdateNotiPos();
            UpdateNotiPos();
        }
    }
    void UpdateNotiPos()
    {
        if (childID < childMax && childMax == transform.childCount)
        {
            if (transform.childCount <= 5)
            {
                if (transform.GetChild(childID).gameObject != null)
                {
                    transform.GetChild(childID).gameObject.GetComponent<DisplayNotifier>().thisID = childID;
                }
            }
            else transform.GetChild(childID).gameObject.transform.parent = GameObject.Find("---KillBox---").transform;
        }
        else
        {
            childID = -1;
            childMax = transform.childCount;
        }
        childID++;
    }
}
