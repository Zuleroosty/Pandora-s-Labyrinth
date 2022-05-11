using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public float effectsMax, ambienceMax, musicMax, averageVolume;
    public AudioClip menuMusic, combatMusic;

    // Update is called once per frame
    void Update()
    {
        effectsMax = averageVolume * 0.75f;
        ambienceMax = averageVolume * 0.25f;
        musicMax = averageVolume * 0.15f;

        if (GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().inCombat)
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().clip = combatMusic;
                GetComponent<AudioSource>().Play();
            }
            if (GetComponent<AudioSource>().volume < musicMax) GetComponent<AudioSource>().volume += 0.01f;
            if (GetComponent<AudioSource>().volume > musicMax) GetComponent<AudioSource>().volume -= 0.01f;
        }
        else if (GetComponent<GameManager>().gameState == GameManager.state.Menu)
        {
            if (!GameObject.Find("EnterButton").GetComponent<EnterButton>().activated)
            {
                if (!GetComponent<AudioSource>().isPlaying)
                {
                    GetComponent<AudioSource>().clip = menuMusic;
                    GetComponent<AudioSource>().Play();
                }
                if (GetComponent<AudioSource>().volume < musicMax) GetComponent<AudioSource>().volume += 0.01f;
                if (GetComponent<AudioSource>().volume > musicMax) GetComponent<AudioSource>().volume -= 0.01f;
            }
            else if (GetComponent<AudioSource>().volume > 0) GetComponent<AudioSource>().volume -= 0.01f;
            else if (GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Stop();
        }
        else if (GetComponent<AudioSource>().volume > 0) GetComponent<AudioSource>().volume -= 0.01f;
        else if (GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Stop();
    }
}
