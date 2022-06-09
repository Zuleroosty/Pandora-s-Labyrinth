using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGen : MonoBehaviour
{
    public GameObject newContents, combatContents, emptyContents, pandoraContents;
    bool hasGeneratedRoom;
    int randNum;

    // Update is called once per frame
    void Update()
    {
        if (!hasGeneratedRoom && GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Start)
        {
            if (!transform.parent.name.Contains("Spawn"))
            {
                if (transform.parent.name.Contains("Pandora"))
                {
                    //spawn pandora
                    newContents = Instantiate(pandoraContents, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    newContents.transform.parent = transform;
                    hasGeneratedRoom = true;
                }
                else
                {
                    randNum = Random.Range(0, 2);
                    if (randNum == 0)
                    {
                        randNum = Random.Range(0, 101);
                        if (randNum <= 60)
                        {
                            //spawn empty
                            newContents = Instantiate(emptyContents, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                            newContents.transform.parent = transform;
                            hasGeneratedRoom = true;
                        }
                        else
                        {
                            randNum -= 60;
                            if (randNum <= 40)
                            {
                                //spawn combat
                                newContents = Instantiate(combatContents, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                                newContents.transform.parent = transform;
                                hasGeneratedRoom = true;
                            }
                        }
                    }
                    if (randNum == 1)
                    {
                        randNum = Random.Range(0, 101);
                        if (randNum <= 60)
                        {
                            //spawn combat
                            newContents = Instantiate(combatContents, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                            newContents.transform.parent = transform;
                            hasGeneratedRoom = true;
                        }
                        else
                        {
                            randNum -= 60;
                            if (randNum <= 40)
                            {
                                //spawn empty
                                newContents = Instantiate(emptyContents, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                                newContents.transform.parent = transform;
                                hasGeneratedRoom = true;
                            }
                        }
                    }
                }
            }
            else hasGeneratedRoom = true;
        }
    }
}
