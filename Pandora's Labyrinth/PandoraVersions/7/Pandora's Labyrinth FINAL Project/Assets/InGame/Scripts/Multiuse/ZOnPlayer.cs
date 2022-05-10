using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZOnPlayer : MonoBehaviour
{
    public bool ignore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!ignore)
        {
            if (transform.position.y < GameObject.Find("PlayerSprite").transform.position.y - 1)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, GameObject.Find("PlayerSprite").transform.position.z + 0.5f);
            }
            else transform.position = new Vector3(transform.position.x, transform.position.y, GameObject.Find("PlayerSprite").transform.position.z - 0.5f);
        }
    }

    public void ForceZ(int zPos)
    {
        ignore = true;
        transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
    }
}
