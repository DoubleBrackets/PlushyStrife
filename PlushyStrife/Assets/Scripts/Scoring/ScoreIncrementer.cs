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
            if (ScoreManager.Instance == null)
            {
                return;
            }

            ScoreManager.Instance.AddScore(1);
        }
    }
}