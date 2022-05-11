using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int finishedBlocks, totalBlocks;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (finishedBlocks >= totalBlocks)
        {
            print("Puzzle Complete");
        }
    }
}
