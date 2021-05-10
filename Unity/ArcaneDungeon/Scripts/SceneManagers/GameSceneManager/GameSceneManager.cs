using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    private SettingsHandler settingsHandler;
    private PlayerManager playerManager;

    private void Awake()
    {
        settingsHandler = FindObjectOfType<SettingsHandler>();
        playerManager = FindObjectOfType<PlayerManager>();

        settingsHandler.loadSettings();
        playerManager.loadPlayer();
    }


    private void OnApplicationQuit()
    {
        settingsHandler.saveSettings();
        playerManager.savePlayer();
    }
}
