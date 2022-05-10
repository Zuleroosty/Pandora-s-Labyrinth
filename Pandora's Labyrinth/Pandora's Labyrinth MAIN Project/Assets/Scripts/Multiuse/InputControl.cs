using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    public int numberToDisplay;
    public TextMesh displayText;
    public int numberIncrease = 1;
    public int numberMax = 10;
    public int numberMin = -10;
    int timer;
    int timerMax = 60;
    bool timerActive;
    bool numberReset;

    // Start is called before the first frame update
    void Start()
    {
        numberToDisplay = 0;
        numberReset = false;
        timerActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive == true)
        {
            if (timer >= 0)
            {
                timer -= 1;
            }

            if (timer == 0)
            {
                timer = timerMax;
                timerActive = false;
            }
        }

        if (timerActive == false)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (numberToDisplay == numberMax)
                {
                    numberReset = true;
                }
                if (numberReset == false)
                {
                    if (numberToDisplay <= numberMax)
                    {
                        numberToDisplay += numberIncrease;
                    }
                }

                timerActive = true;
                timer = timerMax;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (numberToDisplay == numberMin)
                {
                    numberReset = true;
                }
                if (numberReset == false)
                {
                    if (numberToDisplay >= numberMin)
                    {
                        numberToDisplay -= numberIncrease;
                    }
                }

                timerActive = true;
                timer = timerMax;
            }
        }
        if (numberReset == true)
        {
            numberToDisplay = 8;
            numberReset = false;
        }

        displayText.text = "" + numberToDisplay;
    }
}
