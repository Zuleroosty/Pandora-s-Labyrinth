using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    public GameObject scorePrefab, scoreLocation, newScoreObject;
    public float health, maxHealth, flashTimer, offSet;
    public AudioClip deathFX1, deathFX2, painFX1, painFX2, painFX3;
    public bool flashDamage, radialDamage;
    private SpriteRenderer projectileSprite;
    private Color thisColour;
    private GameObject projectileParent;
    private int childID, childMax, randNum, damageScore, deathScore;

    private void Start()
    {
        projectileParent = GameObject.Find("----ProjectileParent----");
        thisColour = GetComponent<EnemyAI>().damageObject.GetComponent<SpriteRenderer>().color;
        thisColour.a = 0;
        GetComponent<EnemyAI>().damageObject.GetComponent<SpriteRenderer>().color = thisColour;

        offSet = 0.5f;
        offSet += (GameObject.Find(">GameManager<").GetComponent<LevelHandler>().averagePlayerLevel + GameObject.Find(">GameManager<").GetComponent<LevelHandler>().currentPlayLevel) * 0.2f;

        if (this.name.Contains("Ranged"))
        {
            maxHealth = 230 * offSet;
            damageScore = 25;
            deathScore = 315;
        }
        if (this.name.Contains("Fast"))
        {
            maxHealth = 55 * offSet;
            damageScore = 5;
            deathScore = 125;
        }
        if (this.name.Contains("Normal"))
        {
            maxHealth = 160 * offSet;
            damageScore = 15;
            deathScore = 255;
        }
        if (this.name.Contains("Boss"))
        {
            maxHealth = 5325 * offSet;
            damageScore = 55;
            deathScore = 10450;
        }

        health = maxHealth;

        if (!this.name.Contains("Boss") && GameObject.Find("BossEnemy(Clone)") == null) GameObject.Find(">GameManager<").GetComponent<GameManager>().currentRoomParent.GetComponent<RoomHandler>().enemyCount++;
    }

    private void Update()
    {
        if (projectileParent.transform.childCount > 0) ProjectileCollisionTest(); //REPEATED TO AVOID MISSING

        if (flashDamage)
        {
            if (flashTimer < 15) flashTimer++;
            if (flashTimer >= 5 && flashTimer < 10) thisColour.a = 1;
            if (flashTimer >= 10) thisColour.a = 0;
            if (flashTimer >= 15)
            {
                flashDamage = false;
                flashTimer = 0;
            }
        }

        GetComponent<EnemyAI>().damageObject.GetComponent<SpriteRenderer>().color = thisColour;

        if (health <= 0)
        {
            newScoreObject = Instantiate(scorePrefab, scoreLocation.transform.position, Quaternion.identity);
            newScoreObject.GetComponent<ScoreDisplayAnim>().scoreToDisplay = deathScore;

            randNum = Random.Range(0, 1);
            switch (randNum)
            {
                case 0:
                    GameObject.Find(">GameManager<").GetComponent<GameManager>().SpawnNew3DFX(deathFX1, transform.position);
                    break;
                case 1:
                    if (deathFX2 == null) GameObject.Find(">GameManager<").GetComponent<GameManager>().SpawnNew3DFX(deathFX1, transform.position);
                    else GameObject.Find(">GameManager<").GetComponent<GameManager>().SpawnNew3DFX(deathFX2, transform.position);
                    break;
            }

            // UPDATE STAT TRACKERS
            switch (GetComponent<EnemyAI>().enemyType)
            {
                case EnemyAI.type.Normal:
                    GameObject.Find(">GameManager<").GetComponent<StatHandler>().goblinsKilled++;
                    if (GameObject.Find("BossEnemy(Clone)") == null) GameObject.Find(">GameManager<").GetComponent<GameManager>().currentRoomParent.GetComponent<RoomHandler>().enemyCount--;
                    break;
                case EnemyAI.type.Fast:
                    GameObject.Find(">GameManager<").GetComponent<StatHandler>().spidersKilled++;
                    if (GameObject.Find("BossEnemy(Clone)") == null) GameObject.Find(">GameManager<").GetComponent<GameManager>().currentRoomParent.GetComponent<RoomHandler>().enemyCount--;
                    break;
                case EnemyAI.type.Ranged:
                    GameObject.Find(">GameManager<").GetComponent<StatHandler>().scorpionsKilled++;
                    if (GameObject.Find("BossEnemy(Clone)") == null) GameObject.Find(">GameManager<").GetComponent<GameManager>().currentRoomParent.GetComponent<RoomHandler>().enemyCount--;
                    break;
                case EnemyAI.type.Hard:
                    GameObject.Find(">GameManager<").GetComponent<StatHandler>().minotaursKilled++;
                    if (GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().inCombat) GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().inCombat = false;
                    GameObject.Find(">GameManager<").GetComponent<GameManager>().UpdateObjective("- EXIT THE LABYRINTH");
                    break;
            }

            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        transform.position += (transform.position - projectileSprite.transform.position);
        if (GetComponent<EnemyAI>().activateBoost) GetComponent<EnemyAI>().boostTimer = 20;
        if (health - damageAmount < 0)
        {
            GameObject.Find(">GameManager<").GetComponent<StatHandler>().damageDealt += health;
            health = 0;
        }
        else
        {
            health -= damageAmount;
            GameObject.Find(">GameManager<").GetComponent<StatHandler>().damageDealt += damageAmount;
            newScoreObject = Instantiate(scorePrefab, scoreLocation.transform.position, Quaternion.identity);
            newScoreObject.GetComponent<ScoreDisplayAnim>().scoreToDisplay = damageScore;
        }

        randNum = Random.Range(0, 3);
        switch (randNum)
        {
            case 0:
                GameObject.Find(">GameManager<").GetComponent<GameManager>().SpawnNew3DFX(painFX1, transform.position);
                break;
            case 1:
                GameObject.Find(">GameManager<").GetComponent<GameManager>().SpawnNew3DFX(painFX2, transform.position);
                break;
            case 3:
                GameObject.Find(">GameManager<").GetComponent<GameManager>().SpawnNew3DFX(painFX3, transform.position);
                break;
        }
    }
    private void ProjectileCollisionTest()
    {
        if (childID < childMax && childMax == projectileParent.transform.childCount)
        {
            projectileSprite = projectileParent.transform.GetChild(childID).GetComponent<SpriteRenderer>();
            if (projectileSprite.name.Contains("Explosion"))
            {
                if (projectileSprite.GetComponent<SpriteRenderer>().bounds.Intersects(GetComponent<EnemyAI>().collisionObject.GetComponent<SpriteRenderer>().bounds))
                {
                        if (!radialDamage)
                        {
                            TakeDamage(200 * offSet);
                            flashDamage = true;
                            radialDamage = true;
                        }
                }
                else radialDamage = false;
            }
            else if (projectileSprite.gameObject.GetComponent<ProjectileMovement>().isPlayerOwned)
            {
                if (GetComponent<EnemyAI>().collisionObject.GetComponent<SpriteRenderer>().bounds.Intersects(projectileSprite.bounds))
                {
                    flashDamage = true;
                    projectileSprite.gameObject.GetComponent<ProjectileMovement>().enemyObject = this.gameObject;
                    projectileSprite.gameObject.GetComponent<ProjectileMovement>().OnCollisionDestroy(4);
                }
            }
        }
        else
        {
            childID = -1;
            childMax = projectileParent.transform.childCount;
        }
        childID++;
    }
}
