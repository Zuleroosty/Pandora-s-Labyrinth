using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public GameObject secondBlock, thirdBlock;
    public bool blockParent;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            transform.localScale = new Vector3(0.4f, 0.4f, 1);
            if (GameObject.Find("MouseCursor").GetComponent<SpriteRenderer>().bounds.Intersects(GetComponent<SpriteRenderer>().bounds))
            {
                transform.localScale = new Vector3(0.5f, 0.5f, 1);
                if (Input.GetKey(KeyCode.Mouse0)) transform.localScale = new Vector3(0.3f, 0.3f, 1);
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (!blockParent)
                    {
                        transform.parent.gameObject.GetComponent<BlockHandler>().currentNumber++;
                        if (transform.parent.gameObject.GetComponent<BlockHandler>().currentNumber > 9)
                        {
                            transform.parent.gameObject.GetComponent<BlockHandler>().currentNumber = 0;
                        }
                    }
                    if (secondBlock != null)
                    {
                        secondBlock.GetComponent<BlockHandler>().currentNumber++;
                        if (secondBlock.GetComponent<BlockHandler>().currentNumber > 9)
                        {
                            secondBlock.GetComponent<BlockHandler>().currentNumber = 0;
                        }
                    }
                    if (thirdBlock != null)
                    {
                        thirdBlock.GetComponent<BlockHandler>().currentNumber++;
                        if (thirdBlock.GetComponent<BlockHandler>().currentNumber > 9)
                        {
                            thirdBlock.GetComponent<BlockHandler>().currentNumber = 0;
                        }
                    }
                }
            }
        }
    }
}
