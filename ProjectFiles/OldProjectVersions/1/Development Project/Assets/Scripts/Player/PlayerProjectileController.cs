using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileController : MonoBehaviour
{
    public Sprite projectileSprite, projectColSprite;
    public int fireTimer, fireRate;
    bool toggleFireType, canFire;
    GameObject projectile;

    // Update is called once per frame
    void Update()
    {
        // Basic shoot & spawn
        if (Input.GetKeyDown(KeyCode.E)) toggleFireType = !toggleFireType;
        if (Input.GetKeyDown(KeyCode.Mouse0) && toggleFireType && canFire)
        {
            SpawnProjectile();
        }
        if (Input.GetKey(KeyCode.Mouse0) && !toggleFireType)
        {
            if (canFire) SpawnProjectile();
            canFire = false;
        }

        if (!canFire)
        {
            if (fireTimer < fireRate) fireTimer++;
            if (fireTimer >= fireRate)
            {
                fireTimer = 0;
                canFire = true;
            }
        }
    }

    private void SpawnProjectile()
    {
        projectile = new GameObject("Projectile", typeof(SpriteRenderer), typeof(ProjectileManager));
        projectile.GetComponent<ProjectileManager>().owner = "Player";
        projectile.transform.parent = GameObject.Find("ProjectileParent").transform;
        projectile.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        projectile.transform.position = transform.position;
        projectile.GetComponent<SpriteRenderer>().sprite = projectileSprite;
        projectile.GetComponent<ProjectileManager>().collideSprite = projectColSprite;
        projectile.GetComponent<ProjectileManager>().intersectLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 5);
        projectile.GetComponent<ProjectileManager>().intersectLocation.z = 0;
    }
}

