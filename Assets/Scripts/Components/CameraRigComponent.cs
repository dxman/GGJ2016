using UnityEngine;

public enum CameraMode { Fixed, Follow };

[System.Serializable]
public struct CameraRigStatus
{
    public Vector3 Euler;
    public Vector3 FollowVelocity;
}

[RequireComponent(typeof(ControlComponent))]
public class CameraRigComponent : MonoBehaviour
{
    public Transform FollowTarget;
    public Vector3 FollowOffset;
    public float FollowHeight;
    public float FollowDistance;
    public float RotateSpeed;
    public float LookSpeed;
    public float DampTime;

    [SerializeField]
    private CameraRigStatus _cameraRigStatus;

    private Transform _myTransform;
    private ControlComponent _myControl;

    private ICameraControl _myCameraControl;

    private Vector3 _targetDirection;

    void Awake()
    {
        _myTransform = GetComponent<Transform>();
        _myControl = GetComponent<ControlComponent>();

        _myCameraControl = _myControl as ICameraControl;
    }

    void OnEnable()
    {
        _cameraRigStatus.FollowVelocity = Vector3.zero;

        _myControl.UpdateEvent += OnUpdate;

        if (_myCameraControl != null)
        {
            _myCameraControl.CameraMoveEvent += OnCameraMove;
        }
    }

    void OnDisable()
    {
        _myControl.UpdateEvent -= OnUpdate;

        if (_myCameraControl != null)
        {
            _myCameraControl.CameraMoveEvent -= OnCameraMove;
        }
    }
    
    private void OnUpdate(float deltaTime)
    {
        // TODO: Move following code into its own AI behavior
        if (FollowTarget != null)
        {
            Vector3 lookDir = FollowTarget.position + FollowOffset - _myTransform.position;
            lookDir.y = 0f;
            lookDir.Normalize();

            Vector3 targetPosition = FollowTarget.position + FollowOffset + (FollowTarget.up * FollowHeight) - (lookDir * FollowDistance);
            //Vector3 targetPosition = new Vector3(FollowTarget.position.x, FollowTarget.position.y + FollowHeight, FollowTarget.position.z - FollowDistance);
            
            _myTransform.position = Vector3.SmoothDamp(_myTransform.position, targetPosition, ref _cameraRigStatus.FollowVelocity, DampTime / _myControl.TimeScale);
            _myTransform.RotateAround((FollowTarget.position + FollowOffset), Vector3.up, RotateSpeed * _targetDirection.x * deltaTime);
            _myTransform.rotation = Quaternion.RotateTowards(_myTransform.rotation, Quaternion.LookRotation((FollowTarget.position + FollowOffset) - _myTransform.position, Vector3.up), LookSpeed * deltaTime);
        }
    }

    private void OnCameraMove(Vector3 targetDirection)
    {
        _targetDirection = targetDirection;
        if (_targetDirection.magnitude > 1f)
        {
            _targetDirection /= _targetDirection.magnitude;
        }
    }
}
