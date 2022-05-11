using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPPickup : MonoBehaviour
{
    public bool hasCollected;
    int xpAmount;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = GameObject.Find("----Loot----").transform;
        xpAmount = Random.Range(5, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.GenLevel)
        {
            Destroy(gameObject);
        }
        else
        {
            if (hasCollected)
            {
                if (GetComponent<SpriteRenderer>().bounds.Intersects(GameObject.Find("AttractionBar").GetComponent<SpriteRenderer>().bounds))
                {
                    GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().xp += xpAmount;
                    Destroy(gameObject);
                }
            }
            else
            {
                if (GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds))
                {
                    hasCollected = true;
                }
            }
        }
    }
}
