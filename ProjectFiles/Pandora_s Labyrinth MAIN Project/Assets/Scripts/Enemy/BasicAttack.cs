using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    GameObject playerObject;
    SpriteRenderer playerSprite, attackRadius;
    public float attackTimer, damage;
    bool isCooldown;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("----PlayerObjectParent----");
        playerSprite = GameObject.Find("PCollision").GetComponent<SpriteRenderer>();
        attackRadius = GetComponent<SpriteRenderer>();

        if (this.name.Contains("Fast")) damage = 3;
        else damage = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (playerSprite.bounds.Intersects(attackRadius.bounds))
            {
                if (!isCooldown)
                {
                    playerObject.GetComponent<PlayerController>().TakeDamage(damage);
                    isCooldown = true;
                }
                else
                {
                    if (attackTimer < 29) attackTimer++;
                    if (attackTimer >= 29)
                    {
                        playerObject.GetComponent<PlayerController>().TakeDamage(damage);
                        isCooldown = false;
                        attackTimer = 0;
                    }
                }
            }
            else
            {
                attackTimer = 0;
                isCooldown = false;
            }
        }
    }
}
