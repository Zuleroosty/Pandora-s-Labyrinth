using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandorasBox : MonoBehaviour
{
    public GameObject minotaurObject;
    bool hasCollected;

    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (!hasCollected)
            {
                if (GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds))
                {
                    hasCollected = true;
                    GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().hasPandorasBox = true;
                    Instantiate(minotaurObject, new Vector3(transform.position.x + 5, transform.position.y, 0), Quaternion.identity);
                    GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().inCombat = true;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
