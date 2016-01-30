using UnityEngine;

public class FollowBehavior : Behavior
{
    private Transform _myTransform;

    private IHumanControl _myHumanControl;

    private Transform _followTarget;

    private Vector3 _moveInput;

    public FollowBehavior(ControlComponent control, Transform followTarget) : base(control)
    {
        _myTransform = control.gameObject.GetComponent<Transform>();

        _myHumanControl = control as IHumanControl;

        _followTarget = followTarget;
    }

    protected override void Execute()
    {
        if (_myHumanControl != null)
        {
            if (_followTarget != null)
            {
                _moveInput = _followTarget.position - _myTransform.position;
            }
            else
            {
                _moveInput = Vector3.zero;
            }
            _myHumanControl.Move(_moveInput);
        }
    }
}