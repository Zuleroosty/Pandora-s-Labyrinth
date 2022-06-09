using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFXHandler : MonoBehaviour
{
    public AudioClip takeDamage, deathScream;
    private void Update()
    {
        GetComponent<AudioSource>().volume = GameObject.Find(">GameManager<").GetComponent<AudioHandler>().effectsMax;
    }
    public void PlayTakeDamage()
    {
        GetComponent<AudioSource>().pitch = 1f + Random.Range(0.1f, 0.3f);
        GetComponent<AudioSource>().PlayOneShot(takeDamage);
    }
    public void PlayDeathScream()
    {
        GetComponent<AudioSource>().pitch = 1f + Random.Range(0.2f, 0.4f);
        GetComponent<AudioSource>().PlayOneShot(deathScream);
    }
}
