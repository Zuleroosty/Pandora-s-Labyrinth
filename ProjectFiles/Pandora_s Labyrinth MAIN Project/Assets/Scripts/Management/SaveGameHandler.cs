using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameHandler : MonoBehaviour
{
    PlayerController playerCon;
    StatHandler gameStats;

    private void Start()
    {
        playerCon = GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>();
        gameStats = GameObject.Find(">GameManager<").GetComponent<StatHandler>();

        if (PlayerPrefs.GetInt("saveGameAvailable") != 1) PlayerPrefs.SetInt("saveGameAvailable", 0); // CHECK IF SAVE GAME AVAILABLE - IF NO THEN SET TO 0 (NO SAVE GAME)
    }
    private void Update()
    {
        if (playerCon == null) playerCon = GameObject.Find("----PlayerObjectParent----").GetComponent<PlayerController>();
    }
    public void SaveInfoToFile()
    {
        PlayerPrefs.SetInt("saveGameAvailable", 1); // SET SAVE GAME AVAILABLE FOR FUTURE USE
                                                    
        // PLAYER VARIABLES
        PlayerPrefs.SetInt("playerLevel", playerCon.level);
        PlayerPrefs.SetFloat("maxXP", playerCon.maxXp);
        PlayerPrefs.SetFloat("totalXP", playerCon.xp);
        PlayerPrefs.SetFloat("health", playerCon.health);
        PlayerPrefs.SetFloat("maxHealth", playerCon.maxHealth);
        PlayerPrefs.SetInt("bombCount", playerCon.totalBombs);
        PlayerPrefs.SetInt("potionCount", playerCon.totalMedkits);
        PlayerPrefs.SetInt("goldCount", playerCon.gold);
        PlayerPrefs.SetFloat("healthIncrease", playerCon.healthIncrease);
        PlayerPrefs.SetInt("healthLevel", playerCon.healthRequiredLevel);
        PlayerPrefs.SetFloat("speedIncrease", playerCon.speedIncrease);
        PlayerPrefs.SetInt("speedLevel", playerCon.speedRequiredLevel);
        PlayerPrefs.SetInt("spearLevel", playerCon.currentSpear);

        // GAME STAT VARIABLES
        PlayerPrefs.SetInt("currentPlayLevel", GameObject.Find(">GameManager<").GetComponent<LevelHandler>().currentPlayLevel);
        PlayerPrefs.SetInt("levelsCompleted", gameStats.levelsCompleted);
        PlayerPrefs.SetInt("roomsDiscovered", gameStats.roomsDiscovered);
        PlayerPrefs.SetInt("roomsEntered", gameStats.roomsEntered);
        PlayerPrefs.SetInt("playSeconds", gameStats.second);
        PlayerPrefs.SetInt("playMinutes", gameStats.minute);
        PlayerPrefs.SetInt("playHours", gameStats.hour);

        // KILLS
        PlayerPrefs.SetInt("spidersKilled", gameStats.spidersKilled);
        PlayerPrefs.SetInt("goblinsKilled", gameStats.goblinsKilled);
        PlayerPrefs.SetInt("scorpionsKilled", gameStats.scorpionsKilled);
        PlayerPrefs.SetInt("minotaursKilled", gameStats.minotaursKilled);

        // PLAYER STATS
        PlayerPrefs.SetFloat("damageDealt", gameStats.damageDealt);
        PlayerPrefs.SetFloat("damageTaken", gameStats.damageTaken);
        PlayerPrefs.SetInt("spearsThrown", gameStats.spearsThrown);
        PlayerPrefs.SetInt("bombsDropped", gameStats.bombsDropped);
        PlayerPrefs.SetInt("potionsDrank", gameStats.potionsDrank);
        PlayerPrefs.SetInt("playerScore", gameStats.playerScore);

        PlayerPrefs.Save();
    }
    public void LoadInfoFromFile()
    {
        // PLAYER VARIABLES
        playerCon.level = PlayerPrefs.GetInt("playerLevel");
        playerCon.maxXp = PlayerPrefs.GetFloat("maxXP");
        playerCon.xp = PlayerPrefs.GetFloat("totalXP");
        playerCon.health = PlayerPrefs.GetFloat("health");
        playerCon.maxHealth = PlayerPrefs.GetFloat("maxHealth");
        playerCon.totalBombs = PlayerPrefs.GetInt("bombCount");
        playerCon.totalMedkits = PlayerPrefs.GetInt("potionCount");
        playerCon.gold = PlayerPrefs.GetInt("goldCount");
        playerCon.healthIncrease = PlayerPrefs.GetFloat("healthIncrease");
        playerCon.healthRequiredLevel = PlayerPrefs.GetInt("healthLevel");
        playerCon.speedIncrease = PlayerPrefs.GetFloat("speedIncrease");
        playerCon.speedRequiredLevel = PlayerPrefs.GetInt("speedLevel");
        playerCon.currentSpear = PlayerPrefs.GetInt("spearLevel");

        // GAME STAT VARIABLES
        GameObject.Find(">GameManager<").GetComponent<LevelHandler>().currentPlayLevel = PlayerPrefs.GetInt("currentPlayLevel");
        gameStats.levelsCompleted = PlayerPrefs.GetInt("levelsCompleted");
        gameStats.roomsDiscovered = PlayerPrefs.GetInt("roomsDiscovered");
        gameStats.roomsEntered = PlayerPrefs.GetInt("roomsEntered");
        gameStats.second = PlayerPrefs.GetInt("playSeconds");
        gameStats.minute = PlayerPrefs.GetInt("playMinutes");
        gameStats.hour = PlayerPrefs.GetInt("playHours");

        // KILLS
        gameStats.spidersKilled = PlayerPrefs.GetInt("spidersKilled");
        gameStats.goblinsKilled = PlayerPrefs.GetInt("goblinsKilled");
        gameStats.scorpionsKilled = PlayerPrefs.GetInt("scorpionsKilled");
        gameStats.minotaursKilled = PlayerPrefs.GetInt("minotaursKilled");

        // PLAYER STATS
        gameStats.damageDealt = PlayerPrefs.GetFloat("damageDealt");
        gameStats.damageTaken = PlayerPrefs.GetFloat("damageTaken");
        gameStats.spearsThrown = PlayerPrefs.GetInt("spearsThrown");
        gameStats.bombsDropped = PlayerPrefs.GetInt("bombsDropped");
        gameStats.potionsDrank = PlayerPrefs.GetInt("potionsDrank");
        gameStats.playerScore = PlayerPrefs.GetInt("playerScore");
    }
}
