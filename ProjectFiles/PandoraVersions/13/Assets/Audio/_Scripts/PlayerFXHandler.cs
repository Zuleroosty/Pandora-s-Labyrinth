using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFXHandler : MonoBehaviour
{
    public AudioClip footStep1, footStep2, takeDamage, throwSpear, sprintBoost, drinkPotion, itemPickup, goldPickup, pandoraPickup, xpPickup, levelUp;
    public GameObject fxPlayer;
    public bool isWalking, flip;
    public int walkTimer, maxWalkTimer, delayTimer, maxDelay;

    private void Update()
    {
        if (GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().velocity != new Vector3(0, 0, 0))
        {
            if (delayTimer < maxDelay) delayTimer++;
            else
            {
                if (walkTimer < 15) walkTimer++;
                if (walkTimer >= 15)
                {
                    GetComponent<AudioSource>().volume = GameObject.Find(">GameManager<").GetComponent<AudioHandler>().effectsMax * 0.25f;
                    GetComponent<AudioSource>().pitch = 1f + Random.Range(0.01f, 0.03f);
                    walkTimer = 0;
                    flip = !flip;
                    if (flip)
                    {
                        GetComponent<AudioSource>().PlayOneShot(footStep1);
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(footStep2);
                        maxDelay = Random.Range(2, 4);
                        delayTimer = 0;
                    }
                }
            }
        }
        if (!fxPlayer.GetComponent<AudioSource>().isPlaying) fxPlayer.GetComponent<AudioSource>().volume = GameObject.Find(">GameManager<").GetComponent<AudioHandler>().effectsMax;
    }
    public void PlayTakeDamage()
    {
        fxPlayer.GetComponent<AudioSource>().volume = GameObject.Find(">GameManager<").GetComponent<AudioHandler>().effectsMax * 0.55f;
        fxPlayer.GetComponent<AudioSource>().pitch = 1f + Random.Range(-0.1f, 0.3f);
        fxPlayer.GetComponent<AudioSource>().PlayOneShot(takeDamage);
    }
    public void PlayThrowSpear()
    {
        fxPlayer.GetComponent<AudioSource>().volume = GameObject.Find(">GameManager<").GetComponent<AudioHandler>().effectsMax * 0.45f;
        fxPlayer.GetComponent<AudioSource>().pitch = 1f + Random.Range(-0.1f, 0.3f);
        fxPlayer.GetComponent<AudioSource>().PlayOneShot(throwSpear);
    }
    public void PlaySprintBoost()
    {
        fxPlayer.GetComponent<AudioSource>().volume = GameObject.Find(">GameManager<").GetComponent<AudioHandler>().effectsMax * 0.55f;
        fxPlayer.GetComponent<AudioSource>().pitch = 1f + Random.Range(-0.1f, 0.3f);
        fxPlayer.GetComponent<AudioSource>().PlayOneShot(sprintBoost);
    }
    public void PlayDrinkPotion()
    {
        fxPlayer.GetComponent<AudioSource>().volume = GameObject.Find(">GameManager<").GetComponent<AudioHandler>().effectsMax * 0.55f;
        fxPlayer.GetComponent<AudioSource>().pitch = 1f + Random.Range(-0.1f, 0.3f);
        fxPlayer.GetComponent<AudioSource>().PlayOneShot(drinkPotion);
    }
    public void PlayItemPickup()
    {
        fxPlayer.GetComponent<AudioSource>().volume = GameObject.Find(">GameManager<").GetComponent<AudioHandler>().effectsMax * 0.65f;
        fxPlayer.GetComponent<AudioSource>().pitch = 1f + Random.Range(-0.1f, 0.3f);
        fxPlayer.GetComponent<AudioSource>().PlayOneShot(itemPickup);
    }
    public void PlayGoldPickup()
    {
        fxPlayer.GetComponent<AudioSource>().volume = GameObject.Find(">GameManager<").GetComponent<AudioHandler>().effectsMax * 0.65f;
        fxPlayer.GetComponent<AudioSource>().pitch = 1f + Random.Range(-0.1f, 0.3f);
        fxPlayer.GetComponent<AudioSource>().PlayOneShot(goldPickup);
    }
    public void PlayPandoraPickup()
    {
        fxPlayer.GetComponent<AudioSource>().volume = GameObject.Find(">GameManager<").GetComponent<AudioHandler>().effectsMax * 0.65f;
        fxPlayer.GetComponent<AudioSource>().pitch = 1f;
        fxPlayer.GetComponent<AudioSource>().PlayOneShot(pandoraPickup);
    }
    public void PlayXPPickup()
    {
        fxPlayer.GetComponent<AudioSource>().volume = GameObject.Find(">GameManager<").GetComponent<AudioHandler>().effectsMax * 0.35f;
        fxPlayer.GetComponent<AudioSource>().pitch = 1f + Random.Range(-0.01f, 0.03f);
        fxPlayer.GetComponent<AudioSource>().PlayOneShot(xpPickup);
    }
    public void PlayLevelUp()
    {
        fxPlayer.GetComponent<AudioSource>().volume = GameObject.Find(">GameManager<").GetComponent<AudioHandler>().effectsMax * 0.55f;
        fxPlayer.GetComponent<AudioSource>().pitch = 1f;
        fxPlayer.GetComponent<AudioSource>().PlayOneShot(levelUp);
    }
}