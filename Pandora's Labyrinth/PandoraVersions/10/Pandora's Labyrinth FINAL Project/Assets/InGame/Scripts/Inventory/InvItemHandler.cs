using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvItemHandler : MonoBehaviour
{
    public int itemID, itemQuantity;
    public Sprite itemSprite;
    public string itemName, itemLore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (itemQuantity <= 0) Destroy(gameObject);
    }
}
