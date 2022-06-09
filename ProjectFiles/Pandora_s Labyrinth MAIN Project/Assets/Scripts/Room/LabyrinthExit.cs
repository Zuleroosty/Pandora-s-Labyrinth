using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthExit : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().hasPandorasBox)
            {
                if (!transform.parent.GetComponent<CollisionManager>().disableCollision) transform.parent.GetComponent<CollisionManager>().disableCollision = true;
                if (GameObject.Find("PCollision").GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds))
                {
                    GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState = GameManager.state.Win;
                }
            }
        }
    }
}
