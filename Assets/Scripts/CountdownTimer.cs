using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float TimerStart;

    public bool IsExpired
    {
        get { return (_timeRemaining < 0.01f); }
    }

    private float _timeRemaining;
    private bool _messageDisplayed;

    void Awake()
    {
        _timeRemaining = TimerStart;
    }

    void Update()
    {
        if (_timeRemaining > 0f)
        {
            _timeRemaining -= Time.deltaTime;
        }
        else if (!_messageDisplayed)
        {
            _timeRemaining = 0f;
            _messageDisplayed = true;
        }
    }

    public void Reset()
    {
        _timeRemaining = TimerStart;
    }
}
