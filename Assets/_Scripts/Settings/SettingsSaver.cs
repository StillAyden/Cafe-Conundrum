using System.IO;
using UnityEngine;

public static class SettingsSaver
{
    private static string settingsFilePath = Path.Combine(Application.persistentDataPath, "GameSettings.json");

    public static void SaveSettings(GameSettings gameSettings)
    {
        string json = JsonUtility.ToJson(gameSettings);
        File.WriteAllText(settingsFilePath, json);
    }

    public static void LoadSettings(GameSettings gameSettings)
    {
        if (File.Exists(settingsFilePath))
        {
            string json = File.ReadAllText(settingsFilePath);
            JsonUtility.FromJsonOverwrite(json, gameSettings);
        }
        else
        {
            // Save default settings if no file exists
            SaveSettings(gameSettings);
        }
    }
}
