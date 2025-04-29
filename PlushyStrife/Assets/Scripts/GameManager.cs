using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private enum GameState
    {
        CountingDown,
        InGame,
        End
    }

    private const string HighScorePref = "plushyStrifeHighScore";
    public static GameManager Instance;

    [Header("Gameplay")]

    [SerializeField]
    private float _duration;

    [SerializeField]
    private float _countdownDuration;

    [Header("Events")]

    public UnityEvent OnEnterCountdown;

    public UnityEvent OnEnterGame;

    public UnityEvent OnEnterEnd;

    public float GameplayTimer => _gameplayTimer;
    public float GameDuration => _duration;
    public float CountdownTimer => _countdownTimer;

    [ShowNonSerializedField]
    private GameState _state;

    [ShowNonSerializedField]
    private float _gameplayTimer;

    [ShowNonSerializedField]
    private float _countdownTimer;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        EnterCountdown();
    }

    private void Update()
    {
        if (_state == GameState.InGame)
        {
            _gameplayTimer -= Time.deltaTime;

            if (_gameplayTimer <= 0f)
            {
                EnterEndState();
            }
        }

        else if (_state == GameState.CountingDown)
        {
            _countdownTimer -= Time.deltaTime;

            if (_countdownTimer <= 0f)
            {
                EnterGameplayState();
            }
        }
    }

    private void EnterEndState()
    {
        _state = GameState.End;
        OnEnterEnd?.Invoke();
    }

    private void EnterCountdown()
    {
        _state = GameState.CountingDown;
        _countdownTimer = _countdownDuration;
        OnEnterCountdown?.Invoke();
    }

    private void EnterGameplayState()
    {
        _state = GameState.InGame;
        _gameplayTimer = _duration;
        OnEnterGame?.Invoke();
    }
}