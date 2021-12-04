using UnityEngine;

public class PursuitState : SeekableMovingState
{
    private readonly IMovingState _target;

    private Vector3 _seekingPos
    {
        get
        {
            var renderingRange = (CurrentPos - _target.CurrentPos).magnitude / MaxVelocity;
            return _target.CurrentPos + _target.Velocity * renderingRange;
        }
    }

    public override Vector3 GetSpeed() => Seek(_seekingPos);

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(CurrentPos, _seekingPos);
        Gizmos.DrawWireSphere(_seekingPos, 0.2f);
    }

    public PursuitState(Transform transform, Vector3 startVelocity, float maxSpeed, IMovingState target)
        : base(transform, startVelocity, maxSpeed)
    {
        _target = target;
    }
}