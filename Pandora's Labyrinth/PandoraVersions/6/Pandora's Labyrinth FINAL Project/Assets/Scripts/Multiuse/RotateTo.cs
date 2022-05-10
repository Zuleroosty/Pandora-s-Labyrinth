using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTo : MonoBehaviour
{
    public GameObject target;
    public Vector3 targetPos;
    float angle;

    private void Start()
    {
        if (name == "PowerShotBar") target = GameObject.Find("ProjectileSpawn");
    }
    // Update is called once per frame
    void Update()
    {
        if (target != null) targetPos = target.transform.position;
        targetPos.x = targetPos.x - transform.position.x;
        targetPos.y = targetPos.y - transform.position.y;

        angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
