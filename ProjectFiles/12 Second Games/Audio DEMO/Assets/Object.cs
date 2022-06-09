using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Object : MonoBehaviour
{
    public AudioClip fx1, fx2;
    public AudioSource cameraAudioSource, thisAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        cameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>(); // ATTACHED TO CAMERA OBJECT (MUSIC)
        thisAudioSource = GetComponent<AudioSource>(); // ATTACHED TO THIS OBJECT (FX)
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // ----QUICK PLAY EG HIT MARKER SOUND EFFECT - CANNOT LOOP
        {
            thisAudioSource.pitch = Random.Range(0f, 1.51f); // TRY TO KEEP CLOSE TO A VALUE OF 1 TO RETAIN AUDIO QUALITY
            thisAudioSource.PlayOneShot(fx1); // ADD AUDIO CLIP INSIDE BRACKETS - PLAYS CLIP ONCE - PLAYS OVER CURRENT AUDIO IF AUDIO SOURCE PLAYING
            // ADJUSTMENTS NEED TO BE BEFORE AUDIO IS PLAYED
        }


        if (Input.GetKeyDown(KeyCode.Alpha2)) // ----AUDIO TRACK CHANGE EG BATTLE MUSIC -> CALMING MUSIC
        {
            if (thisAudioSource.isPlaying) thisAudioSource.Stop(); // STOPS IF AUDIO ALREADY PLAYING A CLIP
            thisAudioSource.clip = fx2; // ADDS CLIP TO AUDIO SOURCE
            thisAudioSource.Play(); // PLAYS CLIP
        }


        if (Input.GetKeyDown(KeyCode.Space)) // ON OFF CAMERA AUDIO SOURCE - CLIP PREVIOUSLY ADDED IN HIERACHY
        {
            if (cameraAudioSource.isPlaying)
            {
                cameraAudioSource.Stop();
            }
            else cameraAudioSource.Play();
        }

        // ADJUSTMENTS
        if (Input.GetKeyDown(KeyCode.Minus)) // VOLUME DOWN
        {
            cameraAudioSource.volume -= 0.025f; // FLOAT RANGE 0-1
        }
        if (Input.GetKeyDown(KeyCode.Plus)) // VOLUME UP
        {
            cameraAudioSource.volume += 0.025f;
        }

        if (Input.GetKeyDown(KeyCode.L)) // ON OFF LOOPING OF TRACK ADDED TO AUDIO SOURCE
        {
            thisAudioSource.loop = !thisAudioSource.loop; // THIS SWITCH ONLY POSSIBLE WITH BOOLEANS
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift)) // PITCH UP
        {
            cameraAudioSource.pitch += 0.025f;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl)) // PITCH DOWN
        {
            cameraAudioSource.pitch -= 0.025f;
        }
        if (Input.GetKeyDown(KeyCode.Z)) // RANDOMISE PITCH
        {
            cameraAudioSource.pitch = Random.Range(0.9f, 1.02f); // TRY TO KEEP CLOSE TO A VALUE OF 1 TO RETAIN AUDIO QUALITY
        }

        if (Input.GetKeyDown(KeyCode.B)) // SWITCH 2D - 3D
        {
            if (thisAudioSource.spatialBlend < 0.5f) thisAudioSource.spatialBlend = 0.8f; // HIGHER IS 3D
            else thisAudioSource.spatialBlend = 0; // LOWER IS 2D
            thisAudioSource.maxDistance = 40; // SET DISTANCE VOLUME REACHED 0 (3D ONLY)
        }
    }
}
