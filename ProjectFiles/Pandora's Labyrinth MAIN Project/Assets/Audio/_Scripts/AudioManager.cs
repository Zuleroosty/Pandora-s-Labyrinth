using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip soundFX;
    int randNum, randTimer;
    private void Start()
    {
        randNum = Random.Range(120, 1200);
    }
    private void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame && GameObject.Find("BossEnemy(Clone)") == null)
        {
            if (randTimer < randNum) randTimer++;
            if (randTimer >= randNum)
            {
                randTimer = 0;
                randNum = Random.Range(360, 1800);
                GetComponent<AudioSource>().volume = GameObject.Find(">GameManager<").GetComponent<AudioHandler>().ambienceMax + Random.Range(-0.075f, 0.075f);
                GetComponent<AudioSource>().pitch = 1f + Random.Range(0.01f, 0.03f);
                GetComponent<AudioSource>().PlayOneShot(soundFX);
            }
        }
    }
}
