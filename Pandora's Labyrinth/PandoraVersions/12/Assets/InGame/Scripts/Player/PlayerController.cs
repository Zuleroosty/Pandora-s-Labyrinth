 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player Variables
    public GameObject gameManager, radialBurstObject, projectilePrefab, playerSpriteObject;
    GameManager gameManagerScript;
    SpriteRenderer playerSprite;
    Color thisColour;
    public float speed, speedDefault, speedIncrease;
    public Vector3 velocity;
    public int delayTimer, flashTimer, speedCost, speedRequiredLevel;
    public string infoTextDisplay;
    public bool flashDamage, ignoreDamage, hasPandorasBox, inCombat;

    // Levelling/XP
    public float xp, maxXp;
    public int level;

    // Spear/Armour
    public enum spear { lvl0, lvl1, lvl2, lvl3, lvl4 }
    public spear currentSpear;
    public enum armour { lvl0, lvl1, lvl2, lvl3, lvl4 }
    public armour currentArmour;

    // Stat Variables
    public float health, maxHealth, healthCooldown, healthIncrease;
    public float stamina, maxStamina, staminaRegenTimer, staminaCooldown, boostTimer;
    public bool blockStaminaRegen, activateBoost, canRegenHealth;
    public float shield, maxShield;
    public int gold, healthCost, healthRequiredLevel;

    // Projectile Variables
    public GameObject spearSprite;
    public Sprite spearLvl0, spearLvl1, spearLvl2, spearLvl3, spearLvl4;
    public enum weaponState { ready, firing, reloading, empty }
    public weaponState currentWeaponState;
    public Sprite projectileFireSprite, projectileHitSprite;
    public GameObject newProjectile;
    public bool canShoot, demandShoot, reloadRequired, singleShot, forceReload, isReloading, powerShot;
    public int projectileSpeed, totalBombs, maxBombs, totalMedkits, maxMedkits;
    public float powerShotCooldown, projectileDamage, projectileCooldown, maxProjectileCooldown;
    public Vector3 shootLocation;

    // Start is called before the first frame update
    void Start()
    {
        // Get Variables
        playerSprite = playerSpriteObject.GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find(">GameManager<");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        thisColour = playerSprite.color;

        // Default Player Stats
        speedIncrease = 0;
        healthIncrease = 0;

        healthRequiredLevel = 1;
        speedRequiredLevel = 1;

        this.name = "----PlayerObjectParent----";
        speedDefault = 0.125f;
        speed = speedDefault;
        maxHealth = 650;
        health = maxHealth;
        maxStamina = 100;
        stamina = maxStamina;
        maxShield = 4;
        shield = 0;
        maxXp = 500;
        maxBombs = 3;
        maxMedkits = 3;

        // Default Weapon Stats
        powerShotCooldown = 240;
        projectileDamage = 25;
        UpdateSpearSprite();

        // Set Spawn Position
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // UPDATE VALUES ON ENTER NEXT LEVEL
        if (gameManagerScript.gameState == GameManager.state.Start)
        {
            hasPandorasBox = false;
            health = maxHealth;
            stamina = maxStamina;
            speed = speedDefault;
            healthCooldown = 0;
            staminaCooldown = 0;
            transform.position = new Vector3(0, 0, 0);
        }

        // ENABLED ONLY DURING PLAY
        if (gameManagerScript.gameState == GameManager.state.InGame)
        {
            //-----END LEVEL WHEN KILLED---------
            if (health <= 0)
            {
                gameManagerScript.gameState = GameManager.state.Lose;
            }
            //-----------------------------------

            //----LEVEL UP WHEN REACH MAX XP-----
            if (xp >= maxXp)
            {
                xp -= maxXp;
                maxXp *= 1.2f;
                level++;
                GameObject.Find(">GameManager<").GetComponent<GameManager>().NewNotification("LEVEL UP", 0);
            }
            //-----------------------------------

            //----HEALTH REGEN & FEEDBACK--------
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
            if (health < maxHealth && Input.GetKeyDown(KeyCode.E) && totalMedkits > 0)
            {
                totalMedkits--;
                if (health <= maxHealth - (maxHealth / 3))
                {
                    health += maxHealth / 3;
                }
                else health = maxHealth;
            }
            //----------------------------------


            //-----FUNCTIONS--------------------
            if (health > 0) MovementInput();
            WeaponController();
            UpdateSpriteDirection();
            UpdateSpearSprite();
            //----------------------------------
        }
    }
    public void StartNewLevel()
    {
        transform.position = new Vector3(0, 0, 0);
    }
    void UpdateSpearSprite()
    {
        switch (currentSpear)
        {
            case spear.lvl0:
                maxProjectileCooldown = 25;
                spearSprite.GetComponent<SpriteRenderer>().sprite = spearLvl0;
                break;
            case spear.lvl1:
                maxProjectileCooldown = 20;
                spearSprite.GetComponent<SpriteRenderer>().sprite = spearLvl1;
                break;
            case spear.lvl2:
                maxProjectileCooldown = 15;
                spearSprite.GetComponent<SpriteRenderer>().sprite = spearLvl2;
                break;
            case spear.lvl3:
                maxProjectileCooldown = 10;
                spearSprite.GetComponent<SpriteRenderer>().sprite = spearLvl3;
                break;
            case spear.lvl4:
                maxProjectileCooldown = 5;
                spearSprite.GetComponent<SpriteRenderer>().sprite = spearLvl4;
                break;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (!ignoreDamage)
        {
            GameObject.Find("Main Camera").GetComponent<CameraFollow>().shakeCamera = true;
            GameObject.Find("HitScreen").GetComponent<HitDisplay>().pulseIn = true;
            flashDamage = true;
            healthCooldown = 120;
            if (shield > 0) shield--;
            else if (health - damageAmount > 0) health -= damageAmount;
            else if (health - damageAmount <= 0) health = 0;
        }
    }

    private void MovementInput()
    {
        // Boost Input w/Cooldown
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.LeftShift) && stamina >= maxStamina / 2)
            {
                if (!activateBoost)
                {
                    GameObject.Find("Main Camera").GetComponent<CameraFollow>().pulseCameraOut = true;
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
            if (staminaCooldown >= 45)
            {
                stamina += 0.75f;
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
        if (Input.GetKeyDown(KeyCode.Space) && totalBombs > 0) //-----DROP BOMB
        {
            totalBombs--;
            Instantiate(radialBurstObject, new Vector3(transform.position.x - 0.2f, transform.position.y - 0.75f, 0), Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && !powerShot) //-----POWER SHOT
        {
            GameObject.Find("Main Camera").GetComponent<CameraFollow>().pulseCameraIn = true;
            powerShotCooldown = 0;
            powerShot = true;
            SpawnProjectile(1);
            SpawnProjectile(2);
            SpawnProjectile(3);
        }
        if (Input.GetKey(KeyCode.Mouse0) && projectileCooldown >= maxProjectileCooldown) //-----BASIC SHOT
        {
            GameObject.Find("Main Camera").GetComponent<CameraFollow>().pulseCameraIn = true;
            projectileCooldown = 0;
            shootLocation = new Vector3(0, 0, 0);
            SpawnProjectile(0);
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
        if (projectileCooldown < maxProjectileCooldown) projectileCooldown++;
    }

    private void SpawnProjectile(int fireType)
    {
        //-----PROJECTILE SPAWNING---------
        ProjectileMovement projectileManager;
        newProjectile = Instantiate(projectilePrefab, GameObject.Find("ProjectileSpawn").transform.position, Quaternion.identity);
        projectileManager = newProjectile.GetComponent<ProjectileMovement>();
        projectileManager.isPlayerOwned = true;
        switch (fireType)
        {
            case 0:
                projectileManager.shootLocation = ProjectileMovement.location.Mouse;
                break;
            case 1:
                projectileManager.shootLocation = ProjectileMovement.location.ps1;
                break;
            case 2:
                projectileManager.shootLocation = ProjectileMovement.location.ps2;
                break;
            case 3:
                projectileManager.shootLocation = ProjectileMovement.location.ps3;
                break;
        }
        //---------------------------------
    }
    private void UpdateSpriteDirection()
    {
        if (GameObject.Find("DisplayCursor").transform.position.x < transform.position.x)
        {
            GameObject.Find("PlayerShadow").GetComponent<SpriteRenderer>().flipX = false;
            GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().flipX = false;
            GameObject.Find("AimingArm").transform.localScale = new Vector3(-1.25f, -1.25f, 1);
            GameObject.Find("AimingArm").transform.localPosition = new Vector3(0.042f, 0.065f, -1);
        }
        else
        {
            GameObject.Find("PlayerShadow").GetComponent<SpriteRenderer>().flipX = true;
            GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().flipX = true;
            GameObject.Find("AimingArm").transform.localScale = new Vector3(-1.25f, 1.25f, 1);
            GameObject.Find("AimingArm").transform.localPosition = new Vector3(-0.042f, 0.065f, -1);
        }
    }
}
