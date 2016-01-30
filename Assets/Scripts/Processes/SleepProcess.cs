using System;

public class SleepProcess : Process
{
    private readonly float _duration;
    private float _timer;

    public SleepProcess(float duration)
    {
        _duration = duration;
        _timer = 0f;
    }

    public override void Update(float timeElapsed)
    {
        _timer += timeElapsed;
        if (_timer >= _duration)
        {
            Finished = true;
        }
    }
}
