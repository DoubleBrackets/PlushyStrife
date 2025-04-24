using UnityEngine;

namespace Scoring
{
    public class ScoreIncrementer : MonoBehaviour
    {
        public void IncrementScore(int amount)
        {
            ScoreManager.Instance.AddScore(amount);
        }

        public void IncrementScore()
        {
            ScoreManager.Instance.AddScore(1);
        }
    }
}