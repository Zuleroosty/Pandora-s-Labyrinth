using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    public GameObject player, connectedBlock;
    SpriteRenderer playerSprite, selfSprite, blockSprite;
    bool isCollected, isComplete;
    enum currentState {Dropped, Collected};
    currentState state;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerSprite = player.GetComponent<SpriteRenderer>();
        blockSprite = connectedBlock.GetComponent<SpriteRenderer>();
        selfSprite = GetComponent<SpriteRenderer>();

        state = currentState.Dropped;

        GameObject.Find("GameManager").GetComponent<GameManager>().totalBlocks++;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isComplete)
        {
            if (selfSprite.bounds.Intersects(playerSprite.bounds) && Input.GetKeyDown(KeyCode.Space))
            {
                if (state == currentState.Dropped) state = currentState.Collected;
                else if (state == currentState.Collected) state = currentState.Dropped;
            }
            if (state == currentState.Collected)
            {
                transform.position = new Vector3(player.transform.position.x + 0.5f, player.transform.position.y);
                
            }
            if (state == currentState.Dropped)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y);
                if (selfSprite.bounds.Intersects(blockSprite.bounds))
                {
                    isComplete = true;
                    transform.position = connectedBlock.transform.position;
                    GameObject.Find("GameManager").GetComponent<GameManager>().finishedBlocks++;
                }
            }
        }
    }
}
