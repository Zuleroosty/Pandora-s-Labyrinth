using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandorasBox : MonoBehaviour
{
    public GameObject minotaurObject;
    bool hasCollected;

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
        GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().hasPandorasBox = true;
        Instantiate(minotaurObject, new Vector3(transform.position.x, transform.position.y + 5, 0), Quaternion.identity);
        GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().inCombat = true;
    }
}
