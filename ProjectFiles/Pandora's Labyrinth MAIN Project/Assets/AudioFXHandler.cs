using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFXHandler : MonoBehaviour
{
    private void Start()
    {
        // PARENT ON SPAWN
        transform.parent = GameObject.Find("----AudioTemp----").transform;
        // RANDOMISE AUDIO OUTPUT
        GetComponent<AudioSource>().pitch += Random.Range(-0.03f, 0.03f);
        GetComponent<AudioSource>().volume = Random.Range(0.08f, 0.11f);
        Destroy(gameObject, 3);
    }
}
