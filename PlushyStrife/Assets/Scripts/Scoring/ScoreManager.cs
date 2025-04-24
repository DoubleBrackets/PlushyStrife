using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Depends")]

    [SerializeField]
    private TMP_Text scoreText;

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

        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}