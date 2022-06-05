using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public bool isPositive;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (GameObject.Find("MouseCursor").GetComponent<SpriteRenderer>().bounds.Intersects(GetComponent<SpriteRenderer>().bounds))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {

                }
            }
        }
    }
}
