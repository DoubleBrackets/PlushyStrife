using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CountdownTimerDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _timerText;

    public UnityEvent OnSecondChange;
    public UnityEvent OnStartDisplaying;
    public UnityEvent OnStopDisplaying;

    private bool _isDisplaying;

    [ShowNonSerializedField]
    private int seconds;

    private void Awake()
    {
        SetDisplaying(false);
    }

    private void Update()
    {
        if (_isDisplaying)
        {
            float time = GameManager.Instance.CountdownTimer;
            var timeSeconds = (int)Mathf.Ceil(time);

            if (timeSeconds != seconds)
            {
                seconds = timeSeconds;
                OnSecondChange?.Invoke();
            }

            _timerText.text = timeSeconds.ToString();
        }
    }

    public void SetDisplaying(bool isDisplaying)
    {
        _isDisplaying = isDisplaying;

        if (isDisplaying)
        {
            OnStartDisplaying?.Invoke();
            seconds = -1;
        }
        else
        {
            OnStopDisplaying?.Invoke();
        }
    }
}