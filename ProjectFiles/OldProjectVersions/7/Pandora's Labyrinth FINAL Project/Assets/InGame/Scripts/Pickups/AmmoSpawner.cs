using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    GameManager gameManager;
    GameObject ammoObject, playerObject, newAmmoObject;
    int spawnTimer, delayTime;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find(">GameManager<").GetComponent<GameManager>();
        playerObject = GameObject.Find("----PlayerObjectParent----");
        ammoObject = gameManager.ammoDrop;

        delayTime = Random.Range(240, 361);
    }

    // Update is called once per frame
    void Update()
    {
        if (newAmmoObject == null)
        {
            if (spawnTimer < delayTime) spawnTimer++;
            if (spawnTimer >= delayTime)
            {
            }
        }
    }
}
