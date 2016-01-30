using UnityEngine;

[System.Serializable]
public struct MoveStatus
{
    public bool IsGrounded;
    public Vector3 PlanarMoveDirection;
    public Vector3 VerticalMoveDirection;
    public Vector3 MoveDirection;
    public float PlanarVelocity;
}

public abstract class MoveComponent : MonoBehaviour
{
    [SerializeField]
    protected MoveStatus MoveStatus;

    public MoveStatus GetMoveStatus()
    {
        return MoveStatus;
    }
}
