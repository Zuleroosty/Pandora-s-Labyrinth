using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHandler : MonoBehaviour
{
    public int currentNumber;

    private void Update()
    {
        transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = currentNumber.ToString();
    }
}
