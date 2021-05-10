using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsData
{
    public float savedMusicVolume;
    public float savedPlayerVolume;
    public float savedEffectVolume;
    public float savedEnvironmentVolume;
    public float savedAbilityVolume;
    public float savedEnemyVolume;

    public float savedMouseSensitivity;
    public bool savedFPSToggler;

    public SettingsData(SettingsHandler settingsHandler)
    {
        savedMusicVolume = settingsHandler.musicVolume;
        savedPlayerVolume = settingsHandler.playerVolume;
        savedEffectVolume = settingsHandler.effectVolume;
        savedEnvironmentVolume = settingsHandler.environmentVolume;
        savedAbilityVolume = settingsHandler.abilityVolume;
        savedEnemyVolume = settingsHandler.enemyVolume;

        savedMouseSensitivity = settingsHandler.mouseSensitivity;
        savedFPSToggler = settingsHandler.fpsToggler;
    }
}
