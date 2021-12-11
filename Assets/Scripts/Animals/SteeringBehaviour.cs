using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour
{
    [SerializeField, Min(0)] protected float _maxSpeed = 5;
    [SerializeField, Min(0)] protected float _shrapness = 0.5f;

    protected Vector3 velocity = Vector3.zero;
    protected abstract List<UnityMovingState> GetMovingStates();
    private List<UnityMovingState> _currentStates;

    private Vector3 prevVelocity;

    public void Move()
    {
        _currentStates = GetMovingStates();

        var steering = Vector3.zero;
        _currentStates.ForEach(x => steering += x.CalculatedSpeed);

        steering *= _shrapness;
        var maxSpeed = _currentStates.Max(x => x.MaxVelocity);
        velocity = UnityMovingState.Truncate(steering + velocity, maxSpeed);
        
        _currentStates.ForEach(x => x.Velocity = velocity);
        
        transform.Translate(velocity * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        _currentStates = GetMovingStates();
        Move();
    }

    protected virtual void OnDrawGizmos()
    {
        _currentStates?.ForEach(x => x.OnDrawGizmos());
    }
}