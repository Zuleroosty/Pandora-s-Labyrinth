using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearUpgrade : MonoBehaviour
{
    public Sprite spearLvl1, spearLvl2, spearLvl3, spearLvl4;
    bool hasCollected, moveDir;
    PlayerController playerScript;
    GameObject spearObject;
    Vector3 spawnPoint, spearSpawnPoint, minY, maxY;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>();
        spearObject = transform.parent.gameObject;
        spawnPoint = transform.position;
        spearSpawnPoint = spearObject.transform.localPosition;

        minY = spearSpawnPoint;
        minY.y = spearSpawnPoint.y - 0.02f;
        maxY = spearSpawnPoint;
        maxY.y = spearSpawnPoint.y + 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = spawnPoint;
        if (!hasCollected)
        {
            if (GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds))
            {
                hasCollected = true;
                GameObject.Find(">GameManager<").GetComponent<GameManager>().NewNotification("Spear++");
                switch (playerScript.currentSpear)
                {
                    case PlayerController.spear.lvl0:
                        playerScript.currentSpear = PlayerController.spear.lvl1;
                        break;
                    case PlayerController.spear.lvl1:
                        playerScript.currentSpear = PlayerController.spear.lvl2;
                        break;
                    case PlayerController.spear.lvl2:
                        playerScript.currentSpear = PlayerController.spear.lvl3;
                        break;
                    case PlayerController.spear.lvl3:
                        playerScript.currentSpear = PlayerController.spear.lvl4;
                        break;
                }
            }
            switch (playerScript.currentSpear)
            {
                case PlayerController.spear.lvl0:
                    transform.parent.GetComponent<SpriteRenderer>().sprite = spearLvl1;
                    break;
                case PlayerController.spear.lvl1:
                    transform.parent.GetComponent<SpriteRenderer>().sprite = spearLvl2;
                    break;
                case PlayerController.spear.lvl2:
                    transform.parent.GetComponent<SpriteRenderer>().sprite = spearLvl3;
                    break;
                case PlayerController.spear.lvl3:
                    transform.parent.GetComponent<SpriteRenderer>().sprite = spearLvl4;
                    break;
            }
            AnimateSpear();
        }
        else
        {
            Destroy(spearObject.gameObject);
        }
    }
    void AnimateSpear()
    {
        if (moveDir)
        {
            if (spearObject.transform.localPosition.y < maxY.y - 0.008f)
            {
                spearObject.transform.localPosition = Vector3.Lerp(spearObject.transform.localPosition, maxY, 2f * Time.deltaTime);
            }
            else moveDir = false;
        }
        else
        {
            if (spearObject.transform.localPosition.y > minY.y + 0.008f)
            {
                spearObject.transform.localPosition = Vector3.Lerp(spearObject.transform.localPosition, minY, 2f * Time.deltaTime);
            }
            else moveDir = true;
        }
    }
}
