using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SteeringBehs : MonoBehaviour
{
    [SerializeField, Min(0)] private float _maxSpeed = 5;
    [SerializeField, Min(0)] private float _shrapness = 0.5f;
    [SerializeField, Min(0)] private float _gizmosLengthMultiplier = 2;
    [SerializeField, Min(0)] private float _deadZoneRadius = 2;
    [SerializeField] private Transform target;

    public float MaxSpeed => _maxSpeed;
    public Vector3 Velocity { get; private set; }

    Vector3 Seek(Vector3 target)
    {
        var distance = target - transform.position;
        return (Vector3.Normalize(distance) * MaxSpeed) - Velocity;
    }

    Vector3 Pursuit(SteeringBehs target) => Seek(target.transform.position + target.Velocity * 3);

    private Action OnDrawGizm = null;
    private float sharoAngle = 0;

    Vector3 Sharoebitsya()
    {
        const float circleDistance = 3;
        const float circleRadius = 2;
        const float shake = 15;
        var circleCoord = transform.position + Velocity.normalized * circleDistance;
        sharoAngle += Random.Range(-shake, shake);
        var sharoVector = (Quaternion.Euler(0, 0, sharoAngle) * (circleCoord.normalized * circleRadius));

        OnDrawGizm = () =>
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(circleCoord, circleRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(circleCoord, circleCoord + sharoVector);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, circleCoord + sharoVector);
        };
        return circleCoord - transform.position + sharoVector;
    }

    private void FixedUpdate()
    {
        var steering = Seek();
        steering = Truncrate(steering, _shrapness);
        Velocity = Truncrate(steering + Velocity, MaxSpeed);

        transform.Translate(Velocity * Time.fixedDeltaTime);
    }
    private Vector3 Truncrate(Vector3 vector, float dist)
    {
        if (vector.magnitude > dist)
            return vector.normalized * dist;
        return vector;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, (transform.position + Velocity * _gizmosLengthMultiplier));
        
        OnDrawGizm?.Invoke();
    }
}