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

        if (this.name.Contains("Fast")) damage = 55;
        else if (this.name.Contains("Normal")) damage = 15;
        else if (this.name.Contains("Boss")) damage = 62.5f;
        else damage = 15;
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
                    if (attackTimer < 60) attackTimer++;
                    if (attackTimer >= 60)
                    {
                        attackTimer = 0;
                        isCooldown = false;
                        playerObject.GetComponent<PlayerController>().TakeDamage(damage);
                        playerObject.transform.position += (playerObject.transform.position - transform.position);
                        playerObject.transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, 0);
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
