using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMaster : MonoBehaviour
{
    public GameObject target, nextLink, prefab, pathParent, objectParent, playerRadius;
    public int totalLinks, destroyTimer, randNum;
    public float speed, spawnRadius;
    Vector3 velocity, spawnPos, targetPos;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

        if (objectParent == null) objectParent = transform.parent.parent.gameObject;
        speed = objectParent.GetComponent<EnemyAI>().speed;
        spawnRadius = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (target == null) target = GameObject.Find("PCollision");
            if (pathParent == null)
            {
                pathParent = new GameObject("Path: " + this.name);
                pathParent.transform.parent = GameObject.Find("----PathLinks----").transform;
            }
            else
            {
                totalLinks = pathParent.transform.childCount;
                if (nextLink == null)
                {
                    spawnPos = new Vector3(0, 0, 0);
                    if (target.transform.position.x > transform.position.x) spawnPos.x = spawnRadius;
                    if (target.transform.position.x < transform.position.x) spawnPos.x = -spawnRadius;
                    if (target.transform.position.y > transform.position.y) spawnPos.y = spawnRadius;
                    if (target.transform.position.y < transform.position.y) spawnPos.y = -spawnRadius;

                    if (prefab != null)
                    {
                        nextLink = Instantiate(prefab, transform.position + spawnPos, Quaternion.identity);
                        nextLink.GetComponent<PathHandler>().prevLink = this.gameObject;
                        nextLink.GetComponent<PathHandler>().owner = this.gameObject;
                    }
                }
                else
                {
                    targetPos.x = nextLink.transform.position.x;
                    targetPos.y = nextLink.transform.position.y + (objectParent.transform.position.y - transform.position.y);
                    targetPos.z = objectParent.transform.position.z;
                    if (!target.GetComponent<SpriteRenderer>().bounds.Intersects(playerRadius.GetComponent<SpriteRenderer>().bounds))
                    {
                        objectParent.transform.position = Vector3.Lerp(objectParent.transform.position, targetPos, objectParent.GetComponent<EnemyAI>().speed * Time.deltaTime);
                    }
                    if (destroyTimer < randNum) destroyTimer++;
                    if (destroyTimer >= randNum)
                    {
                        destroyTimer = 0;
                        randNum = Random.Range(30, 61);
                        Destroy(pathParent.gameObject);

                    }
                }
            }
        }
    }
    private void OnDestroy()
    {
        Destroy(pathParent.gameObject);
    }
}
