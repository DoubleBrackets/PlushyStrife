using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class LeaderboardScreen : MonoBehaviour
{
    [SerializeField]
    private LeaderboardEntry leaderBoardEntryPrefab;

    [SerializeField]
    private SaveDataSO saveData;

    [SerializeField]
    private RectTransform contentParent;

    [SerializeField]
    private UnityEvent onPressButton;

    [SerializeField]
    private UnityEvent onContinue;

    private bool didContinue;

    private void Start()
    {
        Resources.UnloadUnusedAssets();

        saveData.LoadSaveFromFile();

        // Load leaderboard data from JSON file
        var rank = 1;
        foreach (SaveDataSO.LeaderboardEntryData entry in saveData.SaveData.leaderboardEntries.OrderByDescending(a =>
                     a.Score))
        {
            // Create a new leaderboard entry
            LeaderboardEntry leaderboardEntry = Instantiate(leaderBoardEntryPrefab, contentParent);
            leaderboardEntry.gameObject.SetActive(true);

            // load texture data
            Texture2D tex = saveData.LoadImageForEntry(entry);

            // Initialize the leaderboard entry with data
            leaderboardEntry.Initialize(
                rank,
                entry.Name,
                entry.Weapon,
                entry.Score,
                tex
            );

            rank++;
        }
    }

    public void Finish()
    {
        if (didContinue)
        {
            return;
        }

        didContinue = true;
        // Invoke the continue event
        onPressButton?.Invoke();

        FinishAsync().Forget();
    }

    private async UniTaskVoid FinishAsync()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        onContinue?.Invoke();
    }
}