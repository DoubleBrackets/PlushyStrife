using System;
using System.Collections.Generic;
using System.IO;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveData")]
public class SaveDataSO : ScriptableObject
{
    [Serializable]
    public class LeaderboardEntryData
    {
        public string Name;
        public int Score;
        public string TimeStamp;
        public string Weapon;

        /// <summary>
        ///     Relative image path
        /// </summary>
        public string WeaponImagePath;
    }

    [Serializable]
    public class SaveDataType
    {
        public List<LeaderboardEntryData> leaderboardEntries = new();
    }

    private const string saveFileName = "leaderboard.json";

    public SaveDataType SaveData => saveData;

    [ShowNonSerializedField]
    [AllowNesting]
    private SaveDataType saveData = new();

    public void AddLeaderboardEntry(string userName, int score, string weaponName, Texture2D weaponImage)
    {
        LoadSaveFromFile();

        // Save image to png
        var timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string imagePath = GetImageWeaponPath(userName, weaponName, timeStamp);
        string finalImagePath = Path.Combine(Application.persistentDataPath, imagePath);

        // Create image directory
        string directoryPath = Path.GetDirectoryName(finalImagePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        Debug.Log($"Saving image to: {finalImagePath}");

        File.WriteAllBytes(finalImagePath, weaponImage.EncodeToPNG());

        // Create leaderboard entry
        LeaderboardEntryData entry = new()
        {
            Name = userName,
            Score = score,
            TimeStamp = timeStamp,
            Weapon = weaponName,
            WeaponImagePath = imagePath
        };

        // Add entry to leaderboard
        saveData.leaderboardEntries.Add(entry);

        // Sort leaderboard by score
        saveData.leaderboardEntries.Sort((x, y) => y.Score.CompareTo(x.Score));

        WriteSaveToFile();
    }

    public void WriteSaveToFile()
    {
        string json = JsonUtility.ToJson(saveData, true);
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);

        // verify json
        if (string.IsNullOrEmpty(json))
        {
            Debug.LogError("Failed to serialize save data to JSON.");
            return;
        }

        Debug.Log($"Saving leaderboard to: {filePath}");

        File.WriteAllText(filePath, json);
    }

    public void LoadSaveFromFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            if (string.IsNullOrEmpty(json))
            {
                Debug.LogError("Failed to read save data from JSON.");
                return;
            }

            saveData = JsonUtility.FromJson<SaveDataType>(json);
        }
        else
        {
            Debug.LogWarning($"Save file not found at: {filePath}, using blank save");
            saveData = new SaveDataType();
        }
    }

    private string GetImageWeaponPath(string userName, string weaponName, string timeStamp)
    {
        return $"WeaponImages/{userName}_{weaponName}_{timeStamp}.png";
    }

    public Texture2D LoadImageForEntry(LeaderboardEntryData data)
    {
        string filePath = Path.Combine(Application.persistentDataPath, data.WeaponImagePath);

        if (File.Exists(filePath))
        {
            byte[] fileData = File.ReadAllBytes(filePath);
            Texture2D tex = new(2, 2);
            // will auto-resize the texture dimensions.
            tex.LoadImage(fileData);
            return tex;
        }

        Debug.LogWarning($"Image not found at: {filePath}");
        return null;
    }
}