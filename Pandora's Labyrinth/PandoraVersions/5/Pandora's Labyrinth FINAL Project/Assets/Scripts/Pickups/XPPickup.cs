using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPPickup : MonoBehaviour
{
    int xpAmount;
    bool hasCollected;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = GameObject.Find("----Loot----").transform;
        xpAmount = Random.Range(5, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollected)
        {
            GameObject.Find(">GameManager<").GetComponent<GameManager>().NewNotification("+" + xpAmount + " XP");
            Destroy(this.gameObject);
        }
        else if (!hasCollected)
        {
            if (GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds))
            {
                hasCollected = true;
                GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().xp += xpAmount;
            }
        }
    }
}
