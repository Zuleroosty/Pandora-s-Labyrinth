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

        speedDefault = 0.1f;
        speed = speedDefault;

        maxHealth = 650;
        health = maxHealth;

        maxStamina = 100;
        stamina = maxStamina;

        maxShield = 4;
        shield = 0;

        level = 0;
        xp = 0;
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
            velocity = new Vector3(0, 0, 0);
            UpdateSpearSprite();
            RegenAndCooldowns();

            //----LEVEL UP WHEN REACH MAX XP-----
            if (xp >= maxXp)
            {
                xp -= maxXp;
                maxXp *= 1.2f;
                level++;
                GameObject.Find(">GameManager<").GetComponent<GameManager>().NewNotification("LEVEL UP", 0);
                GetComponent<PlayerFXHandler>().PlayLevelUp();
            }

            //----HEALTH & FEEDBACK--------
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
        }
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
            if (shield > 0) shield--;
            else if (health - damageAmount > 0)
            {
                health -= damageAmount;
                GameObject.Find(">GameManager<").GetComponent<StatHandler>().damageTaken += damageAmount;
            }
            else if (health - damageAmount <= 0) health = 0;
        }
    }
    private void RegenAndCooldowns()
    {
        // SHOOTING COOLDOWN
        if (projectileCooldown < maxProjectileCooldown)
        {
            projectileCooldown++;
        }
        // TRIPLE SHOT COOLDOWN
        if (powerShot)
        {
            powerShotCooldown++;
            if (powerShotCooldown >= 240)
            {
                powerShot = false;
            }
        }
        // STAMINA REGENERATION
        if (!blockStaminaRegen && stamina < maxStamina)
        {
            staminaCooldown++;
            if (staminaCooldown >= 45)
            {
                stamina += 1.25f;
            }
        }
        else if (stamina > maxStamina) stamina = maxStamina;
    }
    private void UpdateSpearSprite() // UPDATE SPEAR SPRITE AND COOLDOWN TIME
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
        if (health < maxHealth && totalMedkits > 0)
        {
            if ((health + (maxHealth / 3)) < maxHealth)
            {
                health += (maxHealth / 3);
            }
            else health = maxHealth;
            totalMedkits--;
            GameObject.Find(">GameManager<").GetComponent<StatHandler>().potionsDrank++;
            GetComponent<PlayerFXHandler>().PlayDrinkPotion();
        }
    }
    public void DropBomb()
    {
        if (totalBombs > 0)
        {
            totalBombs--;
            Instantiate(radialBurstObject, new Vector3(transform.position.x - 0.2f, transform.position.y - 0.75f, 0), Quaternion.identity);
            GameObject.Find(">GameManager<").GetComponent<StatHandler>().bombsDropped++;
        }
    }
    public void SingleShot()
    {
        if (projectileCooldown >= maxProjectileCooldown)
        {
            GameObject.Find("SpearSprite").transform.localPosition = new Vector3(0.15f, -0.045f, 0.5f);
            projectileCooldown = 0;
            shootLocation = new Vector3(0, 0, 0);
            SpawnProjectile(0);
        }
    }
    public void TripleShot()
    {
        if (!powerShot)
        {
            GameObject.Find("SpearSprite").transform.localPosition = new Vector3(0.15f, -0.045f, 0.5f);
            powerShotCooldown = 0;
            powerShot = true;
            SpawnProjectile(1);
            SpawnProjectile(2);
            SpawnProjectile(3);
        }
    }
    public void UseBoost()
    {
        if (stamina >= maxStamina / 2 && isMoving)
        {
            if (!activateBoost && !isColliding)
            {
                GetComponent<PlayerFXHandler>().PlaySprintBoost();
                GameObject.Find("Main Camera").GetComponent<CameraFollow>().pulseCameraOut = true;
                stamina -= maxStamina / 2;
                activateBoost = true;
            }
        }
    }
}
