using System.IO;
using UnityEngine;

public static class SaveSystem
{

    //Settings
    private static readonly string settingsPath = Path.Combine(Application.persistentDataPath, "ArcaneDungeonSettings.sf");
    public static void SaveSettings(SettingsHandler settingsHandler)
    {
        Debug.Log("SaveSystem: Saving Settings!");
        SettingsData data = new SettingsData(settingsHandler);

        FileStream stream = new FileStream(settingsPath, FileMode.Create);
        BinaryWriter writer = new BinaryWriter(stream);

        writer.Write(data.savedMusicVolume);
        writer.Write(data.savedPlayerVolume);
        writer.Write(data.savedEffectVolume);
        writer.Write(data.savedEnvironmentVolume);
        writer.Write(data.savedAbilityVolume);
        writer.Write(data.savedEnemyVolume);

        writer.Write(data.savedMouseSensitivity);
        writer.Write(data.savedFPSToggler);

        stream.Close();
    }

    public static SettingsData LoadSettings(SettingsHandler settingsHandler)
    {
        Debug.Log("SaveSystem: Loading Settings!");
        SettingsData data = new SettingsData(settingsHandler);

        FileStream stream = new FileStream(settingsPath, FileMode.Open);
        BinaryReader reader = new BinaryReader(stream);

        if (File.Exists(settingsPath))
        {
            data.savedMusicVolume = reader.ReadSingle();
            data.savedPlayerVolume = reader.ReadSingle();
            data.savedEffectVolume = reader.ReadSingle();
            data.savedEnvironmentVolume = reader.ReadSingle();
            data.savedAbilityVolume = reader.ReadSingle();
            data.savedEnemyVolume = reader.ReadSingle();

            data.savedMouseSensitivity = reader.ReadSingle();
            data.savedFPSToggler = reader.ReadBoolean();

            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Error 404: File with the following path not found!:" + settingsPath);
            return null;
        }
    }


    //Player
    private static readonly string playerPath = Path.Combine(Application.persistentDataPath, "ArcaneDungeonPlayer.sf");
    public static void SavePlayer(PlayerManager playerManager)
    {
        Debug.Log("SaveSystem: Saving Player!");
        PlayerData data = new PlayerData(playerManager);

        FileStream stream = new FileStream(playerPath, FileMode.Create);
        BinaryWriter writer = new BinaryWriter(stream);

        writer.Write(data.savedLevel);
        writer.Write(data.savedExperience);
        writer.Write(data.savedCoins);
        writer.Write(data.maxXp);

        stream.Close();
    }


    public static PlayerData LoadPlayer(PlayerManager playerManager)
    {
        if (File.Exists(playerPath))
        {
            Debug.Log("SaveSystem: Loading Player!");
            PlayerData data = new PlayerData(playerManager);

            FileStream stream = new FileStream(playerPath, FileMode.Open);
            BinaryReader reader = new BinaryReader(stream);

            data.savedLevel = reader.ReadInt32();
            data.savedExperience = reader.ReadInt32();
            data.savedCoins = reader.ReadInt32();

            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Error 404: File with the following path not found!:" + playerPath);
            return null;
        }
    }
}