using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDestroy : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Reset) Destroy(gameObject);
    }
}
