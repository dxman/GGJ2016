using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MoveComponent))]
[RequireComponent(typeof(ControlComponent))]
public class AnimationComponent : MonoBehaviour
{
    private Transform _myTransform;
    private Animator _myAnimator;
    private MoveComponent _myMovement;
    private ControlComponent _myControl;

    private MoveStatus _moveStatus;

    void Awake()
    {
        _myTransform = GetComponent<Transform>();
        _myAnimator = GetComponentInChildren<Animator>();
        _myMovement = GetComponent<MoveComponent>();
        _myControl = GetComponent<ControlComponent>();
    }

    void OnEnable()
    {
        _myControl.UpdateEvent += OnUpdate;
    }

    void OnDisable()
    {
        _myControl.UpdateEvent -= OnUpdate;
    }

    private void OnUpdate(float deltaTime)
    {
        _moveStatus = _myMovement.GetMoveStatus();

        _myAnimator.speed = _myControl.TimeScale;
        _myAnimator.SetBool("IsGrounded", _moveStatus.IsGrounded);
        _myAnimator.SetFloat("PlanarVelocity", _moveStatus.PlanarVelocity);
    }
}
