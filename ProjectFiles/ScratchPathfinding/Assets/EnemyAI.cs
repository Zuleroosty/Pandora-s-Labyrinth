using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject playerObject, nextObject, startObject, newPathObject;
    public Vector3 velocity;
    float xSpeed, ySpeed, speed;
    public bool pathStartUpdated;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("B");

        speed = 0.05f;

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
        SpawnNewPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (nextObject != null)
        {
            // MOVE TO OBJECT IF VALID
            xSpeed = 0;
            ySpeed = 0;

            if (nextObject.transform.position.x > transform.position.x + 0.03f) xSpeed = speed;
            else if (nextObject.transform.position.x < transform.position.x - 0.03f) xSpeed = -speed;

            if (nextObject.transform.position.y > transform.position.y + 0.03f) ySpeed = speed;
            else if (nextObject.transform.position.y < transform.position.y - 0.03f) ySpeed = -speed;

            velocity = new Vector3(xSpeed, ySpeed, 0);

            transform.position += velocity;
            if (GetComponent<SpriteRenderer>().bounds.Intersects(nextObject.GetComponent<SpriteRenderer>().bounds))
            {
                nextObject = nextObject.GetComponent<CreatePath>().nextObject;
            }
        }
        else nextObject = startObject;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // FOLLOW PATH
        }
    }
    public void SpawnNewPath()
    {
        Destroy(startObject);
        pathStartUpdated = false;
        startObject = Instantiate(newPathObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        startObject.GetComponent<CreatePath>().prevObject = this.gameObject;
    }
}
