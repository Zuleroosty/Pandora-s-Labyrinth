using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionMaster : MonoBehaviour
{
    GameObject masterParent, colliderObject;
    SpriteRenderer colliderSprite;
    int childID, childMax;

    // Start is called before the first frame update
    void Start()
    {
        masterParent = GameObject.Find("ProjectileParent");
    }

    // Update is called once per frame
    void Update()
    {
        RunCollisionTest();
    }

    void RunCollisionTest()
    {
        // Projectile Collision
        if (childID < childMax && childMax == masterParent.transform.childCount)
        {
            colliderSprite = masterParent.transform.GetChild(childID).GetComponent<SpriteRenderer>();
            colliderObject = colliderSprite.gameObject;
            if (GetComponent<SpriteRenderer>().bounds.Intersects(colliderSprite.bounds))
            {
                if (colliderObject.name == "Projectile")
                {
                    if (this.name.Contains("HitBox"))
                    {
                        if (colliderObject.GetComponent<ProjectileManager>().owner == "Enemy")
                        {
                            colliderObject.GetComponent<ProjectileManager>().Destroy();
                            GameObject.Find("Player").GetComponent<PlayerController>().health -= colliderObject.GetComponent<ProjectileManager>().projectileDamage;
                        }
                        if (colliderObject.GetComponent<ProjectileManager>().owner == "Player")
                        {
                            colliderObject.GetComponent<ProjectileManager>().Destroy();
                            GameObject.Find("Enemy").GetComponent<EnemyAI>().health -= colliderObject.GetComponent<ProjectileManager>().projectileDamage;
                        }
                    }
                }
            }
        }
        else
        {
            childID = -1;
            childMax = masterParent.transform.childCount;
        }
        childID++;

        // Wall Collision Blocking Entities
        if (this.name.Contains("Wall"))
        {
            if (GameObject.Find("PlayerCollider").GetComponent<SpriteRenderer>().bounds.Intersects(GetComponent<SpriteRenderer>().bounds))
            {
                GameObject.Find("Player").transform.position -= GameObject.Find("Player").GetComponent<PlayerController>().velocity;
            }
        }
    }
}
