using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDisplay : MonoBehaviour
{
    public GameObject circle1, circle2, circle3;
    public Sprite empty, full;
    public int currentPotions;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            currentPotions = GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().totalMedkits;

            if (currentPotions > 0) circle1.GetComponent<SpriteRenderer>().sprite = full;
            else circle1.GetComponent<SpriteRenderer>().sprite = empty;

            if (currentPotions > 1) circle2.GetComponent<SpriteRenderer>().sprite = full;
            else circle2.GetComponent<SpriteRenderer>().sprite = empty;

            if (currentPotions > 2) circle3.GetComponent<SpriteRenderer>().sprite = full;
            else circle3.GetComponent<SpriteRenderer>().sprite = empty;
        }
    }
}
