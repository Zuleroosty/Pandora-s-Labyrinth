using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    GameObject playerObject;
    SpriteRenderer playerSprite, attackRadius;
    public float attackTimer, cooldown, damage;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("----PlayerObjectParent----");
        playerSprite = GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>();
        attackRadius = GetComponent<SpriteRenderer>();

        if (this.name.Contains("Fast")) damage = 0.5f;
        else damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSprite.bounds.Intersects(attackRadius.bounds))
        {
            if (attackTimer < 2) attackTimer++;
            if (attackTimer >= 2)
            {
                attackTimer++;
                playerObject.GetComponent<PlayerController>().TakeDamage(damage);
            }
            if (attackTimer > 20)
            {
                if (cooldown < 120) cooldown++;
                if (cooldown >= 120)
                {
                    playerObject.GetComponent<PlayerController>().TakeDamage(damage);
                    cooldown = 0;
                }
            }
        }
        else
        {
            attackTimer = 0;
            cooldown = 0;
        }
    }
}
