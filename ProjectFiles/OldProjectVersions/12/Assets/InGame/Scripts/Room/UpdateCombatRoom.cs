using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCombatRoom : MonoBehaviour
{
    public GameObject spawner1, spawner2, spawner3, spawner4;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent.parent.GetComponent<RoomHandler>().isCombatRoom = true;
    }
}
