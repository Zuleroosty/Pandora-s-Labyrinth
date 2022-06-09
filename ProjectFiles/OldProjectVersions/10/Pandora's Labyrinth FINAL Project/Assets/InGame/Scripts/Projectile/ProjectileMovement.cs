using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public GameObject visSpriteObject;
    public Vector3 moveToPosition, velocity;
    public Sprite collideSprite, blueFireSprite, redFireSprite, magentaFireSprite, greenFireSprite;
    public int speedAdjuster, projectileRange, rangeTimer, destroyTimer;
    public float projectileDamage;
    public bool destroyObject, canDamage, isPlayerOwned;
    public enum colour {Blue, Red, Magenta, Green}
    public colour fireColour;

    // Start is called before the first frame update
    void Start()
    {
        if (isPlayerOwned)
        {
            visSpriteObject.GetComponent<SpriteRenderer>().sprite = GameObject.Find("SpearSprite").GetComponent<SpriteRenderer>().sprite;
            switch (GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().currentSpear)
            {
                case PlayerController.spear.lvl0:
                    projectileDamage = 44;
                    speedAdjuster = 5;
                    break;
                case PlayerController.spear.lvl1:
                    projectileDamage = 56;
                    speedAdjuster = 4;
                    break;
                case PlayerController.spear.lvl2:
                    projectileDamage = 68;
                    speedAdjuster = 3;
                    break;
                case PlayerController.spear.lvl3:
                    projectileDamage = 70;
                    speedAdjuster = 2;
                    break;
                case PlayerController.spear.lvl4:
                    projectileDamage = 81;
                    speedAdjuster = 1;
                    break;
            }
        }
        else
        {
            projectileDamage = Random.Range(5, 8);
            switch (fireColour)
            {
                case colour.Red:
                    visSpriteObject.GetComponent<SpriteRenderer>().sprite = redFireSprite;
                    break;
                case colour.Blue:
                    visSpriteObject.GetComponent<SpriteRenderer>().sprite = blueFireSprite;
                    break;
                case colour.Magenta:
                    visSpriteObject.GetComponent<SpriteRenderer>().sprite = magentaFireSprite;
                    break;
                case colour.Green:
                    visSpriteObject.GetComponent<SpriteRenderer>().sprite = greenFireSprite;
                    break;
            }
            moveToPosition = GameObject.Find("----PlayerObjectParent----").transform.position;
            speedAdjuster = 3;
        }

        transform.localScale = new Vector3(0.25f, 0.25f, 1);
        visSpriteObject.transform.localPosition = new Vector3(-1.86f, 0, 0);

        // Update Object Properties
        GetComponent<SpriteRenderer>().sortingOrder = 6;
        destroyTimer = 0;
        if (projectileRange <= 0) projectileRange = 180;
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
            visSpriteObject.GetComponent<SpriteRenderer>().sprite = collideSprite;
            visSpriteObject.transform.localScale = new Vector3(20f, 20f, 1);
            visSpriteObject.transform.localPosition = new Vector3(0, 0, -1);
            destroyObject = true;
        }
    }
}
