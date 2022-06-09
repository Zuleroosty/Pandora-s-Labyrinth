using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnTimer : MonoBehaviour
{
    public float seconds;
    int timer;

    // Start is called before the first frame update
    void Start()
    {
        if (seconds == 0) seconds = 30;
    }

    // Update is called once per frame
    void Update()
    {
        if (name.Contains("XPPickup"))
        {
            if (!GetComponent<XPPickup>().hasCollected)
            {
                Timer();
            }
        }
        else
        {
            Timer();
        }
    }
    void Timer()
    {
        timer++;
        if (timer >= seconds * 60) Destroy(gameObject);
    }
}
