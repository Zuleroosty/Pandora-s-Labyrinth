using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public AudioClip woodFX, stoneFX, metalFX, meatFX1, meatFX2, fireballFX;
    public GameObject visSpriteObject, enemyObject;
    public Vector3 moveToPosition, velocity;
    public Sprite collideSprite, blueFireSprite, redFireSprite, magentaFireSprite, greenFireSprite;
    public int projectileRange, rangeTimer, destroyTimer, randNum;
    public float speedAdjuster, projectileDamage, powerShotOffset;
    public bool destroyObject, canDamage, isPlayerOwned, isPowershot;
    public enum colour {Blue, Red, Magenta, Green}
    public colour fireColour;
    public enum location {Mouse, ps1, ps2, ps3}
    public location shootLocation;
    PowerShotHandler powerShotBar;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame || GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Pause)
        {
            powerShotOffset = 0.2f;
            if (isPlayerOwned)
            {
                transform.localScale = new Vector3(0.25f, 0.25f, 5);
                visSpriteObject.GetComponent<SpriteRenderer>().sprite = GameObject.Find("SpearSprite").GetComponent<SpriteRenderer>().sprite;
                switch (GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().currentSpear)
                {
                    case PlayerController.spear.lvl0:
                        if (isPowershot) projectileDamage = 55 * (1 + powerShotOffset);
                        else projectileDamage = 55;
                        speedAdjuster = 2f;
                        break;
                    case PlayerController.spear.lvl1:
                        if (isPowershot) projectileDamage = 75 * (1 + powerShotOffset);
                        else projectileDamage = 75;
                        speedAdjuster = 1.75f;
                        break;
                    case PlayerController.spear.lvl2:
                        if (isPowershot) projectileDamage = 95 * (1 + powerShotOffset);
                        else projectileDamage = 95;
                        speedAdjuster = 1.5f;
                        break;
                    case PlayerController.spear.lvl3:
                        if (isPowershot) projectileDamage = 115 * (1 + powerShotOffset);
                        else projectileDamage = 115;
                        speedAdjuster = 1.25f;
                        break;
                    case PlayerController.spear.lvl4:
                        if (isPowershot) projectileDamage = 135 * (1 + powerShotOffset);
                        else projectileDamage = 135;
                        speedAdjuster = 1f;
                        break;
                }
                powerShotBar = GameObject.Find("PowerShotBar").GetComponent<PowerShotHandler>();
                switch (shootLocation)
                {
                    case location.Mouse:
                        moveToPosition = GameObject.Find("DisplayCursor").transform.position;
                        break;
                    case location.ps1:
                        moveToPosition = powerShotBar.spawnPoint1.transform.position;
                        break;
                    case location.ps2:
                        moveToPosition = powerShotBar.spawnPoint2.transform.position;
                        break;
                    case location.ps3:
                        moveToPosition = powerShotBar.spawnPoint3.transform.position;
                        break;
                }
            }
            else
            {
                projectileDamage = Random.Range(11, 16);
                transform.localScale = new Vector3(0.35f, 0.35f, 5);
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
                speedAdjuster = 2f;
            }

            // SET ROTATION
            float angle;
            angle = Mathf.Atan2(moveToPosition.y - transform.position.y, moveToPosition.x - transform.position.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // Update Object Properties
            GetComponent<SpriteRenderer>().sortingOrder = 6;
            destroyTimer = 0;
            if (projectileRange <= 0) projectileRange = 180;
            if (projectileDamage <= 0) projectileDamage = 43;

            // Allocate Master Parent in Hierarchy
            transform.parent = GameObject.Find("----ProjectileParent----").transform;

            // Set Projectile Velocity Relative to Location
            velocity = ((transform.position - moveToPosition) * -1) * Time.deltaTime;
            velocity.Normalize();
            velocity = velocity / speedAdjuster;
            velocity.z = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (!isPlayerOwned)
            {
                if (GetComponent<SpriteRenderer>().bounds.Intersects(GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().bounds))
                {
                    GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().TakeDamage(projectileDamage);
                    OnCollisionDestroy(4);
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
    }
    public void OnCollisionDestroy(int objectType)
    {
        if (!destroyObject)
        {
            canDamage = false;
            visSpriteObject.GetComponent<SpriteRenderer>().sprite = collideSprite;
            if (isPlayerOwned)
            {
                switch (objectType)
                {
                    case 1:
                        GameObject.Find(">GameManager<").GetComponent<GameManager>().SpawnNew3DFX(woodFX, transform.position);
                        break;
                    case 2:
                        GameObject.Find(">GameManager<").GetComponent<GameManager>().SpawnNew3DFX(stoneFX, transform.position);
                        break;
                    case 3:
                        GameObject.Find(">GameManager<").GetComponent<GameManager>().SpawnNew3DFX(metalFX, transform.position);
                        break;
                    case 4:
                        randNum = Random.Range(1, 3);
                        if (randNum == 1) GameObject.Find(">GameManager<").GetComponent<GameManager>().SpawnNew3DFX(meatFX1, transform.position);
                        else GameObject.Find(">GameManager<").GetComponent<GameManager>().SpawnNew3DFX(meatFX2, transform.position);
                        break;
                }
            }
            else GameObject.Find(">GameManager<").GetComponent<GameManager>().SpawnNew3DFX(fireballFX, transform.position);
            visSpriteObject.transform.localScale = new Vector3(20f, 20f, 1);
            visSpriteObject.transform.localPosition = new Vector3(0, 0, -6);
            destroyObject = true;
            if (enemyObject != null) enemyObject.GetComponent<EnemyHealthHandler>().TakeDamage(projectileDamage);
        }
    }
}
