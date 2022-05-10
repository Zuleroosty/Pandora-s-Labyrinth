﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTo : MonoBehaviour
{
    public GameObject target;
    public Vector3 targetPos;
    float angle;

    private void Start()
    {
        if (target == null)
        {
            if (name == "PowerShotBar") target = GameObject.Find("ProjectileSpawn");
            else if (name == "BossSpawnArm") target = GameObject.Find("----PlayerObjectParent----");
            else target = GameObject.Find("DisplayCursor");
        }
        targetPos = target.transform.position;
        targetPos.x = targetPos.x - transform.position.x;
        targetPos.y = targetPos.y - transform.position.y;

        angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (!name.Contains("Projectile"))
            {
                if (target != null) targetPos = target.transform.position;
                targetPos.x = targetPos.x - transform.position.x;
                targetPos.y = targetPos.y - transform.position.y;

                angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
                if (!name.Contains("AimingArm")) transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
                else transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));
            }
        }
    }
}
