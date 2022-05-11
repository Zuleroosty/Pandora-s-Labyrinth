using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUpdater : MonoBehaviour
{
    public int healthLvl, damageLvl;

    public void UpgradeLevel(bool isHealth)
    {
        if (isHealth)
        {
            healthLvl++;
            GetComponent<PlayerController>().maxHealth *= 1.05f;
        }
        else
        {
            damageLvl++;
            GetComponent<PlayerController>().projectileDamage *= 1.05f;
        }
    }
}
