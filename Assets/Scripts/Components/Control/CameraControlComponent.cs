using UnityEngine;

using System;

public interface ICameraControl
{
    event Action<Vector3> CameraMoveEvent;

    void CameraMove(Vector3 targetDirection);
}

public class CameraControlComponent : ControlComponent, ICameraControl
{
    public event Action<Vector3> CameraMoveEvent = delegate { };

    public void CameraMove(Vector3 targetDirection)
    {
        if (!IsPaused)
        {
            CameraMoveEvent.Invoke(targetDirection);
        }
    }
}