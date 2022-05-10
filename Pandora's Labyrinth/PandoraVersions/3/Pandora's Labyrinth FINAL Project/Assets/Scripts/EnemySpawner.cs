using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameObject gameManager;
    PermissionsHandler permHandler;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find(">GameManager<");
        permHandler = gameManager.GetComponent<PermissionsHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (permHandler.canSpawn)
        {
            // Timer -> Spawn -> Reset
        }
    }
}
