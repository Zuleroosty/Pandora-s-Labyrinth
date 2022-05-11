using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount;
    bool hasCollected;

    // Start is called before the first frame update
    void Start()
    {
        ammoAmount = Random.Range(10, 15);
        transform.parent = GameObject.Find("----Loot----").transform;

        GetComponent<InvItemHandler>().itemQuantity = ammoAmount;
        GetComponent<InvItemHandler>().itemName = "Ammo";
        GetComponent<InvItemHandler>().itemLore = "Used to attack Enemies";
        GetComponent<InvItemHandler>().itemID = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasCollected)
        {
            if (GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds))
            {
                hasCollected = true;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().ammo += ammoAmount;
        GameObject.Find(">GameManager<").GetComponent<GameManager>().NewNotification("+" + ammoAmount + " Ammo");
    }
}
