using UnityEngine;

public abstract class SeekableMovingState : UnityMovingState
{
    protected Vector3 Seek(Vector3 targetPos)
    {
        var distance = targetPos - CurrentPos;
        return (Vector3.Normalize(distance) * MaxVelocity) - Velocity;
    }

    protected Vector3 Flee(Vector3 evadingPos)
    {
        var desiredVelocity = (evadingPos - CurrentPos) * MaxVelocity;
        return desiredVelocity - Velocity;
    }

    protected SeekableMovingState(Transform transform, Vector3 startVelocity, float maxSpeed)
        : base(transform, startVelocity, maxSpeed)
    {
    }
}