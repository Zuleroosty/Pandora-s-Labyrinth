using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevControl : MonoBehaviour
{
    Vector3 tpLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            if (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.P))
            {
                print("1");
                if (!GameObject.Find(">GameManager<").GetComponent<GameManager>().currentRoomParent.name.Contains("PandoraBoxRoom(Clone)"))
                {
                    print("2");
                    tpLocation = GameObject.Find("PandoraBoxRoom(Clone)").transform.position;
                    tpLocation.y -= 3;
                    transform.position = tpLocation;
                }
            }
        }
    }
}
