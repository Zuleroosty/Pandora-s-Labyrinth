using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMasterController : MonoBehaviour
{
    public Sprite projectileSprite, projectColSprite;
    public int fireTimer, fireRate, randomINT;
    bool toggleFireType, canFire, randomFire;
    GameObject projectile;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Basic shoot & spawn
        if (fireTimer < fireRate) fireTimer++;
        if (fireTimer >= fireRate)
        {
            randomINT = Random.Range(0, 3);
            if (randomINT == 1)
            {
                SpawnProjectile();
            }
            fireTimer = 0;
            canFire = true;
            fireRate = Random.Range(10, 31);
        }
    }

    private void SpawnProjectile()
    {
        projectile = new GameObject("Projectile", typeof(SpriteRenderer), typeof(ProjectileManager));
        projectile.GetComponent<ProjectileManager>().owner = "Enemy";
        projectile.transform.parent = GameObject.Find("ProjectileParent").transform;
        projectile.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        projectile.transform.position = transform.position;
        projectile.GetComponent<SpriteRenderer>().sprite = projectileSprite;
        projectile.GetComponent<ProjectileManager>().collideSprite = projectColSprite;
        projectile.GetComponent<ProjectileManager>().intersectLocation = GameObject.Find("Player").transform.position;
        projectile.GetComponent<ProjectileManager>().intersectLocation.z = 0;
    }
}

