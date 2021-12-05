using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SteeringBehaviour : MonoBehaviour
{
    [SerializeField, Min(0)] private float _maxSpeed = 5;
    [SerializeField, Min(0)] private float _shrapness = 0.5f;

    protected List<UnityMovingState> currentStates;

    private void Start()
    {
        currentStates = new List<UnityMovingState>()
        {
            new InsideBoxState(transform, Vector3.zero, _maxSpeed, new Rect(-Vector2.one * 50, Vector2.one * 100)),
            new WanderState(transform, Vector3.zero, _maxSpeed, 6, 3, 5)
        };
    }

    public void Move(Vector3 steering)
    {
        steering = Truncate(steering, _shrapness);
        currentStates.ForEach(x => x.Velocity = Truncate(steering + x.Velocity, _maxSpeed));

        var velocity = Vector3.zero;
        currentStates.ForEach(x => velocity += x.Velocity);

        transform.Translate(Truncate(velocity, _maxSpeed) * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        var velocity = Vector3.zero;
        currentStates.ForEach(x => velocity += x.GetSpeed());

        Move(velocity);
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
        currentStates?.ForEach(x => x.OnDrawGizmos());
    }
}