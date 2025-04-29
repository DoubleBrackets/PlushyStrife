using UnityEngine;
using UnityEngine.Events;

namespace StartMenu
{
    public class StartMenu : MonoBehaviour
    {
        public UnityEvent OnStartGame;

        private bool _isGameStarted;

        public void StartGame()
        {
            if (_isGameStarted)
            {
                return;
            }

            _isGameStarted = true;

            OnStartGame?.Invoke();
        }
    }
}