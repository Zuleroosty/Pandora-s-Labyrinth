using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public float effectsMax, ambienceMax, musicMax, averageVolume;
    public AudioClip menuMusic, combatMusic, winMusic, loseMusic;
    bool playEndMusic;
    private void Start()
    {
        if (averageVolume == 0) averageVolume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        effectsMax = averageVolume * 0.6f;
        ambienceMax = averageVolume * 0.3f;
        musicMax = averageVolume * 0.35f;

        if (GetComponent<AudioSource>().volume < musicMax) GetComponent<AudioSource>().volume += 0.01f;
        if (GetComponent<AudioSource>().volume > musicMax) GetComponent<AudioSource>().volume -= 0.01f;

        if (GetComponent<GameManager>().gameState == GameManager.state.Reset || GetComponent<GameManager>().gameState == GameManager.state.GenLevel)
        {
            if (GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Stop();
        }
        else
        {
            if (GetComponent<GameManager>().gameState == GameManager.state.Win || GetComponent<GameManager>().gameState == GameManager.state.Lose)
            {
                if (!playEndMusic)
                {
                    //if (GetComponent<GameManager>().gameState == GameManager.state.Win)     // WAITING FOR AUDIO TRACKS
                    //{
                    //    GetComponent<AudioSource>().clip = winMusic;
                    //    GetComponent<AudioSource>().Play();
                    //}
                    //else
                    //{
                    //    GetComponent<AudioSource>().clip = loseMusic;
                    //    GetComponent<AudioSource>().Play();
                    //}
                    //playEndMusic = true;
                }
            }
            else
            {
                playEndMusic = false;
                if (GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().inCombat)
                {
                    if (!GetComponent<AudioSource>().isPlaying)
                    {
                        GetComponent<AudioSource>().clip = combatMusic;
                        GetComponent<AudioSource>().Play();
                    }
                }
                else if (GetComponent<GameManager>().gameState == GameManager.state.Menu)
                {
                    if (!GetComponent<AudioSource>().isPlaying)
                    {
                        GetComponent<AudioSource>().clip = menuMusic;
                        GetComponent<AudioSource>().Play();
                    }
                }
                else if (GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Stop();
            }
        }
    }
}
