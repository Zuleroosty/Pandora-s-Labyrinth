using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearUpgrade : MonoBehaviour
{
    public GameObject thisSprite;
    public Sprite spearLvl1, spearLvl2, spearLvl3, spearLvl4;
    bool hasCollected, moveDir;
    PlayerController playerScript;
    Vector3 spawnPoint, spearSpawnPoint, minY, maxY;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>();
        spawnPoint = transform.position;
        spearSpawnPoint = thisSprite.transform.localPosition;

        minY = spearSpawnPoint;
        minY.y = spearSpawnPoint.y - 0.02f;
        maxY = spearSpawnPoint;
        maxY.y = spearSpawnPoint.y + 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame || GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Pause)
        {
            transform.position = spawnPoint;
            if (!hasCollected)
            {
                if (GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds))
                {
                    hasCollected = true;
                    playerScript.gameObject.GetComponent<PlayerFXHandler>().PlaySpearPickup();
                    switch (playerScript.currentSpear)
                    {
                        case PlayerController.spear.lvl0:
                            playerScript.currentSpear = PlayerController.spear.lvl1;
                            GameObject.Find(">GameManager<").GetComponent<GameManager>().NewNotification("Spear Lvl 2", 4);
                            break;
                        case PlayerController.spear.lvl1:
                            playerScript.currentSpear = PlayerController.spear.lvl2;
                            GameObject.Find(">GameManager<").GetComponent<GameManager>().NewNotification("Spear Lvl 3", 4);
                            break;
                        case PlayerController.spear.lvl2:
                            playerScript.currentSpear = PlayerController.spear.lvl3;
                            GameObject.Find(">GameManager<").GetComponent<GameManager>().NewNotification("Spear Lvl 4", 4);
                            break;
                        case PlayerController.spear.lvl3:
                            playerScript.currentSpear = PlayerController.spear.lvl4;
                            GameObject.Find(">GameManager<").GetComponent<GameManager>().NewNotification("Spear Lvl 5", 4);
                            break;
                    }
                }
                switch (playerScript.currentSpear)
                {
                    case PlayerController.spear.lvl0:
                        thisSprite.GetComponent<SpriteRenderer>().sprite = spearLvl1;
                        break;
                    case PlayerController.spear.lvl1:
                        thisSprite.GetComponent<SpriteRenderer>().sprite = spearLvl2;
                        break;
                    case PlayerController.spear.lvl2:
                        thisSprite.GetComponent<SpriteRenderer>().sprite = spearLvl3;
                        break;
                    case PlayerController.spear.lvl3:
                        thisSprite.GetComponent<SpriteRenderer>().sprite = spearLvl4;
                        break;
                }
                AnimateSpear();
            }
            else
            {
                Destroy(thisSprite.gameObject);
            }
        }
        else Destroy(gameObject);
    }
    void AnimateSpear()
    {
        if (moveDir)
        {
            if (thisSprite.transform.localPosition.y < maxY.y - 0.008f)
            {
                thisSprite.transform.localPosition = Vector3.Lerp(thisSprite.transform.localPosition, maxY, 2f * Time.deltaTime);
            }
            else moveDir = false;
        }
        else
        {
            if (thisSprite.transform.localPosition.y > minY.y + 0.008f)
            {
                thisSprite.transform.localPosition = Vector3.Lerp(thisSprite.transform.localPosition, minY, 2f * Time.deltaTime);
            }
            else moveDir = true;
        }
    }
}
