using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereMusicHandler : MonoBehaviour
{
    private void Start()
    {
        GetComponent<AudioSource>().volume = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (!GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Play();
            if (GetComponent<AudioSource>().volume < GameObject.Find(">GameManager<").GetComponent<AudioHandler>().ambienceMax) GetComponent<AudioSource>().volume += 0.01f;
        }
        else
        {
            if (GetComponent<AudioSource>().volume > 0) GetComponent<AudioSource>().volume -= 0.01f;
            else if (GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Stop();
        }
    }
}
