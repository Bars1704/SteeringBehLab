using UnityEngine;

public class SeekState : SeekableMovingState
{
    private Transform _target;

    public override Vector3 GetSpeed() => Seek(_target.position);

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(CurrentPos, _target.position);
    }

    public SeekState(Transform transform, Vector3 startVelocity, float maxSpeed, Transform target)
        : base(transform, startVelocity, maxSpeed)
    {
        _target = target;
    }
}