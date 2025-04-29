using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static int CurrentScore;

    [Header("Depends")]

    [SerializeField]
    private TMP_Text scoreText;

    public UnityEvent OnScoreChanged;

    public static ScoreManager Instance { get; private set; }

    private int score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        CurrentScore = 0;
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
        OnScoreChanged?.Invoke();
        CurrentScore = score;
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}