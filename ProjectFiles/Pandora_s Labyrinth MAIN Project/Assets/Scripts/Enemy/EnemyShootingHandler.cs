using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingHandler : MonoBehaviour
{
    GameObject newProjectile;
    Sprite projectileHitSprite, projectileFireSprite;
    Vector3 shootLocation;
    int projectileSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        projectileSpeed = 3; // Lower = Faster
    }

    // Update is called once per frame
    void Update()
    {
        shootLocation = GameObject.Find("PlayerSprite").transform.position;
    }
    void ShootAtPlayer()
    {
        //-----PROJECTILE SPAWNING---------
        ProjectileMovement projectileManager;
        newProjectile = new GameObject("Projectile", typeof(SpriteRenderer), typeof(ProjectileMovement));
        newProjectile.GetComponent<SpriteRenderer>().sprite = projectileFireSprite;
        newProjectile.transform.position = GameObject.Find("ProjectileSpawn").transform.position;
        projectileManager = newProjectile.GetComponent<ProjectileMovement>();
        projectileManager.speedAdjuster = 1;
        projectileManager.moveToPosition = shootLocation;
        projectileManager.collideSprite = projectileHitSprite;
        projectileManager.speedAdjuster = projectileSpeed;
        //---------------------------------
    }
}
