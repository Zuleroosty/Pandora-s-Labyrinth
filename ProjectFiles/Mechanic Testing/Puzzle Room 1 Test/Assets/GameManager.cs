using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int compRequired, compRecieved, c1, c2, c3, c4, generatedColours, selectedColour, coloursTaken;
    public bool puzzleComplete, c1Assigned, c2Assigned, c3Assigned, c4Assigned;

    // Start is called before the first frame update
    void Start()
    {
        compRequired = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (compRecieved >= compRequired)
        {
            print("Puzzle Complete!");
            puzzleComplete = true;
        }

        // Generate Puzzle
        if (generatedColours < 4)
        {
            selectedColour = Random.Range(1, 6);
            switch (generatedColours)
            {
                case 0:
                    if (selectedColour != c1 && selectedColour != c2 && selectedColour != c3 && selectedColour != c4)
                    {
                        c1 = selectedColour;
                        generatedColours++;
                    }
                    break;
                case 1:
                    if (selectedColour != c1 && selectedColour != c2 && selectedColour != c3 && selectedColour != c4)
                    {
                        c2 = selectedColour;
                        generatedColours++;
                    }
                    break;
                case 2:
                    if (selectedColour != c1 && selectedColour != c2 && selectedColour != c3 && selectedColour != c4)
                    {
                        c3 = selectedColour;
                        generatedColours++;
                    }
                    break;
                case 3:
                    if (selectedColour != c1 && selectedColour != c2 && selectedColour != c3 && selectedColour != c4)
                    {
                        c4 = selectedColour;
                        generatedColours++;
                    }
                    break;
            }
        }
    }
}
