using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFXHandler : MonoBehaviour
{
    public AudioClip hitWood, hitStone, hitMetal, hitEntity1, hitEntity2;
    int randNum;

    public void PlayHitFX(int output)
    {
        GetComponent<AudioSource>().volume = 0.1f + Random.Range(0.02f, 0.06f);
        switch(output)
        {
            case 1: // wood
                GetComponent<AudioSource>().PlayOneShot(hitWood);
                break;
            case 2: // stone
                GetComponent<AudioSource>().PlayOneShot(hitStone);
                break;
            case 3: // metal
                GetComponent<AudioSource>().PlayOneShot(hitMetal);
                break;
            case 4: // entity
                randNum = Random.Range(1, 3);
                if (randNum == 1) GetComponent<AudioSource>().PlayOneShot(hitEntity1);
                else GetComponent<AudioSource>().PlayOneShot(hitEntity2);
                break;

        }
        
    }
}
