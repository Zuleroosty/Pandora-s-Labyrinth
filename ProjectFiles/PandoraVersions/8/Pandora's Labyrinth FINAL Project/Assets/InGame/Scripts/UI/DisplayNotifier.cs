using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayNotifier : MonoBehaviour
{
    public string displayText;
    public Vector3 spawnPos;
    public float displayTime, boxPosX, boxPosY;
    public bool fadeIn, fadeOut;
    GameObject checkObject;
    public int childID, childMax, thisID;

    private void Start()
    {
        transform.parent = GameObject.Find("---Notifications---").transform;
        childMax = transform.parent.childCount;
        fadeIn = true;
        boxPosX = 12;
        thisID = transform.parent.childCount - 1;
        boxPosY = ((thisID * 1.2f) - 1) * -1;
        transform.localPosition = new Vector3(22, boxPosY, 5);
        spawnPos = transform.localPosition;
    }

    void Update()
    {
        if (transform.parent.name.Contains("KillBox"))
        {
            if (transform.localPosition.x < spawnPos.x) transform.localPosition += new Vector3(0.75f, 0, 0);
            else Destroy(gameObject);
        }
        else
        {
            boxPosY = ((thisID * 1.2f) - 1) * -1;
            if (boxPosY > transform.localPosition.y) transform.localPosition += new Vector3(0, 0.1f, 0);
            if (fadeIn)
            {
                if (transform.localPosition.x > boxPosX) transform.localPosition -= new Vector3(0.5f, 0, 0);
                else
                {
                    if (displayTime < 120) displayTime++;
                    if (displayTime >= 120 && thisID == 0)
                    {
                        fadeIn = false;
                        fadeOut = true;
                    }
                }
            }
            if (fadeOut)
            {
                if (transform.localPosition.x < spawnPos.x) transform.localPosition += new Vector3(0.5f, 0, 0);
                else Destroy(gameObject);
            }

            if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Reset)
            {
                Destroy(gameObject);
            }
        }
    }
}
