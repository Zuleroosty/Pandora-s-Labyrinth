using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTimerDisplayHandler : MonoBehaviour
{
    public GameObject textObject, mainBar, backBar;
    Color textColour, mainColour, backBarColour, thisColour;
    RoomHandler currentRoom;
    float xScale;
    bool minotaurSpawned;

    // Start is called before the first frame update
    void Start()
    {
        textColour = textObject.GetComponent<TextMesh>().color;
        mainColour = mainBar.GetComponent<SpriteRenderer>().color;
        backBarColour = backBar.GetComponent<SpriteRenderer>().color;
        thisColour = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        // STOP DISPLAYING IF MINOTAUR HAS BEEN SPAWNED
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Reset) minotaurSpawned = false;
        else if (GameObject.Find("BossEnemy(Clone)") != null) minotaurSpawned = true;

        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame && ! minotaurSpawned)
        {
            currentRoom = GameObject.Find(">GameManager<").GetComponent<GameManager>().currentRoomParent.GetComponent<RoomHandler>();
            if (currentRoom != null)
            {
                if (GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().inCombat)
                {
                    xScale = 1 - (currentRoom.currentSeconds / currentRoom.maxSeconds);
                    print("RoomTimer: " + xScale);
                    if (xScale < 0) xScale = 0;
                    mainBar.transform.localScale = new Vector3(xScale, mainBar.transform.localScale.y, mainBar.transform.localScale.z);

                    if (thisColour.a < 1)
                    {
                        textColour.a += 0.05f;
                        mainColour.a += 0.05f;
                        backBarColour.a += 0.05f;
                        thisColour.a += 0.05f;
                    }
                }
                else if (currentRoom.enemyCount <= 0)
                {
                    if (thisColour.a > 0)
                    {
                        textColour.a -= 0.05f;
                        mainColour.a -= 0.05f;
                        backBarColour.a -= 0.05f;
                        thisColour.a -= 0.05f;
                    }
                }
            }
        }
        else
        {
            textColour.a = 0;
            mainColour.a = 0;
            backBarColour.a = 0;
            thisColour.a = 0;
        }

        textObject.GetComponent<TextMesh>().color = textColour;
        mainBar.GetComponent<SpriteRenderer>().color = mainColour;
        backBar.GetComponent<SpriteRenderer>().color = backBarColour;
        GetComponent<SpriteRenderer>().color = thisColour;
    }
}
