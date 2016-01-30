using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float TimerStart;


    private float _timeRemaining;
    private bool _messageDisplayed;

    void Awake()
    {
        _timeRemaining = TimerStart;
        _messageDisplayed = false;
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
            Debug.Log("Time expired!");
        }
    }
}
