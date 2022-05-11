using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildTextUpdater : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMesh>().text = transform.parent.parent.parent.GetComponent<DisplayNotifier>().displayText;
    }
}
