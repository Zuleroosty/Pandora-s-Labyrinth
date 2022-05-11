using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public float effectsMax, ambienceMax, musicMax, averageVolume;
    public AudioClip menuMusic, combatMusic, winMusic, loseMusic;
    public bool playEndMusic, storyScreen;

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

        if (GetComponent<GameManager>().gameState == GameManager.state.InGame) storyScreen = false;
        if (GetComponent<GameManager>().gameState == GameManager.state.Reset || GetComponent<GameManager>().gameState == GameManager.state.GenLevel || storyScreen)
        {
            if (GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Stop();
        }
        else
        {
            if (GetComponent<GameManager>().gameState == GameManager.state.Win || GetComponent<GameManager>().gameState == GameManager.state.Lose)
            {
                if (!playEndMusic)
                {
                    if (GetComponent<GameManager>().gameState == GameManager.state.Win)     // WAITING FOR AUDIO TRACKS
                    {
                        GetComponent<AudioSource>().clip = winMusic;
                        GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        GetComponent<AudioSource>().clip = loseMusic;
                        GetComponent<AudioSource>().Play();
                    }
                    playEndMusic = true;
                    AdjustVolume();
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
                    AdjustVolume();
                }
                if (GetComponent<GameManager>().gameState == GameManager.state.Menu)
                {
                    if (!GetComponent<AudioSource>().isPlaying)
                    {
                        GetComponent<AudioSource>().clip = menuMusic;
                        GetComponent<AudioSource>().Play();
                    }
                    AdjustVolume();
                }
                else if (GetComponent<AudioSource>().isPlaying)
                {
                    if (GetComponent<AudioSource>().volume > 0) GetComponent<AudioSource>().volume -= 0.001f;
                    else GetComponent<AudioSource>().Stop();
                }
            }
        }
    }
    void AdjustVolume()
    {
        if (GetComponent<AudioSource>().volume < musicMax) GetComponent<AudioSource>().volume += 0.012f;
        if (GetComponent<AudioSource>().volume > musicMax) GetComponent<AudioSource>().volume -= 0.012f;
    }
}
