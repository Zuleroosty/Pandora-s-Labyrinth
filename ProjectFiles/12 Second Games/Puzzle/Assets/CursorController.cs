using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Cursor.visible) Cursor.visible = false;
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 11);
    }
}
