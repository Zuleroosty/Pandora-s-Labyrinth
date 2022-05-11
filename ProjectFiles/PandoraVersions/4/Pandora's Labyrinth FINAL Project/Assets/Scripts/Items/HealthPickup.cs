using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    int healthAmount;
    bool hasCollected;

    // Start is called before the first frame update
    void Start()
    {
        healthAmount = Random.Range(50, 101);
        transform.parent = GameObject.Find("----Loot----").transform;

        GetComponent<InvItemHandler>().itemQuantity = healthAmount / 50;
        GetComponent<InvItemHandler>().itemName = "Medkit";
        GetComponent<InvItemHandler>().itemLore = "Used to heal yourself";
        GetComponent<InvItemHandler>().itemID = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollected)
        {
            GameObject.Find(">GameManager<").GetComponent<GameManager>().NewNotification("+" + healthAmount + " Health");
            Destroy(this.gameObject);
        }
        else if (!hasCollected)
        {
            if (GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds))
            {
                hasCollected = true;
                GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().health += healthAmount;
            }
        }
    }
}
