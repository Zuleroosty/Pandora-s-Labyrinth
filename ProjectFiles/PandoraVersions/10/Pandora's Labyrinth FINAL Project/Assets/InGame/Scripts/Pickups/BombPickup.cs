using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPickup : MonoBehaviour
{
    bool hasCollected;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = GameObject.Find("----Loot----").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollected)
        {
            GameObject.Find(">GameManager<").GetComponent<GameManager>().NewNotification("+ 1 Bomb");
            Destroy(gameObject);
        }
        if (!hasCollected)
        {
            if (GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().totalBombs < GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().maxBombs)
            {
                if (GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds))
                {
                    hasCollected = true;
                    GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().totalBombs++;
                }
            }
        }
    }
}
