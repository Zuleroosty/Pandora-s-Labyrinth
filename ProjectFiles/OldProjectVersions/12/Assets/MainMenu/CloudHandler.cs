using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x < -27.9f)
        {
            transform.localPosition = new Vector3(40, Random.Range(8.5f, 11.5f), -1);
        }
        else transform.position -= new Vector3(0.01f, 0, 0);
    }
}
