using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public Vector3 moveToPosition, velocity;
    public Sprite collideSprite, arrowSprite, fireSprite, bulletSprite;
    public int speedAdjuster, projectileRange, rangeTimer, destroyTimer;
    public float projectileDamage;
    public bool destroyObject, canDamage, isPlayerOwned;
    public enum type {Arrow, Fire, Bullet}
    public type projectileType;
    public enum colour {Blue, Red, Magenta}
    public colour fireColour;

    // Start is called before the first frame update
    void Start()
    {
        // Set Projectile Properties Base on Type
        switch (projectileType)
        {
            case type.Arrow:
                projectileDamage = 43;
                GetComponent<SpriteRenderer>().sprite = arrowSprite;
                transform.localScale = new Vector3(0.35f, 0.35f, 1);
                speedAdjuster = 2;
                break;
            case type.Fire:
                projectileDamage = Random.Range(5, 8);
                GetComponent<SpriteRenderer>().sprite = fireSprite;
                transform.localScale = new Vector3(0.45f, 0.45f, 1);
                moveToPosition = GameObject.Find("----PlayerObjectParent----").transform.position;
                speedAdjuster = 3;
                switch (fireColour)
                {
                    case colour.Blue:
                        GetComponent<SpriteRenderer>().color = Color.blue;
                        break;
                    case colour.Red:
                        GetComponent<SpriteRenderer>().color = Color.red;
                        break;
                    case colour.Magenta:
                        GetComponent<SpriteRenderer>().color = Color.magenta;
                        break;
                }
                break;
            case type.Bullet:
                projectileDamage = 56;
                GetComponent<SpriteRenderer>().sprite = bulletSprite;
                transform.localScale = new Vector3(0.35f, 0.35f, 1);
                speedAdjuster = 1;
                break;
        }

        // Update Object Properties
        GetComponent<SpriteRenderer>().sortingOrder = 6;
        destroyTimer = 0;
        if (projectileRange <= 0) projectileRange = 60;
        if (projectileDamage <= 0) projectileDamage = 43;
        GetComponent<RotateTo>().targetPos = moveToPosition - transform.position;

        // Allocate Master Parent in Hierarchy
        transform.parent = GameObject.Find("----ProjectileParent----").transform;

        // Set Projectile Velocity Relative to Location
        velocity = ((transform.position - moveToPosition) * -1) * Time.deltaTime;
        velocity.Normalize();
        velocity = velocity / speedAdjuster;
        velocity.z = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerOwned)
        {
            if (GetComponent<SpriteRenderer>().bounds.Intersects(GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().bounds))
            {
                GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().TakeDamage(projectileDamage);
                OnCollisionDestroy();
            }
        }

        if (destroyObject)
        {
            if (destroyTimer < 5) destroyTimer++;
            if (destroyTimer >= 5) Object.Destroy(gameObject);
        }
        else
        {
            transform.position += velocity;
            if (rangeTimer < projectileRange) rangeTimer++;
            if (rangeTimer >= projectileRange) destroyObject = true;
        }
    }
    public void OnCollisionDestroy()
    {
        if (!destroyObject)
        {
            canDamage = false;
            GetComponent<SpriteRenderer>().sprite = collideSprite;
            if (projectileType == type.Fire) transform.localScale = new Vector3(0.65f, 0.65f, 1);
            else transform.localScale = new Vector3(0.55f, 0.55f, 1);
            destroyObject = true;
        }
    }
}
