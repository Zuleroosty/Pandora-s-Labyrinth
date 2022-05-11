using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount;
    bool hasCollected;

    // Start is called before the first frame update
    void Start()
    {
        ammoAmount = Random.Range(8, 12);
        transform.parent = GameObject.Find("----Loot----").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasCollected)
        {
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
