using UnityEngine;

public class BehaviorSystem : MonoBehaviour
{
    private PlayerInputBehavior _playerBehavior;
    private PlayerInputBehavior _cameraBehavior;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ControlComponent playerControl = player.GetComponent<ControlComponent>();
        _playerBehavior = new PlayerInputBehavior(playerControl, true);

        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        ControlComponent cameraControl = camera.GetComponent<ControlComponent>();
        _cameraBehavior = new PlayerInputBehavior(cameraControl, true);
    }

    void Update()
    {
        _playerBehavior.Update();
        _cameraBehavior.Update();
    }
}

public abstract class Behavior
{
    private ControlComponent _myControl;

    public Behavior(ControlComponent control)
    {
        _myControl = control;
    }

    public void Update()
    {
        if (_myControl != null)
        {
            Execute();
        }
    }

    protected ControlComponent Control
    {
        get { return _myControl; }
    }

    protected abstract void Execute();
}