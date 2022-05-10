using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvUIHandler : MonoBehaviour // ----- BASIC VERSION
{
    public GameObject slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8;
    int itemCount, itemMax;

    // Start is called before the first frame update
    void Start()
    {
        itemMax = 8;
        itemCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddToInventory(GameObject objectToAdd)
    {
        if (itemCount < itemMax)
        {
            itemCount++;
            if (objectToAdd.name.Contains("Ammo"))
            {
                GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().ammo += objectToAdd.GetComponent<AmmoPickup>().ammoAmount;
                if (GameObject.Find("Slot (1)").transform.childCount > 1)
                {
                    Destroy(objectToAdd.gameObject);
                }
                else
                {
                    slot1 = objectToAdd;
                    objectToAdd.transform.parent = GameObject.Find("Slot (1)").transform;
                    objectToAdd.transform.localPosition = new Vector3(0, 0, -1);
                }
            }
            else if (objectToAdd.name.Contains("Health"))
            {

            }
            else
            {

            }
        }
        else //---- CAN NOT ADD TO INVENTORY / NOT ENOUGH SPACE
        {

        }
    }
}
