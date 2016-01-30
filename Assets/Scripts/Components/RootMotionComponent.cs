using UnityEngine;

public class RootMotionComponent
{
    private void OnUpdate(float deltaTime)
    {
        /*
        // Only needed by root motion
        Vector3 rootDirection = _myTransform.forward;

        // This next part gives the angle for if root motion animation is being used, but it's not needed for the current non-root movement
        Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);
        float angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);
        angleRootToMove /= 180f;
        */
    }
}
