using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    float smoothFactor; // Higher = Faster
    Vector3 requiredSize, currentSize;

    private void Start()
    {
        smoothFactor = 4;
        GetComponent<Camera>().orthographicSize = 9.5f;
        requiredSize.x = 7f;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null) target = GameObject.Find("----PlayerObjectParent----").transform;
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            currentSize.x = GetComponent<Camera>().orthographicSize;
            if (target.GetComponent<PlayerController>().inCombat) requiredSize.x = 6.5f;
            else requiredSize.x = 7f;
            currentSize = Vector3.Lerp(currentSize, requiredSize, smoothFactor * Time.deltaTime);
            GetComponent<Camera>().orthographicSize = currentSize.x;
        }
        Follow();
    }

    void Follow()
    {
        Vector3 smoothPosition = Vector3.Lerp(transform.position, target.position, smoothFactor * Time.fixedDeltaTime);
        transform.position = new Vector3(smoothPosition.x, smoothPosition.y, transform.position.z);
    }
}
