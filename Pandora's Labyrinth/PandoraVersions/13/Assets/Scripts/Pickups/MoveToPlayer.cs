using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    public Vector3 velocity;
    public GameObject target;
    public bool activate;
    float speed;

    private void Start()
    {
        speed = 0.5f;
        target = GameObject.Find("----PlayerObjectParent----");
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (!transform.parent.name.Contains("XP"))
            {
                if (GetComponent<SpriteRenderer>().bounds.Intersects(GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().bounds))
                {
                    if (transform.parent.name.Contains("Health") && target.GetComponent<PlayerController>().totalMedkits < target.GetComponent<PlayerController>().maxMedkits)
                    {
                        activate = true;
                    }
                    else if (transform.parent.name.Contains("Bomb") && target.GetComponent<PlayerController>().totalBombs < target.GetComponent<PlayerController>().maxBombs)
                    {
                        activate = true;
                    }
                    else if (transform.parent.name.Contains("Gold"))
                    {
                        activate = true;
                    }
                    else activate = false;
                }
            }
            else if (transform.GetComponent<XPPickup>().hasCollected)
            {
                target = GameObject.Find("AttractionBar");
                activate = true;
            }
            if (activate)
            {
                velocity = new Vector3(0, 0, 0);

                if (target.transform.position.x > transform.parent.position.x) velocity.x = speed;
                else if (target.transform.position.x < transform.parent.position.x) velocity.x = -speed;

                if (target.transform.position.y > transform.parent.position.y) velocity.y = speed;
                else if (target.transform.position.y < transform.parent.position.y) velocity.y = -speed;

                transform.parent.position += velocity;
            }
        }
    }
}
