using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour
{
    [SerializeField, Min(0)] protected float _maxSpeed = 5;
    [SerializeField, Min(0)] protected float _shrapness = 0.5f;

    protected Vector3 velocity = Vector3.zero;
    protected abstract List<UnityMovingState> GetMovingStates();
    private List<UnityMovingState> _currentStates;
    public void Move()
    {
        _currentStates = GetMovingStates();

        var steering = Vector3.zero;
        _currentStates.ForEach(x => steering += x.GetSpeed());
        
        steering = Truncate(steering, _shrapness);
        velocity = Truncate(steering + velocity, _maxSpeed);
        
        Debug.Log($"steering {steering}");
        Debug.Log($"velocity {velocity}");
        
        _currentStates.ForEach(x => x.Velocity = velocity);

        transform.Translate(Truncate(velocity, _maxSpeed) * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        _currentStates = GetMovingStates();
        Move();
    }

    private Vector3 Truncate(Vector3 vector, float dist)
    {
        if (vector.magnitude > dist)
            return vector.normalized * dist;
        else
            return vector;
    }

    protected virtual void OnDrawGizmos()
    {
        _currentStates?.ForEach(x => x.OnDrawGizmos());
    }
}