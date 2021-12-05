    using UnityEngine;

public class WanderState : UnityMovingState
{
    private readonly float _circleDistance;
    private readonly float _circleRadius;
    private readonly float _shake;

    private float _wonderAngle;
    private Vector3 _wonderVector;
    private Vector3 _circleCoord;


    public override Vector3 GetSpeed()
    {
        _circleCoord = CurrentPos + Velocity.normalized * _circleDistance;
        _wonderAngle += Random.Range(-_shake, _shake);
        _wonderVector = Quaternion.Euler(0, 0, _wonderAngle) * (_circleCoord.normalized * _circleRadius);

        return _circleCoord - CurrentPos + _wonderVector;
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_circleCoord, _circleRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_circleCoord, _circleCoord + _wonderVector);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(CurrentPos, _circleCoord + _wonderVector);
    }

    public WanderState(Transform transform, Vector3 startVelocity, float maxSpeed, float circleDistance,
        float circleRadius, float shake)
        : base(transform, startVelocity, maxSpeed)
    {
        _circleDistance = circleDistance;
        _circleRadius = circleRadius;
        _shake = shake;
    }
}