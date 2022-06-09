using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip soundFX;
    private int randNum, randTimer;
    private bool hasMinotaurSpawned;

    private void Start()
    {
        randNum = Random.Range(120, 601);
    }
    private void Update()
    {
        // RANDOMLY PLAY MINOTAUR ROAR DURING PLAY UNTIL MINOTAUR HAS BEEN SPAWNED
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (GameObject.Find("BossEnemy(Clone)") != null) hasMinotaurSpawned = true;
            if (!hasMinotaurSpawned)
            {
                if (randTimer < randNum) randTimer++;
                if (randTimer >= randNum)
                {
                    randTimer = 0;
                    randNum = Random.Range(360, 1801);
                    GetComponent<AudioSource>().volume = GameObject.Find(">GameManager<").GetComponent<AudioHandler>().ambienceMax + Random.Range(0.000f, 0.075f);
                    GetComponent<AudioSource>().pitch = 1f + Random.Range(0.01f, 0.03f);
                    GetComponent<AudioSource>().PlayOneShot(soundFX);
                }
            }
        }
        else if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Reset) hasMinotaurSpawned = false;
    }
}
