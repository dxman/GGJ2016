using UnityEngine;

public class PlayerInputBehavior : Behavior
{
    private bool _isCameraRelative;

    private IHumanControl _myHumanControl;
    private ICameraControl _myCameraControl;

    private Transform _cameraTransform;

    private Vector3 _moveInput;
    private Vector3 _cameraMoveInput;
    private bool _jumpReleased;

    public PlayerInputBehavior(ControlComponent control, bool isCameraRelative) : base(control)
    {
        _myHumanControl = control as IHumanControl;
        _myCameraControl = control as ICameraControl;

        _isCameraRelative = isCameraRelative;

        GameObject obj = GameObject.FindGameObjectWithTag("MainCamera");
        if (obj != null)
        {
            _cameraTransform = obj.GetComponent<Transform>();

        }
        else
        {
            Debug.LogError("Main Camera not found!");
        }

        _jumpReleased = true;
    }

    protected override void Execute()
    {
        if (_myHumanControl != null)
        {
            _moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            if (_isCameraRelative)
            {
                Vector3 cameraDirection = _cameraTransform.forward;
                cameraDirection.y = 0f;
                Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, cameraDirection);
                _moveInput = referentialShift * _moveInput;
            }
            _myHumanControl.Move(_moveInput);

            if (!Input.GetButton("Jump"))
            {
                _jumpReleased = true;
            }
            else if (_jumpReleased)
            {
                _jumpReleased = false;
                _myHumanControl.Jump();
            }
        }
        
        if (_myCameraControl != null)
        {
            _cameraMoveInput = new Vector3(Input.GetAxisRaw("RightH"), 0, Input.GetAxisRaw("RightV"));
            _myCameraControl.CameraMove(_cameraMoveInput);
        }
    }
}