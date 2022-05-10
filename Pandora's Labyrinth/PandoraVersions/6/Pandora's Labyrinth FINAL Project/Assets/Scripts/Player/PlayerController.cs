using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player Variables
    public GameObject gameManager, radialBurstObject, projectilePrefab;
    TextMesh infoTextObject;
    GameManager gameManagerScript;
    SpriteRenderer playerSprite;
    PermissionsHandler permHandler;
    Color thisColour;
    public float speed, speedDefault;
    public Vector3 velocity;
    public int delayTimer, flashTimer;
    public string infoTextDisplay;
    public bool flashDamage, ignoreDamage, hasPandorasBox, inCombat;

    // Levelling/XP
    public float xp, maxXp;
    public int level;

    // Stat Variables
    public float health, maxHealth, healthCooldown;
    public float stamina, maxStamina, staminaRegenTimer, staminaCooldown, boostTimer;
    public bool blockStaminaRegen, activateBoost, canRegenHealth;
    public float shield, maxShield;
    public int gold;

    // Projectile Variables
    public enum weaponState { ready, firing, reloading, empty }
    public weaponState currentWeaponState;
    public Sprite projectileFireSprite, projectileHitSprite;
    public Vector3 radialLocation;
    public GameObject newProjectile;
    public bool canShoot, demandShoot, reloadRequired, singleShot, forceReload, isReloading, powerShot, radialBurst;
    public int projectileSpeed, shootTimer, ammo, maxAmmo, reloadTimer, reloadDelay;
    public float powerShotCooldown, burstTimer, projectileDamage;
    public Vector3 shootLocation;

    // Start is called before the first frame update
    void Start()
    {
        // Get Variables
        playerSprite = GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>();
        infoTextObject = GameObject.Find("HUDInfoText").GetComponent<TextMesh>();
        gameManager = GameObject.Find(">GameManager<");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        permHandler = gameManager.GetComponent<PermissionsHandler>();
        thisColour = playerSprite.color;
        Application.targetFrameRate = 60;

        // Default Player Stats
        this.name = "----PlayerObjectParent----";
        speedDefault = 0.125f;
        speed = speedDefault;

        maxHealth = 250;
        health = maxHealth;
        maxStamina = 100;
        stamina = maxStamina;
        maxShield = 4;
        shield = 0;
        maxXp = 125;

        // Default Weapon Stats
        projectileSpeed = 5; // Lower = Faster
        burstTimer = 360;
        powerShotCooldown = 240;


        // Default Ammo Counts
        maxAmmo = 25;
        ammo = maxAmmo;

        // Set Spawn Position
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameManagerScript.gameState == GameManager.state.InGame)
        {
            //-----END LEVEL WHEN KILLED---------
            if (health <= 0)
            {
                gameManagerScript.gameState = GameManager.state.Lose;
                //Destroy(gameObject);
            }
            //-----------------------------------

            //----LEVEL UP WHEN REACH MAX XP-----
            if (xp >= maxXp)
            {
                xp -= maxXp;
                maxXp *= 1.2f;
                level++;
                GameObject.Find(">GameManager<").GetComponent<GameManager>().NewNotification("Level UP");
            }
            //-----------------------------------

            //----HEALTH REGEN & FEEDBACK--------
            if (!inCombat && health < maxHealth) health += 0.1f;
            if (flashDamage)
            {
                if (flashTimer < 15) flashTimer++;
                if (flashTimer >= 5 && flashTimer < 10) playerSprite.color = Color.red;
                if (flashTimer >= 10) playerSprite.color = thisColour;
                if (flashTimer >= 15)
                {
                    flashDamage = false;
                    flashTimer = 0;
                }
            }
            //----------------------------------


            //-----FUNCTIONS--------------------
            if (health > 0) MovementInput();
            WeaponController();
            UpdateInfoText();
            //----------------------------------
        }
    }
    public void TakeDamage(float damageAmount)
    {
        if (!ignoreDamage)
        {
            flashDamage = true;
            healthCooldown = 120;
            if (shield > 0) shield--;
            else if (health - damageAmount > 0) health -= damageAmount;
            else if (health - damageAmount <= 0) health = 0;
        }
    }
    private void UpdateInfoText() //-----PLAYER INFO TEXT HANDLER
    {
        infoTextDisplay = "Ammo: " + ammo;
        infoTextObject.text = infoTextDisplay;
    }

    private void MovementInput()
    {
        // Boost Input w/Cooldown
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && stamina >= maxStamina / 2)
            {
                if (!activateBoost)
                {
                    stamina -= maxStamina / 2;
                    activateBoost = true;
                }
            }
        }
        if (activateBoost)
        {
            blockStaminaRegen = true;
            speed = speedDefault + 0.15f;
            if (boostTimer < 20) boostTimer++;
            if (boostTimer >= 20)
            {
                staminaCooldown = 0;
                boostTimer = 0;
                activateBoost = false;
            }
        }
        else
        {
            blockStaminaRegen = false;
            speed = speedDefault;
        }

        // Stamina Regen
        if (!blockStaminaRegen && stamina < maxStamina)
        {
            staminaCooldown++;
            if (staminaCooldown >= 120)
            {
                stamina += 0.5f;
            }
        }
        else if (stamina > maxStamina) stamina = maxStamina;

        // Reset velocity before input
        if (!activateBoost) velocity = new Vector3(0, 0);

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
        if (Input.GetKeyDown(KeyCode.Space) && !radialBurst) //-----RADIAL SHOT
        {
            burstTimer = 0;
            radialBurst = true;
            Instantiate(radialBurstObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && !powerShot) //-----POWER SHOT
        {
            powerShotCooldown = 0;
            powerShot = true;
            PowerShot();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)) //-----BASIC SHOT
        {
            shootLocation = GameObject.Find("DisplayCursor").transform.position;
            SpawnProjectile(25);
        }

        // COOLDOWNS
        if (powerShot)
        {
            powerShotCooldown++;
            if (powerShotCooldown >= 240)
            {
                powerShot = false;
            }
        }
        if (radialBurst)
        {
            burstTimer++;
            if (burstTimer >= 360)
            {
                radialBurst = false;
            }
        }
    }
    private void PowerShot()
    {
        PowerShotHandler powerShotBar = GameObject.Find("PowerShotBar").GetComponent<PowerShotHandler>();

        shootLocation = powerShotBar.spawnPoint1.transform.position;
        SpawnProjectile(35);
        shootLocation = powerShotBar.spawnPoint2.transform.position;
        SpawnProjectile(35);
        shootLocation = powerShotBar.spawnPoint3.transform.position;
        SpawnProjectile(35);
    }

    private void SpawnProjectile(int damage)
    {
        if (ammo > 0)
        {
            //-----PROJECTILE SPAWNING---------
            ProjectileMovement projectileManager;
            newProjectile = Instantiate(projectilePrefab, GameObject.Find("ProjectileSpawn").transform.position, Quaternion.identity);
            projectileManager = newProjectile.GetComponent<ProjectileMovement>();
            projectileManager.projectileDamage = damage;
            projectileManager.moveToPosition = shootLocation;
            projectileManager.isPlayerOwned = true;
            projectileManager.projectileType = ProjectileMovement.type.Arrow;
            //---------------------------------

            ammo--;
        }
        else GameObject.Find(">GameManager<").GetComponent<GameManager>().NewNotification("You have no Ammo!");
    }
}
