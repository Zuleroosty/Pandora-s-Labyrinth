using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZOnPlayer : MonoBehaviour
{
    public GameObject targetObject;
    public bool ignore;
    Vector3 testLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!ignore)
        {
            if (targetObject != null) testLocation = targetObject.transform.position;
            else
            {
                testLocation = transform.position;
            }
            if (testLocation.y < GameObject.Find("PlayerSprite").transform.position.y - 1)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -1.5f);
            }
            else transform.position = new Vector3(transform.position.x, transform.position.y, 0.5f);
        }
    }

    public void ForceZ(int zPos)
    {
        ignore = true;
        transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
    }
}
