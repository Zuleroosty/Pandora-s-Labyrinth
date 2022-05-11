using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandorasBox : MonoBehaviour
{
    public GameObject minotaurObject, spawnLocation;
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
                    GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerFXHandler>().PlayPandoraPickup();
                    GameObject.Find(">GameManager<").GetComponent<GameManager>().UpdateObjective("- EXIT LABYRINTH\n- KILL THE MINOTAUR (OPTIONAL)");
                    GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().hasPandorasBox = true;
                    GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().inCombat = true;
                }
            }
            else
            {
                Instantiate(minotaurObject, spawnLocation.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
