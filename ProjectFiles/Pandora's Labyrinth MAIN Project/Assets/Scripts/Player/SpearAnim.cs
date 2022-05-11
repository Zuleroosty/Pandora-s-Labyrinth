using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearAnim : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x > -0.02) transform.localPosition -= new Vector3(0.05f, 0, 0);
    }
}
