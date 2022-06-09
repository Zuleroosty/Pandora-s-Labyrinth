using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyChildrenOnReset : MonoBehaviour
{
    int childID, childMax;

    // Update is called once per frame
    void Update()
    {
        RemoveChildren(); // REPEATED FOR SPEED
        RemoveChildren();
        RemoveChildren();
        RemoveChildren();
    }
    void RemoveChildren()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Reset && transform.childCount > 0)
        {
            if (childID < childMax && childMax == transform.childCount)
            {
                Destroy(transform.GetChild(childID).gameObject);
            }
            else
            {
                childID = -1;
                childMax = transform.childCount;
            }
            childID++;
        }
    }
}
