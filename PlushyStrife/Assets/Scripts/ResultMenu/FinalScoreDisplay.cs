using System;
using Capture;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ResultMenu
{
    public class FinalScoreDisplay : MonoBehaviour
    {
        [SerializeField]
        private SaveDataSO saveData;

        [SerializeField]
        private WebCam webCam;

        [SerializeField]
        private TMP_Text finalScoreText;

        [SerializeField]
        private UnityEvent onIncrementScore;

        [SerializeField]
        private UnityEvent onScoreFinished;

        [SerializeField]
        private UnityEvent onSubmitted;

        [SerializeField]
        private UnityEvent onFinished;

        [SerializeField]
        private TMP_InputField nameField;

        [SerializeField]
        private TMP_InputField weaponField;

        private bool submitted;

        private void Start()
        {
            Resources.UnloadUnusedAssets();

            IncrementScore().Forget();
        }

        private async UniTaskVoid IncrementScore()
        {
            onIncrementScore?.Invoke();

            await UniTask.Delay(TimeSpan.FromSeconds(1));

            int finalScore = ScoreManager.CurrentScore;

            var currentScore = 0;

            for (var i = 0; i < finalScore; i++)
            {
                currentScore++;
                finalScoreText.text = currentScore.ToString();
                onIncrementScore?.Invoke();
                await UniTask.Delay(TimeSpan.FromMilliseconds(150));
            }

            finalScoreText.text = finalScore.ToString();
            onIncrementScore?.Invoke();

            await UniTask.Delay(TimeSpan.FromSeconds(2.5f));

            onScoreFinished?.Invoke();
        }

        public void TrySubmit()
        {
            Submit().Forget();
        }

        private async UniTaskVoid Submit()
        {
            string name = nameField.text;
            string weapon = weaponField.text;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(weapon))
            {
                Debug.Log("Name or weapon is empty");
            }

            if (submitted)
            {
                return;
            }

            submitted = true;

            onSubmitted?.Invoke();

            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));

            saveData.AddLeaderboardEntry(name, ScoreManager.CurrentScore, weapon, webCam.LastCapture);

            onFinished?.Invoke();
        }
    }
}