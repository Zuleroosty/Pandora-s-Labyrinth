using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPath : MonoBehaviour
{
    public GameObject startPath;
    int timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 5) timer++;
        else
        {
            timer = 0;
            if (!GetComponent<SpriteRenderer>().bounds.Intersects(GameObject.Find("B").GetComponent<SpriteRenderer>().bounds))
            {
                transform.position = GameObject.Find("B").transform.position;
                GameObject.Find("A").GetComponent<EnemyAI>().SpawnNewPath();
            }
        }
    }
}
