 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player Variables
    public GameObject gameManager, radialBurstObject, projectilePrefab, playerSpriteObject, collisionObject;
    public AudioClip throwSpear, takeDamage, collect, levelUp, deathFX;
    public float speed, speedDefault, speedIncrease;
    public Vector3 velocity;
    public int delayTimer, flashTimer, speedCost, speedRequiredLevel;
    public string infoTextDisplay;
    public bool flashDamage, ignoreDamage, hasPandorasBox, inCombat, isColliding, isMoving;

    GameManager gameManagerScript;
    SpriteRenderer playerSprite;
    Color thisColour;

    // Levelling/XP
    public float xp, maxXp;
    public int level;
    public int currentSpear;

    // Stat Variables
    public float health, maxHealth, healAmount, healthCooldown, healthIncrease, stamina, maxStamina, staminaRegenTimer, staminaCooldown, boostTimer, potionCooldown, bombCooldown;
    public bool blockStaminaRegen, activateBoost, canRegenHealth;
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
    public float powerShotCooldown, projectileCooldown, maxProjectileCooldown;
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

        speedDefault = 0.1f;
        speed = speedDefault;

        maxHealth = 450;
        health = maxHealth;

        maxStamina = 100;
        stamina = maxStamina;

        level = 0;
        xp = 0;
        maxXp = 500;

        maxBombs = 3;
        maxMedkits = 3;

        // Default Weapon Stats
        powerShotCooldown = 240;
        UpdateSpearSprite();

        // Set Spawn Position
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // UPDATE VALUES ON NEW LEVEL
        if (gameManagerScript.gameState == GameManager.state.Start)
        {
            hasPandorasBox = false;
            health = maxHealth;
            stamina = maxStamina;
            speed = speedDefault;
            healAmount = maxHealth / 3;
            healthCooldown = 0;
            staminaCooldown = 0;
            transform.position = new Vector3(0, 0, 0);
        }

        // ENABLED ONLY DURING PLAY
        if (gameManagerScript.gameState == GameManager.state.InGame)
        {
            // DEFAULT MOVEMENT WHEN NO INPUT DETECTED
            velocity = new Vector3(0, 0, 0);

            // CUSTOM FUNCTIONS
            UpdateSpearSprite();
            RegenAndCooldowns();

            //----LEVEL UP WHEN REACH MAX XP-----
            if (xp >= maxXp)
            {
                xp -= maxXp;
                maxXp *= 1.2f; // +20% REQUIRED XP PER LEVEL UP
                level++;
                GameObject.Find(">GameManager<").GetComponent<GameManager>().NewNotification("LEVEL UP", 0);
                GetComponent<PlayerFXHandler>().PlayLevelUp();
            }

            //----HEALTH & DAMAGE FEEDBACK--------
            if (health <= 0)
            {
                gameManagerScript.gameState = GameManager.state.Lose;
                playerSprite.color = thisColour;
            }
            if (flashDamage)
            {
                if (flashTimer < 15) flashTimer++;
                if (flashTimer >= 5 && flashTimer < 10) playerSprite.color = Color.red;
                if (flashTimer >= 10) playerSprite.color = thisColour;
                if (flashTimer >= 15)
                {
                    GetComponent<PlayerFXHandler>().PlayTakeDamage();
                    flashDamage = false;
                    flashTimer = 0;
                }
            }

            // SPRINT BOOST
            if (activateBoost)
            {
                blockStaminaRegen = true;
                speed = speedDefault * 2.5f;
                if (boostTimer < 25) boostTimer++;
                if (boostTimer >= 25)
                {
                    boostTimer = 0;
                    activateBoost = false;
                }
            }
            else
            {
                blockStaminaRegen = false;
                speed = speedDefault;
            }
        }
    }
    public void ResetPlayerStats()
    {
        speedIncrease = 0;
        healthIncrease = 0;
        healthRequiredLevel = 1;
        speedRequiredLevel = 1;
        speedDefault = 0.12f;
        speed = speedDefault;
        maxHealth = 450;
        health = maxHealth;
        maxStamina = 100;
        stamina = maxStamina;
        level = 0;
        xp = 0;
        maxXp = 500;
        maxBombs = 3;
        maxMedkits = 3;
        powerShotCooldown = 240;
        UpdateSpearSprite();
        transform.position = new Vector3(0, 0, 0);
    }
    public void StartNewLevel()
    {
        transform.position = new Vector3(0, 0, 0);
    }
    public void TakeDamage(float damageAmount) // CALLED BY ENEMY WHEN INFLICTING DAMAGE TO PLAYER
    {
        if (!ignoreDamage)
        {
            GetComponent<PlayerFXHandler>().PlayTakeDamage();
            if (!GameObject.Find("Main Camera").GetComponent<CameraFollow>().pulseCameraIn) GameObject.Find("Main Camera").GetComponent<CameraFollow>().pulseCameraIn = true;
            GameObject.Find("HitScreen").GetComponent<HitDisplay>().pulseIn = true;
            flashDamage = true;
            healthCooldown = 120;
            if (health - damageAmount > 0)
            {
                health -= damageAmount;
                GameObject.Find(">GameManager<").GetComponent<StatHandler>().damageTaken += damageAmount;
            }
            else if (health - damageAmount <= 0) health = 0;
        }
    }
    private void RegenAndCooldowns()
    {
        // STAMINA REGENERATION
        if (staminaCooldown >= 45 && stamina < maxStamina && !blockStaminaRegen) stamina += 1.25f;

        // COOLDOWNS - MAINLY TO STOP DOUBLE USAGE INGAME OR WHEN CHANGING STATES
        if (bombCooldown < 45) bombCooldown++;
        if (potionCooldown < 45) potionCooldown++;
        if (staminaCooldown < 45) staminaCooldown++;
        if (powerShotCooldown < 240) powerShotCooldown++;
        if (projectileCooldown < maxProjectileCooldown) projectileCooldown++;
    }
    private void UpdateSpearSprite() // UPDATE SPEAR SPRITE AND COOLDOWN TIME
    {
        switch (currentSpear)
        {
            case 0:
                maxProjectileCooldown = 25;
                spearSprite.GetComponent<SpriteRenderer>().sprite = spearLvl0;
                break;
            case 1:
                maxProjectileCooldown = 20;
                spearSprite.GetComponent<SpriteRenderer>().sprite = spearLvl1;
                break;
            case 2:
                maxProjectileCooldown = 15;
                spearSprite.GetComponent<SpriteRenderer>().sprite = spearLvl2;
                break;
            case 3:
                maxProjectileCooldown = 10;
                spearSprite.GetComponent<SpriteRenderer>().sprite = spearLvl3;
                break;
            case 4:
                maxProjectileCooldown = 5;
                spearSprite.GetComponent<SpriteRenderer>().sprite = spearLvl4;
                break;
        }
    }
    private void SpawnProjectile(int fireType)
    {
        GetComponent<PlayerFXHandler>().PlayThrowSpear();

        //-----PROJECTILE SPAWNING---------
        ProjectileMovement projectileManager;
        newProjectile = Instantiate(projectilePrefab, GameObject.Find("ProjectileSpawn").transform.position, Quaternion.identity);
        projectileManager = newProjectile.GetComponent<ProjectileMovement>();
        projectileManager.isPlayerOwned = true;
        switch (fireType)
        {
            case 0:
                projectileManager.shootLocation = ProjectileMovement.location.Mouse;
                projectileManager.isPowershot = false;
                break;
            case 1:
                projectileManager.shootLocation = ProjectileMovement.location.ps1;
                projectileManager.isPowershot = true;
                break;
            case 2:
                projectileManager.shootLocation = ProjectileMovement.location.ps2;
                projectileManager.isPowershot = true;
                break;
            case 3:
                projectileManager.shootLocation = ProjectileMovement.location.ps3;
                projectileManager.isPowershot = true;
                break;
        }
        //---------------------------------

        GameObject.Find(">GameManager<").GetComponent<StatHandler>().spearsThrown++;
    }

    // CONTROLLING INPUTS
    public void InputMovement(float velX, float velY)
    {
        if (!activateBoost)
        {
            velocity = new Vector3(0, 0, 0);
        }
        velocity = new Vector3(speed * (velX * 1.25f), speed * (velY * 1.25f), 0);
        this.transform.position += velocity;
    }
    public void UsePotion()
    {
        if (totalMedkits > 0 && health < maxHealth)
        {
            GetComponent<PlayerFXHandler>().PlayDrinkPotion();
            GameObject.Find(">GameManager<").GetComponent<StatHandler>().potionsDrank++;
            if ((health + healAmount) < maxHealth) health += healAmount;
            else health = maxHealth;
            totalMedkits--;
        }
    }
    public void DropBomb()
    {
        if (totalBombs > 0 && bombCooldown >= 45)
        {
            bombCooldown = 0;
            totalBombs--;
            Instantiate(radialBurstObject, new Vector3(transform.position.x - 0.2f, transform.position.y - 0.75f, 0), Quaternion.identity);
            GameObject.Find(">GameManager<").GetComponent<StatHandler>().bombsDropped++;
        }
    }
    public void SingleShot()
    {
        if (projectileCooldown >= maxProjectileCooldown)
        {
            projectileCooldown = 0;
            GameObject.Find("SpearSprite").transform.localPosition = new Vector3(0.15f, -0.045f, 0.5f);
            shootLocation = new Vector3(0, 0, 0);
            SpawnProjectile(0);
        }
    }
    public void TripleShot()
    {
        if (powerShotCooldown >= 240)
        {
            powerShotCooldown = 0;
            GameObject.Find("SpearSprite").transform.localPosition = new Vector3(0.15f, -0.045f, 0.5f);
            SpawnProjectile(1);
            SpawnProjectile(2);
            SpawnProjectile(3);
        }
    }
    public void UseBoost()
    {
        if ((stamina - (maxStamina * 0.5f)) > 0 && isMoving && !isColliding && !activateBoost)
        {
            GetComponent<PlayerFXHandler>().PlaySprintBoost();
            GameObject.Find("Main Camera").GetComponent<CameraFollow>().pulseCameraOut = true;
            stamina -= (maxStamina * 0.5f);
            activateBoost = true;
        }
    }
}
