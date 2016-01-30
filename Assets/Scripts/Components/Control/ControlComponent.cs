using UnityEngine;

using System;

public class ControlComponent : MonoBehaviour
{
    public event Action<float> UpdateEvent = delegate { };
    public event Action PauseEvent = delegate { };
    public event Action UnpauseEvent = delegate { };

    public float TimeScale;

    [SerializeField]
    protected bool IsPaused;

    private void Update()
    {
        if (!IsPaused)
        {
            UpdateEvent.Invoke(Time.deltaTime * TimeScale);
        }
    }

    public void Pause()
    {
        if (IsPaused)
            return;

        IsPaused = true;
        PauseEvent.Invoke();
    }

    public void Unpause()
    {
        if (!IsPaused)
            return;

        IsPaused = false;
        UnpauseEvent.Invoke();
    }
}