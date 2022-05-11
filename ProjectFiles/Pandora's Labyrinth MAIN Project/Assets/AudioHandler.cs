using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public float effectsMax, ambienceMax, musicMax, averageVolume;
    public AudioClip menuMusic, combatMusic, bossMusic, winMusic, loseMusic, introVoiceOver;
    public bool storyScreen;
    int storyDelay;

    private void Start()
    {
        if (averageVolume == 0) averageVolume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        effectsMax = averageVolume * 0.525f;
        ambienceMax = averageVolume * 0.325f;
        musicMax = averageVolume * 0.15f;

        if (GetComponent<GameManager>().gameState == GameManager.state.Start)
        {
            storyScreen = false;
            if (GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Stop();
        }
        if (GetComponent<GameManager>().gameState == GameManager.state.Reset || GetComponent<GameManager>().gameState == GameManager.state.GenLevel)
        {
            if (GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Stop();
        }
        else
        {
            if (GetComponent<GameManager>().gameState == GameManager.state.Win || GetComponent<GameManager>().gameState == GameManager.state.Lose)
            {
                if (!GetComponent<AudioSource>().isPlaying)
                {
                    if (GetComponent<GameManager>().gameState == GameManager.state.Win)
                    {
                        GetComponent<AudioSource>().clip = winMusic;
                        GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        GetComponent<AudioSource>().clip = loseMusic;
                        GetComponent<AudioSource>().Play();
                    }
                }
                storyDelay = 0;
                GetComponent<AudioSource>().volume = averageVolume * 0.525f;
            }
            else
            {
                GetComponent<AudioSource>().loop = true;
                if (GetComponent<GameManager>().gameState == GameManager.state.Menu)
                {
                    if (storyScreen)
                    {
                        if (storyDelay < 60)
                        {
                            storyDelay++;
                            if (GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Stop();
                        }
                        if (storyDelay >= 60)
                        {
                            GetComponent<AudioSource>().volume = effectsMax;
                            GetComponent<AudioSource>().loop = false;
                            if (GetComponent<AudioSource>().clip != introVoiceOver)
                            {
                                GetComponent<AudioSource>().clip = introVoiceOver;
                                GetComponent<AudioSource>().Play();
                            }
                        }
                        else if (GetComponent<AudioSource>().volume > 0) GetComponent<AudioSource>().volume -= 0.001f;
                        else GetComponent<AudioSource>().Stop();
                    }
                    else
                    {
                        if (!GetComponent<AudioSource>().isPlaying)
                        {
                            GetComponent<AudioSource>().clip = menuMusic;
                            GetComponent<AudioSource>().Play();
                        }
                        AdjustVolume();
                    }
                }
                else if (GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().inCombat)
                {
                    if (!GetComponent<AudioSource>().isPlaying)
                    {
                        GetComponent<AudioSource>().clip = combatMusic;
                        GetComponent<AudioSource>().Play();
                    }
                    AdjustVolume();
                }
                else if (GameObject.Find("BossEnemy(Clone)") != null)
                {
                    if (!GetComponent<AudioSource>().isPlaying)
                    {
                        GetComponent<AudioSource>().clip = bossMusic;
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
