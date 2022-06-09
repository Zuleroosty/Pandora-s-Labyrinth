using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderExtension : MonoBehaviour
{
    private int maxY;
    // Update is called once per frame
    void Update()
    {
        maxY = (GameObject.Find(">GameManager<").GetComponent<GameManager>().maxY * 21) * -1;
        if (GetComponent<SpriteRenderer>().bounds.min.y < maxY) transform.localScale += new Vector3(0, 0.15f, 0);
        if (GetComponent<SpriteRenderer>().bounds.min.y > maxY) transform.localScale -= new Vector3(0, 0.15f, 0);
    }
}
