using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomiser : MonoBehaviour
{
    public int maxSprites;
    public Sprite sprite1, sprite2, sprite3, sprite4;
    public Sprite white1, white2, white3, white4;
    int randNum;

    // Start is called before the first frame update
    void Start()
    {
        randNum = Random.Range(1, maxSprites + 1);
        switch (randNum)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = sprite1;
                if (white1 != null) transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = sprite1;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = sprite2;
                if (white2 != null) transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = sprite3;
                if (white3 != null) transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = sprite3;
                break;
            case 4:
                GetComponent<SpriteRenderer>().sprite = sprite1;
                if (white4 != null) transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = sprite4;
                break;
        }
    }
}
