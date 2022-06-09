using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    public GameObject xpPrefab, goldPrefab;
    public float xpDrop;
    int randNum;

    private void Start()
    {
        xpDrop *= (1 + GameObject.Find(">GameManager<").GetComponent<LevelHandler>().averagePlayerLevel);
    }

    private void OnDestroy()
    {
        if (GameObject.Find("----PlayerObjectParent----") != null)
        {
            if (!GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().hasPandorasBox) DropXP();
            if (this.name.Contains("Boss"))
            {
                DropXP();

                // AMOUNT OF GOLD TO DROP
                randNum = Random.Range(1, 4);
                if (randNum > 0) Instantiate(goldPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
                if (randNum > 1) Instantiate(goldPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
                if (randNum > 2) Instantiate(goldPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
                if (randNum > 3) Instantiate(goldPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
            }
        }
    }
    void DropXP()
    {
        if (xpDrop > 0) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 1) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 2) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 3) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 4) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 5) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 6) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 7) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 8) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 9) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 10) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 11) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 12) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 13) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 14) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 15) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 16) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 17) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 18) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 19) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 20) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 21) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 22) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 23) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 24) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 25) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 26) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 27) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 28) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        if (xpDrop > 29) Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
    }
}
