using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    public GameObject xpSpawnObject, goldSpawnObject, healthDrop, bombDrop, newXpObject;
    public int healthChance, ammoChance, chanceOutcome, xpDrop, goldDrop;
    public bool isDisabled;
    int randomNum;

    private void Start()
    {
        if (xpDrop <= 0) xpDrop = Random.Range(1, 4);
        goldDrop = Random.Range(3, 6);
    }

    private void OnDestroy()
    {
        if (GameObject.Find("----PlayerObjectParent----") != null && !isDisabled)
        {
            newXpObject = Instantiate(xpSpawnObject, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
            if (xpDrop > 1) newXpObject = Instantiate(xpSpawnObject, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
            if (xpDrop > 2) newXpObject = Instantiate(xpSpawnObject, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
            if (xpDrop > 3) newXpObject = Instantiate(xpSpawnObject, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
            if (xpDrop > 4) newXpObject = Instantiate(xpSpawnObject, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
            if (xpDrop > 5) newXpObject = Instantiate(xpSpawnObject, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
            if (xpDrop > 6) newXpObject = Instantiate(xpSpawnObject, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
            if (xpDrop > 7) newXpObject = Instantiate(xpSpawnObject, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
            if (xpDrop > 8) newXpObject = Instantiate(xpSpawnObject, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
            if (xpDrop > 9) newXpObject = Instantiate(xpSpawnObject, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
            if (GameObject.Find("BossEnemy") != null)
            {
                if (this.name.Contains("Boss"))
                {
                    if (goldDrop > 1) Instantiate(goldSpawnObject, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
                    if (goldDrop > 2) Instantiate(goldSpawnObject, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
                    if (goldDrop > 3) Instantiate(goldSpawnObject, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
                    if (goldDrop > 4) Instantiate(goldSpawnObject, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
                    if (goldDrop > 5) Instantiate(goldSpawnObject, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
                }
                else
                {
                    newXpObject = Instantiate(xpSpawnObject, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
                    DropHealthBomb();
                }
            }
        }
    }
    void DropHealthBomb() // ARMOUR - MINOTAUR ONLY
    {
        randomNum = Random.Range(0, 101);
        if (randomNum <= 45) Instantiate(bombDrop, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        else
        {
            randomNum -= 45;
            if (randomNum <= 25) Instantiate(healthDrop, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        }
    }
}
