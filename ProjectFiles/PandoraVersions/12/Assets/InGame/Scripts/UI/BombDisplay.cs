using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDisplay : MonoBehaviour
{
    public GameObject circle1, circle2, circle3;
    public Sprite empty, full;
    public int currentBombs;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame )
        {
            currentBombs = GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().totalBombs;

            if (currentBombs > 0) circle1.GetComponent<SpriteRenderer>().sprite = full;
            else circle1.GetComponent<SpriteRenderer>().sprite = empty;

            if (currentBombs > 1) circle2.GetComponent<SpriteRenderer>().sprite = full;
            else circle2.GetComponent<SpriteRenderer>().sprite = empty;

            if (currentBombs > 2) circle3.GetComponent<SpriteRenderer>().sprite = full;
            else circle3.GetComponent<SpriteRenderer>().sprite = empty;
        }
    }
}
