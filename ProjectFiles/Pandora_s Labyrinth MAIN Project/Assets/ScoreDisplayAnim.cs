using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplayAnim : MonoBehaviour
{
    public int scoreToDisplay, timer;

    Color thisColour;
    Vector3 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        thisColour = GetComponent<TextMesh>().color;
        GetComponent<TextMesh>().text = "+" + scoreToDisplay.ToString();
        GameObject.Find(">GameManager<").GetComponent<StatHandler>().playerScore += scoreToDisplay;
        transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0.025f, 0);
        if (timer < 90) timer++;
        if (timer >= 90)
        {
            if (thisColour.a > 0) thisColour.a -= 0.025f;
            else Destroy(this.gameObject);
        }
        GetComponent<TextMesh>().color = thisColour;
    }
}
