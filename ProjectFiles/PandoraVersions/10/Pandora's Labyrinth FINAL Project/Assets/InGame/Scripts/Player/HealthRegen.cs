using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegen : MonoBehaviour
{
    public float health, maxHealth, regenTimer;
    PlayerController playerCon;

    // Start is called before the first frame update
    void Start()
    {
        playerCon = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        health = playerCon.health;
        maxHealth = playerCon.maxHealth;
        if (playerCon.canRegenHealth)
        {
            if (health > maxHealth * 0.8f)
            {
                if (health < maxHealth) health += 0.05f;
            }
            else if (health > maxHealth * 0.6f)
            {
                if (health < maxHealth * 0.8f) health += 0.05f;
            }
            else if (health > maxHealth * 0.4f)
            {
                if (health < maxHealth * 0.6f) health += 0.05f;
            }
            else if (health > maxHealth * 0.2f)
            {
                if (health < maxHealth * 0.4f) health += 0.05f;
            }
            else if (health > 0)
            {
                if (health < maxHealth * 0.2f) health += 0.05f;
            }
        }
    }
}
