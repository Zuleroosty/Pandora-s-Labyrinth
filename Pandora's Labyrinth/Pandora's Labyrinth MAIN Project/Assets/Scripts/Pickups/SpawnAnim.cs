using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnim : MonoBehaviour
{
    public float maxX, minY, maxY, moveSpeed, speedFactor;
    public bool dirDown, xDir;
    public Vector3 spawnPos;
    
    // Start is called before the first frame update
    void Start()
    {
        minY = Random.Range(0.2f, 0.5f);
        spawnPos = transform.position;
        spawnPos.y -= 0.25f;
        speedFactor = 0.2f;

        if (Random.Range(0, 2) >= 1) maxX = Random.Range(0.25f, 1f);
        else maxX = Random.Range(-0.25f, -1f);
        if (maxX > 0) maxY = ((maxX / 1.5f) * 0.5f) + 1;
        if (maxX < 0) maxY = (((maxX * -1) / 1.5f) * 0.5f) + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            moveSpeed = ((transform.position.x / (spawnPos.x + maxX)) * speedFactor);
            if (this.name.Contains("XP"))
            {
                if (!transform.GetChild(0).GetComponent<XPPickup>().hasCollected)
                {
                    if (transform.position.y > spawnPos.y - minY)
                    {
                        if (maxX > 0 && transform.position.x < spawnPos.x + maxX) //Right
                        {
                            transform.position += new Vector3(moveSpeed / 2, 0, 0);
                        }
                        if (maxX < 0 && transform.position.x > spawnPos.x + maxX) //Left
                        {
                            transform.position -= new Vector3(moveSpeed / 2, 0, 0);
                        }
                        if (dirDown) transform.position -= new Vector3(0, moveSpeed, 0);
                        else transform.position += new Vector3(0, moveSpeed * 1.5f, 0);
                    }
                    if (transform.position.y > spawnPos.y + maxY) dirDown = true;
                }
            }
            else
            {
                if (transform.position.y > spawnPos.y - minY)
                {
                    if (maxX > 0 && transform.position.x < spawnPos.x + maxX) //Right
                    {
                        transform.position += new Vector3(moveSpeed / 2, 0, 0);
                    }
                    if (maxX < 0 && transform.position.x > spawnPos.x + maxX) //Left
                    {
                        transform.position -= new Vector3(moveSpeed / 2, 0, 0);
                    }
                    if (dirDown) transform.position -= new Vector3(0, moveSpeed, 0);
                    else transform.position += new Vector3(0, moveSpeed * 1.5f, 0);
                }
                if (transform.position.y > spawnPos.y + maxY) dirDown = true;
            }
        }
    }
}
