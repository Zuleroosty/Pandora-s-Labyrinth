using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    public GameObject xpSpawnObject;
    GameObject gameManager, ammoObject, healthObject, newXpObject;
    public int healthChance, ammoChance, chanceOutcome, xpDrop;
    int randomNum;

    private void Start()
    {
        gameManager = GameObject.Find(">GameManager<");

        ammoObject = gameManager.GetComponent<GameManager>().ammoDrop;
        healthObject = gameManager.GetComponent<GameManager>().healthDrop;
    }

    private void OnDestroy()
    {
        if (GameObject.Find("----PlayerObjectParent----") != null)
        {
            if (GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().health <= GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().maxHealth / 5)
            { //CHANGE HEALTH DROP CHANCE BASED ON PLAYER HEALTH
                healthChance = 22;
                ammoChance = 8;
                //EMPTY DROP ALWAYS 60
            }
            else
            {
                healthChance = 8;
                ammoChance = 22;
            }
            chanceOutcome = 0;
            randomNum = Random.Range(0, 101);
            if (randomNum <= 45) chanceOutcome = 60;
            else
            {
                randomNum -= 45;
                if (randomNum <= 25) chanceOutcome = 22;
                else
                {
                    randomNum -= 25;
                    if (randomNum <= 20) randomNum -= 20;
                    if (randomNum <= 10) SpawnUpgrade();
                }
            }
            if (chanceOutcome == healthChance) Instantiate(healthObject, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
            if (chanceOutcome == ammoChance) Instantiate(ammoObject, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
            newXpObject = Instantiate(xpSpawnObject, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
            newXpObject.GetComponent<XPSpawner>().xpMax = xpDrop;
        }
    }
    void SpawnUpgrade()
    {
        randomNum = Random.Range(0, 101);
        if (randomNum <= 45) chanceOutcome = 60;
        else
        {
            randomNum -= 45;
            if (randomNum <= 25) chanceOutcome = 22;
            else
            {
                randomNum -= 25;
                if (randomNum <= 20) randomNum -= 20;
                if (randomNum <= 10) chanceOutcome = 8;
            }
        }
    }
}
