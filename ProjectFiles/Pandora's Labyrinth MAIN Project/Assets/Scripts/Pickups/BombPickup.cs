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
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame || GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Pause)
        {
            if (hasCollected)
            {
                GameObject.Find(">GameManager<").GetComponent<GameManager>().NewNotification("+ 1", 2);
                GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerFXHandler>().PlayItemPickup();
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
        else Destroy(gameObject);
    }
}
