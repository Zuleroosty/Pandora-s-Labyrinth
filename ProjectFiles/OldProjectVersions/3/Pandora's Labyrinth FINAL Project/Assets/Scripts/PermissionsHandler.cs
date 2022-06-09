using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermissionsHandler : MonoBehaviour
{
    public bool canSpawn, canMove;

    private void Start()
    {
        canMove = true;
        canSpawn = true;
    }
}
