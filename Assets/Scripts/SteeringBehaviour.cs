using UnityEngine;
using Random = UnityEngine.Random;

public class SteeringBehaviour : MonoBehaviour
{
    [SerializeField, Min(0)] private float _maxSpeed = 5;
    [SerializeField, Min(0)] private float _shrapness = 0.5f;

    protected UnityMovingState currentState;

    public void Move(Vector3 steering)
    {
        steering = Truncate(steering, _shrapness);
        currentState.Velocity = Truncate(steering + currentState.Velocity, _maxSpeed);
        transform.Translate(currentState.Velocity * Time.fixedDeltaTime);
    }

    private Vector3 Truncate(Vector3 vector, float dist)
    {
        if (vector.magnitude > dist)
            return vector.normalized * dist;
        else
            return vector;
    }

    private void OnDrawGizmos()
    {
        currentState?.OnDrawGizmos();
    }
}