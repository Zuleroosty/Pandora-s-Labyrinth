using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{
    bool hasCollided;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = GameObject.Find("FallingObjectsParent").transform;
        speed = Random.Range(0.085f, 0.126f);
        transform.position = new Vector3(Random.Range(-8.00f, 8.01f), 10, 0);
        transform.localScale = new Vector3(Random.Range(0.75f, 1.26f), Random.Range(0.75f, 1.26f), 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (transform.position.y < -5) transform.position = new Vector3(Random.Range(-7.00f, 8.01f), 5, 0);
            transform.position -= new Vector3(0, speed + GameObject.Find("GameManager").GetComponent<GameManager>().objectOffset, 0);

            if (GetComponent<SpriteRenderer>().bounds.Intersects(GameObject.Find("PlayerObject").GetComponent<SpriteRenderer>().bounds) && !hasCollided)
            {
                hasCollided = true;
                GameObject.Find("PlayerObject").GetComponent<PlayerController>().ResetPosition();
                Destroy(gameObject);
            }
        }
        else Destroy(gameObject);
    }
}
