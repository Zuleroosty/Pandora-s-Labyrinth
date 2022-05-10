using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotBridge : MonoBehaviour
{
    public GameObject slotItem, slotText;
    public int children;

    private void Update()
    {
        children = transform.childCount;
        if (children > 1)
        {
            if (transform.GetChild(1).gameObject != null)
            {
                slotItem = transform.GetChild(1).gameObject;
                slotItem.transform.localPosition = new Vector3(0, 0, -1);
                slotText.GetComponent<TextMesh>().text = slotItem.GetComponent<InvItemHandler>().itemName + " x" + slotItem.GetComponent<InvItemHandler>().itemQuantity;
            }
        }
        else
        {
            slotText.GetComponent<TextMesh>().text = "";
            slotItem = null;
        }
    }
}
