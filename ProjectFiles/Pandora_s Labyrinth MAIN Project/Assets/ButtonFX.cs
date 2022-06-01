using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFX : MonoBehaviour
{
    public AudioClip hover, press, reject, pause, unPause;
    private void Update()
    {
        GetComponent<AudioSource>().volume = transform.parent.GetComponent<AudioHandler>().effectsMax * 0.25f;
    }

    public void ButtonHover()
    {
        GetComponent<AudioSource>().PlayOneShot(hover);
    }
    public void ButtonPress()
    {
        GetComponent<AudioSource>().PlayOneShot(press);
    }
    public void ButtonReject()
    {
        GetComponent<AudioSource>().PlayOneShot(reject);
    }
    public void PlayPause()
    {
        GetComponent<AudioSource>().PlayOneShot(pause);
    }
    public void PlayUnPause()
    {
        GetComponent<AudioSource>().PlayOneShot(unPause);
    }
}
