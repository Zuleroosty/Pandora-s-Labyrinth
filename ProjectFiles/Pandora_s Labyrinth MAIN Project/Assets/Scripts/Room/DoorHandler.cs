using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public bool isLocked, hasPlayedLockedFX, hasPlayedUnlockFX;
    public AudioClip lockFX, unlockFX;
    Color thisColour;

    // Start is called before the first frame update
    void Start()
    {
        thisColour = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (this.name.Contains("Pandora"))
            {
                if (GameObject.Find(">GameManager<").GetComponent<GameManager>().hasMinotaurSpawned) isLocked = true;
                else isLocked = false;
            }
            else if (this.name.Contains("Exit"))
            {
                if (GameObject.Find(">GameManager<").GetComponent<GameManager>().hasMinotaurSpawned && GameObject.Find("BossEnemy(Clone)") == null) isLocked = false;
                else isLocked = true;
            }
            else if (!transform.parent.parent.name.Contains("Spawn")) isLocked = transform.parent.parent.GetComponent<RoomHandler>().isRoomLocked;


            if (isLocked)
            {
                transform.GetChild(0).GetComponent<CollisionManager>().disableCollision = false;
                if (thisColour.a < 1) thisColour.a += 0.25f;
                if (!hasPlayedLockedFX)
                {
                    GameObject.Find(">GameManager<").GetComponent<GameManager>().SpawnNew3DFX(lockFX, transform.position);
                    hasPlayedLockedFX = true;
                    hasPlayedUnlockFX = false;
                }
            }
            else
            {
                transform.GetChild(0).GetComponent<CollisionManager>().disableCollision = true;
                if (thisColour.a > 0) thisColour.a -= 0.05f;
                if (!hasPlayedUnlockFX)
                {
                    GameObject.Find(">GameManager<").GetComponent<GameManager>().SpawnNew3DFX(unlockFX, transform.position);
                    hasPlayedUnlockFX = true;
                    hasPlayedLockedFX = false;
                }
            }

            GetComponent<SpriteRenderer>().color = thisColour;
        }
    }
}
