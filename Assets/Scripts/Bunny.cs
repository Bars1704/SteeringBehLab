using System.Collections.Generic;
using UnityEngine;

public class Bunny : SteeringBehaviour
{
    [SerializeField] private float _detectionCirlceRadius = 6;

    private InsideBoxState _insideBoxState;
    private WanderState _wonderState;

    private void Start()
    {
        _wonderState = new WanderState(transform, velocity, _maxSpeed/3, 7, 7, 15);
        _insideBoxState =
            new InsideBoxState(transform, velocity, _maxSpeed/2, new Rect(-Vector2.one * 50, Vector2.one * 100));
    }

    protected override List<UnityMovingState> GetMovingStates()
    {
        var c = Physics2D.OverlapCircleAll(transform.position, _detectionCirlceRadius);
        if (c.Length <= 1)
            return new List<UnityMovingState>() { _wonderState, _insideBoxState };
        else
            return new List<UnityMovingState>()
            {
                _insideBoxState,
                new FleeState(transform, velocity, _maxSpeed, c[1].transform)
            };
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _detectionCirlceRadius);
    }
}