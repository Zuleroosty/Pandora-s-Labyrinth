using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZOnPlayer : MonoBehaviour
{
    public GameObject targetObject;
    public bool ignore;
    Vector3 testLocation;

    // Update is called once per frame
    void Update()
    {
        if (!ignore && GameObject.Find("PlayerSprite") != null)
        {
            if (targetObject != null) testLocation = targetObject.transform.position;
            else
            {
                testLocation = transform.position;
            }
            if (testLocation.y < GameObject.Find("PlayerSprite").transform.position.y - 1)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -1.5f); // IN FRONT OF PLAYER
                if (this.name.Contains("Pillar")) transform.GetChild(0).transform.position = new Vector3(transform.GetChild(0).transform.position.x, transform.GetChild(0).transform.position.y, -1.5f);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 0.5f); // BEHIND PLAYER
                if (this.name.Contains("Pillar")) transform.GetChild(0).transform.position = new Vector3(transform.GetChild(0).transform.position.x, transform.GetChild(0).transform.position.y, 0);
            }
        }
    }

    public void ForceZ(int zPos)
    {
        ignore = true;
        transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
    }
}
