using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialBurst : MonoBehaviour
{
    public Sprite bombStatic, bomb1, bomb2, bomb3, bombExplode, radialBurst;
    public GameObject thisSprite, explosionPrefab;
    int displayTimer;
    bool explode;
    Color thisColour;

    private void Start()
    {
        if (gameObject.name.Contains("Bomb")) transform.parent = GameObject.Find("----Bombs----").transform;
        else transform.parent = GameObject.Find("----ProjectileParent----").transform;
        thisColour = thisSprite.GetComponent<SpriteRenderer>().color;
        thisColour.a = 1;
        transform.localScale = new Vector3(5, 5, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame || GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Pause)
        {
            if (this.name.Contains("Explosion")) explode = true;
            if (!explode)
            {
                displayTimer++;
                if (displayTimer > 0) thisSprite.GetComponent<SpriteRenderer>().sprite = bombStatic;
                if (displayTimer > 10) thisSprite.GetComponent<SpriteRenderer>().sprite = bomb1;
                if (displayTimer > 20) thisSprite.GetComponent<SpriteRenderer>().sprite = bomb2;
                if (displayTimer > 30) thisSprite.GetComponent<SpriteRenderer>().sprite = bomb3;
                if (displayTimer > 40) thisSprite.GetComponent<SpriteRenderer>().sprite = bombExplode;
                if (displayTimer >= 42)
                {
                    transform.localScale = new Vector3(0.02f, 0.02f, 1);
                    Instantiate(explosionPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    Destroy(this.gameObject);
                }
            }
            else
            {
                if (GameObject.Find("Main Camera").GetComponent<CameraFollow>().shakeCamera == false) GameObject.Find("Main Camera").GetComponent<CameraFollow>().shakeCamera = true;
                thisSprite.GetComponent<SpriteRenderer>().sprite = radialBurst;
                transform.localScale += new Vector3(1.5f, 1.5f, 0);
                thisColour = thisSprite.GetComponent<SpriteRenderer>().color;
                if (transform.localScale.x >= 7.5f)
                {
                    if (thisColour.a > 0) thisColour.a -= 0.1f;
                    else Destroy(gameObject);
                }
                else if (transform.localScale.x >= 5f)
                {
                    thisColour.a -= 0.04f;
                }
                thisSprite.GetComponent<SpriteRenderer>().color = thisColour;
            }
        }
        else Destroy(gameObject);
    }
}
