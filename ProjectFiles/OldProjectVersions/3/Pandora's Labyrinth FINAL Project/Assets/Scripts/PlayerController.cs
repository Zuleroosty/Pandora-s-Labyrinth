using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player Variables
    GameObject gameManager;
    PermissionsHandler permHandler;
    public float speed;
    public Vector3 velocity;
    public int delayTimer;
    public string infoTextDisplay;

    // Projectile Variables
    public enum weaponState { ready, firing, reloading, empty }
    public weaponState currentWeaponState;
    public Sprite projectileFireSprite, projectileHitSprite;
    public GameObject newProjectile;
    public bool canShoot, demandShoot, reloadRequired, singleShot, forceReload, isReloading, powerShot;
    public float projectileFireRate, shootRate; // 0.2f by default (fires once every 0.2 seconds)
    public int projectileSpeed, shootTimer, clip, maxClip, ammo, maxAmmo, reloadTimer, reloadDelay;
    public int powerShotTimer, powerShotCooldown, shotsTaken;
    public Vector3 shootLocation;

    // Start is called before the first frame update
    void Start()
    {
        // Get GameManager/PermissionHandler
        gameManager = GameObject.Find(">GameManager<");
        permHandler = gameManager.GetComponent<PermissionsHandler>();
        Application.targetFrameRate = 60;

        // Default Player Stats
        speed = 0.075f;

        // Default Weapon Stats
        projectileFireRate = 0.2f; // Lower = Faster (Shoots p/Second)
        projectileSpeed = 3; // Lower = Faster

        // Default Ammo Counts
        maxClip = 5;
        maxAmmo = 40;
        clip = 5;
        ammo = maxAmmo - clip;
    }

    // Update is called once per frame
    void Update()
    {
        //-----MOVEMENT MANAGER------------
        if (permHandler.canMove) MovementInput();
        //---------------------------------

        //-----WEAPON MANAGER--------------
        if (currentWeaponState == weaponState.reloading)
        {
            if (reloadTimer < reloadDelay) reloadTimer++;
            if (reloadTimer > reloadDelay - 5)
            {
                reloadRequired = false;
            }
            if (reloadTimer >= reloadDelay)
            {
                currentWeaponState = weaponState.ready;
                isReloading = false;
                reloadTimer = 0;
            }
        }
        else WeaponController();

        if (currentWeaponState == weaponState.ready && !Input.GetKey(KeyCode.Mouse0))
        {
            if (forceReload) forceReload = false;
        }
        //---------------------------------

        //-----UPDATE PLAYER INFO TEXT-----
        UpdateInfoText();
        //---------------------------------

    }
    private void UpdateInfoText() //-----PLAYER INFO TEXT HANDLER
    {
        TextMesh infoTextObject;
        infoTextObject = GameObject.Find("InfoText").GetComponent<TextMesh>();

        // UPDATE TEXT BASED ON PLAYER STATE
        infoTextDisplay = "";
        if (isReloading) infoTextDisplay = "Reloading...";
        else if (ammo <= 0 && clip <= 0) infoTextDisplay = "You have NO ammo!";
        else if (infoTextDisplay == "") infoTextDisplay = "Clip: " + clip + "\n" + "Ammo: " + ammo;

        infoTextObject.text = infoTextDisplay;
    }

    private void MovementInput()
    {
        // Reset velocity before input
        velocity = new Vector3(0, 0);

        // Update velocity after input
        if (Input.GetKey(KeyCode.A)) velocity.x = -speed;
        if (Input.GetKey(KeyCode.W)) velocity.y = speed;
        if (Input.GetKey(KeyCode.D)) velocity.x = speed;
        if (Input.GetKey(KeyCode.S)) velocity.y = -speed;

        // Move player Object
        transform.position += velocity;
    }

    private void WeaponController()
    {
        if (!isReloading) // Shoot projectile or reload if needed
        {
            if (Input.GetKey(KeyCode.Mouse1) && !powerShot) //-----POWER SHOT
            {
                ShootOrReload();
                powerShot = true;
                shotsTaken = clip;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0)) //-----BASIC SHOT
            {
                ShootOrReload();
            }
            else // Check if weapon required reload when not shooting
            {
                currentWeaponState = weaponState.ready;
                singleShot = false;
                if (clip > 0) canShoot = true;
                else
                {
                    canShoot = false;
                    if (ammo > 0 && clip < maxClip)
                    {
                        reloadRequired = true;
                    }
                }
            }
        }

        // Power Shot
        if (powerShot)
        {
            if (shotsTaken > 0)
            {
                if (powerShotTimer < 10) powerShotTimer++;
                if (powerShotTimer >= 10)
                {
                    ShootOrReload();
                    powerShotTimer = 0;
                    shotsTaken--;
                }
            }
            if (shotsTaken <= 0)
            {
                if (powerShotCooldown < 180) powerShotCooldown++;
                if (powerShotCooldown >= 180)
                {
                    powerShotCooldown = 0;
                    powerShot = false;
                }
            }
        }

        // Reload Weapon
        if (Input.GetKeyDown(KeyCode.R) && clip < maxClip)
        {
            isReloading = true;
            ReloadWeapon();
        }
        if (forceReload) ReloadWeapon();

        // Shoot Rate Timer
        if (currentWeaponState == weaponState.firing)
        {
            shootRate = projectileFireRate * 60f;
            if (shootTimer < shootRate) shootTimer++;
            if (shootTimer >= shootRate)
            {
                SpawnProjectile();
                shootTimer = 0;
                currentWeaponState = weaponState.ready;
            }
        }
    }
    private void ShootOrReload()
    {
        shootLocation = GameObject.Find("DisplayCursor").GetComponent<CursorController>().mouseWorldPosition;
        if (ammo > 0 && clip <= 0) reloadRequired = true;
        if (reloadRequired)
        {
            isReloading = true;
            ReloadWeapon();
        }
        else if (currentWeaponState != weaponState.firing && canShoot)
        {
            if (!singleShot)
            {
                SpawnProjectile();
                singleShot = true;
            }
            shootTimer = 0;
            currentWeaponState = weaponState.firing;
        }
    }

    private void ReloadWeapon()
    {
        currentWeaponState = weaponState.reloading;
        int requiredAmmo;
        requiredAmmo = maxClip - clip;
        if (ammo > requiredAmmo) // Does player have enough ammo to reload?
        {
            ammo -= requiredAmmo;
            clip = maxClip;
            canShoot = true;
            reloadDelay = 30;
        }
        else if (ammo <= requiredAmmo || ammo > 0) // Does the player less than the needed ammo?
        {
            clip += ammo;
            ammo = 0;
            canShoot = true;
            reloadDelay = 30;
        }
        else
        {
            print("Cannot Reload - Not Enough Ammo!");
            currentWeaponState = weaponState.empty;
        }
    }

    private void SpawnProjectile()
    {
        if (clip > 0)
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

            clip--;

            print("Projectile Fired!");
        }
        else forceReload = true;
    }
}
