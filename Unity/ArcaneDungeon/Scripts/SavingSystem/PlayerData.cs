using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int savedLevel;
    public int savedExperience;
    public int savedCoins;
    public int maxXp;

    public PlayerData(PlayerManager playerManager)
    {
        savedLevel = playerManager.level;
        savedExperience = playerManager.currentExperience;
        savedCoins = playerManager.currentCoins;
    }
}
