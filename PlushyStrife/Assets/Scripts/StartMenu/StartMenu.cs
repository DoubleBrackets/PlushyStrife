using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace StartMenu
{
    public class StartMenu : MonoBehaviour
    {
        public UnityEvent OnStartGame;
        public UnityEvent OnExit;

        private bool _isGameStarted;

        public void StartGame()
        {
            if (_isGameStarted)
            {
                return;
            }

            _isGameStarted = true;

            StartGameAsync().Forget();
        }

        private async UniTaskVoid StartGameAsync()
        {
            OnStartGame?.Invoke();

            await UniTask.Delay(1000);

            OnExit?.Invoke();
        }
    }
}