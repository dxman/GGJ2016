using UnityEngine;

[RequireComponent(typeof(ControlComponent))]
[RequireComponent(typeof(SCCComponent))]
public class SCCMoveComponent : MoveComponent
{
    public float MoveSpeed;
    public float MoveAcceleration;
    public float JumpAcceleration;
    public float JumpHeight;
    public float Gravity;
    public float StopFriction;
    public float TurnSpeed;

    private Transform _myTransform;
    private ControlComponent _myControl;
    private SCCComponent _mySuperCharacterController;

    private Vector3 _targetDirection;
    private Vector3 _previousPosition;
        
    void Awake()
    {
        _myControl = GetComponent<ControlComponent>();
        _myTransform = GetComponent<Transform>();
        _mySuperCharacterController = GetComponent<SCCComponent>();

        _targetDirection = Vector3.zero;
    }

    void OnEnable()
    {
        _mySuperCharacterController.SuperUpdateEvent += OnSuperUpdate;

        if (_myControl is IHumanControl)
        {
            ((IHumanControl)_myControl).MoveEvent += OnMove;
            ((IHumanControl)_myControl).JumpEvent += OnJump;
        }
    }

    void OnDisable()
    {
        _mySuperCharacterController.SuperUpdateEvent -= OnSuperUpdate;

        if (_myControl is IHumanControl)
        {
            ((IHumanControl)_myControl).MoveEvent -= OnMove;
            ((IHumanControl)_myControl).JumpEvent -= OnJump;
        }
    }

    private void OnSuperUpdate(float deltaTime)
    {
        Vector3 moved = new Vector3(_myTransform.position.x - _previousPosition.x, 0f, _myTransform.position.z - _previousPosition.z);
        MoveStatus.PlanarVelocity = moved.magnitude / deltaTime;
        _previousPosition = _myTransform.position;

        // Calculate planar movement
        MoveStatus.PlanarMoveDirection = Math3d.ProjectVectorOnPlane(_mySuperCharacterController.up, MoveStatus.MoveDirection);
        if (_targetDirection != Vector3.zero)
        {
            if (MoveStatus.IsGrounded)
            {
                MoveStatus.PlanarMoveDirection = Vector3.MoveTowards(MoveStatus.PlanarMoveDirection, _targetDirection * MoveSpeed, MoveAcceleration * deltaTime);
            }
            else
            {
                MoveStatus.PlanarMoveDirection = Vector3.MoveTowards(MoveStatus.PlanarMoveDirection, _targetDirection * MoveSpeed, JumpAcceleration * deltaTime);
            }
        } 
        else if (MoveStatus.IsGrounded)
        {
            MoveStatus.PlanarMoveDirection = Vector3.MoveTowards(MoveStatus.PlanarMoveDirection, Vector3.zero, StopFriction * deltaTime);
        }

        if (MoveStatus.IsGrounded && !MaintainingGround())
        {
            DetachFromGround();
        }

        // Calculate vertical movement
        //MoveStatus.VerticalMoveDirection = MoveStatus.MoveDirection - MoveStatus.PlanarMoveDirection;
        if (!MoveStatus.IsGrounded)
        {
            if (Vector3.Angle(MoveStatus.VerticalMoveDirection, _mySuperCharacterController.up) > 90 && AcquiringGround())
            {
                AttachToGround();
            }
            else
            {
                MoveStatus.VerticalMoveDirection -= _mySuperCharacterController.up * Gravity * deltaTime;
            }
        }
        
        // Turn to face the direction of movement
        if (MoveStatus.PlanarMoveDirection.magnitude > 0)
        {
            _myTransform.rotation = Quaternion.RotateTowards(_myTransform.rotation, Quaternion.LookRotation(MoveStatus.PlanarMoveDirection, _mySuperCharacterController.up), TurnSpeed * deltaTime);
        }

        // Move the player by our velocity every frame
        MoveStatus.MoveDirection = MoveStatus.PlanarMoveDirection + MoveStatus.VerticalMoveDirection;
        _myTransform.position += MoveStatus.MoveDirection * deltaTime;
    }

    private void OnMove(Vector3 targetDirection)
    {
        _targetDirection = targetDirection;
        _targetDirection.y = 0f;
        if (_targetDirection.magnitude > 1f)
        {
            _targetDirection /= _targetDirection.magnitude;
        }
    }

    private void OnJump()
    {
        if (MoveStatus.IsGrounded)
        {
            DetachFromGround();
            MoveStatus.VerticalMoveDirection = _mySuperCharacterController.up * CalculateJumpSpeed(JumpHeight, Gravity);
        }
    }

    private void AttachToGround()
    {
        _mySuperCharacterController.EnableSlopeLimit();
        _mySuperCharacterController.EnableClamping();

        MoveStatus.IsGrounded = true;
        MoveStatus.VerticalMoveDirection = Vector3.zero;
    }

    private void DetachFromGround()
    {
        _mySuperCharacterController.DisableClamping();
        _mySuperCharacterController.DisableSlopeLimit();

        MoveStatus.IsGrounded = false;
    }
    
    private bool AcquiringGround()
    {
        return _mySuperCharacterController.currentGround.IsGrounded(false, 0.01f);
    }

    private bool MaintainingGround()
    {
        return _mySuperCharacterController.currentGround.IsGrounded(true, 0.2f);
    }
    
    // Calculate the initial velocity of a jump based off gravity and desired maximum height attained
    private float CalculateJumpSpeed(float jumpHeight, float gravity)
    {
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
}
