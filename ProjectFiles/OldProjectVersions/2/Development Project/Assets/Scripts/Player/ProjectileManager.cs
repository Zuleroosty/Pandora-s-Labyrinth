using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public int destroyTimer, projectileDamage, projectileRange;
    public bool hasCollided, hasHitPlayer;
    public string owner;
    public Vector3 intersectLocation, velocity;
    public Sprite collideSprite;
    GameObject colliderParent;
    SpriteRenderer collisionTest;
    int childID, childMax;

    // Start is called before the first frame update
    void Start()
    {
        destroyTimer = 0;
        projectileRange = 60;
        projectileDamage = 5;
        // Get Collision Parent
        colliderParent = GameObject.Find("LevelCollisionBoxes");
        // Set sort in order
        GetComponent<SpriteRenderer>().sortingOrder = 6;

        // Set Velocity Relative to Location
        velocity = ((transform.position - intersectLocation) * -1) * Time.deltaTime;
        velocity.Normalize();
        velocity = velocity / 5; // Adjust speed (low = faster / high = slower)
        velocity.z = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollided)
        {
            if (destroyTimer < 5) destroyTimer++;
            if (destroyTimer >= 5)
            {
                Object.Destroy(gameObject);
            }
        }
        if (!hasCollided)
        {
            // Player Collision Test
            if (GetComponent<SpriteRenderer>().bounds.Intersects(GameObject.Find("Player").GetComponent<SpriteRenderer>().bounds) && owner == "Enemy")
            {
                hasCollided = true;
                hasHitPlayer = true;
                GetComponent<SpriteRenderer>().sprite = collideSprite;
                GameObject.Find("Player").GetComponent<PlayerController>().health -= projectileDamage;
            }

            // Enemy Collision Test
            if (GetComponent<SpriteRenderer>().bounds.Intersects(GameObject.Find("Enemy").GetComponent<SpriteRenderer>().bounds) && owner == "Player")
            {
                print("Hit Enemy");
                hasCollided = true;
                GetComponent<SpriteRenderer>().sprite = collideSprite;
            }

            // Standard Collision Test
            ProjectileCollisionTest();
            ProjectileCollisionTest();
        }
        transform.position += velocity;
        if (destroyTimer < projectileRange) destroyTimer++;
        if (destroyTimer >= projectileRange) Object.Destroy(gameObject);
    }

    private void OnDestroy()
    {
        print("Destroyed");
    }

    private void ProjectileCollisionTest()
    {
        if (childID < childMax && childMax == colliderParent.transform.childCount)
        {
            print("1");
            collisionTest = colliderParent.transform.GetChild(childID).GetComponent<SpriteRenderer>();
            //ProjectileCollisionTest = projectileParent.GetChile(childID).GetChild(  HITBOX IF NOT COLLIDER - number  )GetComponent<SpriteRenderer>();
            if (GetComponent<SpriteRenderer>().bounds.Intersects(collisionTest.bounds))
            {
                print("2");
                GetComponent<SpriteRenderer>().sprite = collideSprite;
                destroyTimer = 0;
                hasCollided = true;
            }
        }
        else
        {
            print("3");
            childID = -1;
            childMax = colliderParent.transform.childCount;
        }
        childID++;
    }
    public void Destroy()
    {

        destroyTimer = 0;
        GetComponent<SpriteRenderer>().sprite = collideSprite;
        hasCollided = true;
    }
}
