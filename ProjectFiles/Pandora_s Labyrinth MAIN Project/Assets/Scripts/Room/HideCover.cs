using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCover : MonoBehaviour
{
    public GameObject roomCover, gameManager;
    Color coverColour;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find(">GameManager<");
        if (roomCover != null) coverColour = roomCover.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds))
        {
            gameManager.GetComponent<GameManager>().currentRoomParent = this.gameObject;
            coverColour.a = 0.2f;
        }
        else
        {
            coverColour.a = 1;
        }
        roomCover.GetComponent<SpriteRenderer>().color = coverColour;
    }
}
