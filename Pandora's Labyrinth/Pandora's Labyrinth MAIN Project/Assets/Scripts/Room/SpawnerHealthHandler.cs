using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHealthHandler : MonoBehaviour
{
    RoomHandler owningRoomHandler;

    private void Update()
    {
        if (owningRoomHandler.currentSeconds >= owningRoomHandler.maxSeconds && owningRoomHandler.enemyCount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
