using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 1);
        GetComponent<SpriteRenderer>().sortingOrder = 10;
        transform.parent = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.visible) Cursor.visible = false;
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
    }
}
