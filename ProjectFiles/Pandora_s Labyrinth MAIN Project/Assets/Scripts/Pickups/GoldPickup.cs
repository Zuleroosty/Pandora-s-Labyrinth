using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickup : MonoBehaviour
{
    public GameObject colliderBox;
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
                GameObject.Find(">GameManager<").GetComponent<GameManager>().NewNotification("+ 1", 3);
                GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerFXHandler>().PlayGoldPickup();
                Destroy(this.gameObject);
            }
            else if (!hasCollected)
            {
                if (GameObject.Find("PCollision").GetComponent<SpriteRenderer>().bounds.Intersects(colliderBox.GetComponent<SpriteRenderer>().bounds))
                {
                    hasCollected = true;
                    GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().gold++;
                }
            }
        }
        else Destroy(gameObject);
    }
}
