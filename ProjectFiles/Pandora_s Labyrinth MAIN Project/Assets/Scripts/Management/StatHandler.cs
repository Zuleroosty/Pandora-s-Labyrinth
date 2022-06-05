using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    public int spidersKilled, goblinsKilled, scorpionsKilled, minotaursKilled;
    public int spearsThrown, bombsDropped, potionsDrank;
    public int playerLevel, playerScore, roomsDiscovered, roomsEntered, levelsCompleted;
    public float currentXP, maxXP, damageDealt, damageTaken;
    public string playtimeString, xpString;

    string secondString, minuteString, hourString;
    int frame, second, minute, hour;
    bool hasReset;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Pause) GameObject.Find("DisplayScoreText").GetComponent<TextMesh>().text = "SCORE: " + playerScore.ToString();
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            //-------------------------------------------------------------------
            // PLAYTIMER
            //-------------------------------------------------------------------

            if (frame < 60) frame++;
            if (frame >= 60)
            {
                frame = 0;
                second++;
            }
            if (second >= 60)
            {
                second = 0;
                minute++;
            }
            if (minute >= 60)
            {
                minute = 0;
                hour++;
            }

            // CONVERT TIMER TO STRING
            if (second < 10) secondString = "0" + second;
            else secondString = "" + second;
            if (minute < 10) minuteString = "0" + minute;
            else minuteString = "" + minute;
            if (hour < 10) hourString = "0" + hour;
            else hourString = "" + hour;

            playtimeString = hourString + ":" + minuteString + ":" + secondString;

            //-------------------------------------------------------------------
            // PLAYER STATS
            //-------------------------------------------------------------------

            playerLevel = GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().level;
            currentXP = GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().xp;
            maxXP = GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>().maxXp;
            xpString = currentXP + "/" + maxXP + " XP";

            //-------------------------------------------------------------------
            //-------------------------------------------------------------------

            hasReset = false;
        }
        else if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.Menu && !hasReset)
        {
            ResetStats();
            hasReset = true;
        }
    }
    public void ResetStats()
    {
        frame = 0;
        second = 0;
        minute = 0;

        spearsThrown = 0;
        bombsDropped = 0;
        potionsDrank = 0;
        roomsDiscovered = 0;
        roomsEntered = 0;
        levelsCompleted = 0;
        damageDealt = 0;
        damageTaken = 0;
        playerScore = 0;
    }
}
