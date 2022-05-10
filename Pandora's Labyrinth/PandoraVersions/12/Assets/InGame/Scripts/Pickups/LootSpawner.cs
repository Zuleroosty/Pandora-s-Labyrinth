using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    public GameObject xpSpawnObject;
    GameObject gameManager, newXpObject;
    public int healthChance, ammoChance, chanceOutcome, xpDrop;
    int randomNum;

    private void Start()
    {
        gameManager = GameObject.Find(">GameManager<");

        xpDrop = Random.Range(1, 4);
    }

    private void OnDestroy()
    {
        if (GameObject.Find("----PlayerObjectParent----") != null)
        {
            newXpObject = Instantiate(xpSpawnObject, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
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
