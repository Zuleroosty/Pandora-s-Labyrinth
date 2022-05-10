using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 velocity;
    public int health, healthMax;
    public float speed, healthRatio;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.1f;
        healthMax = 120;

        health = healthMax;

        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {

        // Reset velocity before input
        velocity = new Vector3(0, 0);

        // Update velocity after input
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) velocity.x = -speed;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) velocity.y = speed;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) velocity.x = speed;
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) velocity.y = -speed;

        // Move player Object
        transform.position += velocity;
    }

    void UpdateHealthBar()
    {
        // Cap health & update ratio for progressbar
        if (health < 0) health = 0;
        if (health > healthMax) health = healthMax;
        healthRatio = health / healthMax;
        GameObject.Find("PlayerHealthBarTop").transform.localScale = new Vector3(healthRatio, 1, 1);
    }
}
