using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject targetObject;
    public bool isPlayer, isEnemy, isSpawner, isBarrel;
    public float health, maxHealth;
    public Vector3 scaleAdjuster, spawnScale;

    // Start is called before the first frame update
    void Start()
    {
        targetObject = transform.parent.parent.gameObject;

        if (targetObject.name.Contains("Player")) isPlayer = true;
        else isPlayer = false;
        if (targetObject.name.Contains("Enemy")) isEnemy = true;
        else isEnemy = false;
        if (targetObject.name.Contains("Spawner")) isSpawner = true;
        else isSpawner = false;
        if (targetObject.name.Contains("Barrel")) isBarrel = true;
        else isBarrel = false;

        scaleAdjuster = transform.localScale;
        spawnScale = transform.parent.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemy)
        {
            health = targetObject.GetComponent<EnemyHealthHandler>().health;
            maxHealth = targetObject.GetComponent<EnemyHealthHandler>().maxHealth;
        }
        else if (isPlayer)
        {
            health = targetObject.GetComponent<PlayerController>().health;
            maxHealth = targetObject.GetComponent<PlayerController>().maxHealth;
        }
        else if (isBarrel)
        {
            health = targetObject.GetComponent<BarrelHealthHandler>().health;
            maxHealth = targetObject.GetComponent<BarrelHealthHandler>().maxHealth;
        }

        scaleAdjuster.x = health / maxHealth;
        if (scaleAdjuster.x < 1) transform.localScale = scaleAdjuster;

        if (scaleAdjuster.x >= 1)
        {
            transform.parent.transform.localScale = new Vector3(0, 0, 1);
        }
        else transform.parent.transform.localScale = spawnScale;
    }
}
