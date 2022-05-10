using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurSpawner : MonoBehaviour
{
    public GameObject gameManager, parentRoomObject, normalPrefab, fastPrefab, rangedPrefab, newEnemyPrefab;
    PermissionsHandler permHandler;
    int randomTimer, timer, randNum, enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find(">GameManager<");
        permHandler = gameManager.GetComponent<PermissionsHandler>();
        parentRoomObject = transform.parent.parent.gameObject;

        randomTimer = Random.Range(120, 241);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("----EnemyParent----").transform.childCount < 6)
        {
            if (timer < randomTimer) timer++;
            if (timer >= randomTimer)
            {
                randNum = Random.Range(0, 101);
                if (randNum <= 65) Instantiate(normalPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                else
                {
                    Instantiate(rangedPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                }
                randomTimer = Random.Range(120, 121);
                timer = 0;
                enemyCount++;
            }
        }
    }
}
