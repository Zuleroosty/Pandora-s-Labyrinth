using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    public Vector3 velocity;
    GameObject player;
    float speed;

    private void Start()
    {
        speed = 1f;
        player = GameObject.Find("----PlayerObjectParent----");
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<SpriteRenderer>().bounds.Intersects(GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().bounds))
        {
            velocity = new Vector3(0, 0, 0);

            if (player.transform.position.x > transform.position.x) velocity.x = speed;
            else if (player.transform.position.x < transform.position.x) velocity.x = -speed;

            if (player.transform.position.y > transform.position.y) velocity.y = speed;
            else if (player.transform.position.y < transform.position.y) velocity.y = -speed;

            transform.position += velocity;
        }
    }
}
