using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public bool shakeCamera, pulseCameraIn, pulseCameraOut;
    float smoothFactor; // Higher = Faster
    Vector3 requiredSize, currentSize, shakeOffset;
    int effectTimer, shakeTimer;

    private void Start()
    {
        GetComponent<Camera>().orthographicSize = 9.5f;
        requiredSize.x = 7f;

        target = GameObject.Find(">>----MainMenu----<<").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (shakeCamera)
            {
                if (effectTimer < 15)
                {
                    effectTimer++;
                    if (shakeTimer < 3) shakeTimer++;
                    else
                    {
                        shakeTimer = 0;

                        shakeOffset.x = Random.Range(-2f, 3f);
                        shakeOffset.y = Random.Range(-2f, 3f);
                        // REPEATED FOR MAXIMUM RANDOM ABILITY
                        shakeOffset.x = Random.Range(-2f, 3f);
                        shakeOffset.y = Random.Range(-2f, 3f);
                    }

                    if (target.GetComponent<PlayerController>().inCombat) requiredSize.x = 6.5f;
                    else requiredSize.x = 7f;

                    currentSize = Vector3.Lerp(currentSize, requiredSize, smoothFactor * Time.deltaTime);
                    GetComponent<Camera>().orthographicSize = currentSize.x;
                }
                else
                {
                    effectTimer = 0;
                    shakeCamera = false;
                }
            }
            else if (pulseCameraOut)
            {
                shakeOffset = new Vector3(0, 0, 0);
                if (effectTimer < 25)
                {
                    effectTimer++;
                    if (target.GetComponent<PlayerController>().inCombat) requiredSize.x = 7f;
                    else requiredSize.x = 7.5f;
                    currentSize = Vector3.Lerp(currentSize, requiredSize, (smoothFactor / 3) * Time.deltaTime);
                    GetComponent<Camera>().orthographicSize = currentSize.x;
                    Follow();
                }
                else
                {
                    effectTimer = 0;
                    pulseCameraOut = false;
                }
            }
            else if (pulseCameraIn)
            {
                shakeOffset = new Vector3(0, 0, 0);
                if (effectTimer < 8)
                {
                    effectTimer++;
                    if (target.GetComponent<PlayerController>().inCombat) requiredSize.x = 6.25f;
                    else requiredSize.x = 6.75f;
                    currentSize = Vector3.Lerp(currentSize, requiredSize, (smoothFactor / 2) * Time.deltaTime);
                    GetComponent<Camera>().orthographicSize = currentSize.x;
                    Follow();
                }
                else
                {
                    effectTimer = 0;
                    pulseCameraIn = false;
                }
            }
            else
            {
                shakeOffset = new Vector3(0, 0, 0);
                smoothFactor = 4;
                if (target.name.Contains(">>----MainMenu----<<")) target = GameObject.Find("----PlayerObjectParent----").transform;
                currentSize.x = GetComponent<Camera>().orthographicSize;
                if (target.GetComponent<PlayerController>().inCombat) requiredSize.x = 6.5f;
                else requiredSize.x = 7f;
                currentSize = Vector3.Lerp(currentSize, requiredSize, smoothFactor * Time.deltaTime);
                GetComponent<Camera>().orthographicSize = currentSize.x;
            }
            Follow();

        }
        else if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.GenLevel)
        {
            smoothFactor = 10;
            if (GameObject.Find(">GameManager<").GetComponent<GameManager>().spawnNextLvl) target = GameObject.Find("----PlayerObjectParent----").transform;
            else target = GameObject.Find(">>----MainMenu----<<").transform;
            currentSize.x = GetComponent<Camera>().orthographicSize;
            requiredSize.x = 9.5f;
            currentSize = Vector3.Lerp(currentSize, requiredSize, smoothFactor * Time.deltaTime);
            GetComponent<Camera>().orthographicSize = currentSize.x;
            Follow();
        }
        else if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Start)
        {
            transform.position = new Vector3(0, 0, -10);
        }
    }

    void Follow()
    {
        if (shakeCamera) smoothFactor = 2;
        else smoothFactor = 4;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, target.position + shakeOffset, smoothFactor * Time.fixedDeltaTime);
        transform.position = new Vector3(smoothPosition.x, smoothPosition.y, transform.position.z);
    }
}
