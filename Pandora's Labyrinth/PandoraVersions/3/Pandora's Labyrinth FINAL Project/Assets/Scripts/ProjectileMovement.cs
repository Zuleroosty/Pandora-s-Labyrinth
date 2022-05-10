using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public Vector3 moveToPosition, velocity;
    public Sprite collideSprite;
    public int speedAdjuster, projectileRange, projectileDamage, rangeTimer, destroyTimer;
    public bool destroyObject;

    // Start is called before the first frame update
    void Start()
    {
        // Update Object Properties
        GetComponent<SpriteRenderer>().sortingOrder = 6;
        destroyTimer = 0;
        if (projectileRange <= 0) projectileRange = 60;
        if (projectileDamage <= 0) projectileDamage = 5;

        // Allocate Master Parent in Hierarchy
        transform.parent = GameObject.Find("----ProjectileMaster----").transform;

        // Set Projectile Velocity Relative to Location
        velocity = ((transform.position - moveToPosition) * -1) * Time.deltaTime;
        velocity.Normalize();
        velocity = velocity / speedAdjuster;
        velocity.z = 0;
    }

    // Update is called once per frame
    void Update()
    {
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
        GetComponent<SpriteRenderer>().sprite = collideSprite;
        destroyObject = true;
    }
}
