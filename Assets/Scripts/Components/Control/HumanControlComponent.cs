using UnityEngine;

using System;

public interface IHumanControl
{
    event Action<Vector3> MoveEvent;
    event Action JumpEvent;

    void Move(Vector3 targetDirection);

    void Jump();
}

public class HumanControlComponent : ControlComponent, IHumanControl
{
    public event Action<Vector3> MoveEvent = delegate { };
    public event Action JumpEvent = delegate { };

    public void Move(Vector3 targetDirection)
    {
        if (!IsPaused)
        {
            MoveEvent.Invoke(targetDirection);
        }
    }

    public void Jump()
    {
        if (!IsPaused)
        {
            JumpEvent.Invoke();
        }
    }
}