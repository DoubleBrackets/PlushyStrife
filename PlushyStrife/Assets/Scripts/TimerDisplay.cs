using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimerDisplay : MonoBehaviour
{
    [SerializeField]
    private Slider timerSlider;

    [SerializeField]
    private TMP_Text timerText;

    public UnityEvent OnStartDisplaying;
    public UnityEvent OnStopDisplaying;
    public UnityEvent OnSecondsChanged;

    private bool _isDisplaying;
    private int seconds;

    private void Start()
    {
        SetDisplaying(false);
    }

    private void Update()
    {
        if (_isDisplaying)
        {
            float time = GameManager.Instance.GameplayTimer;
            float totalTime = GameManager.Instance.GameDuration;

            float normalizedTime = time / totalTime;

            timerSlider.value = normalizedTime;

            var timeSeconds = (int)Mathf.Ceil(time);

            if (timeSeconds != seconds)
            {
                seconds = timeSeconds;
                OnSecondsChanged?.Invoke();
            }

            timerText.text = "" + timeSeconds;
        }
    }

    public void SetDisplaying(bool isDisplaying)
    {
        _isDisplaying = isDisplaying;

        if (isDisplaying)
        {
            OnStartDisplaying?.Invoke();
        }
        else
        {
            OnStopDisplaying?.Invoke();
        }
    }
}