using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bunny : AnimalBase
{
    [SerializeField] private float _detectionCirlceRadius = 6;

    private InsideBoxState _insideBoxState;
    private WanderState _wonderState;

    private void Start()
    {
        _wonderState = new WanderState(transform, velocity, _maxSpeed / 5f, 5, 3, 5);
        _insideBoxState =
            new InsideBoxState(transform, velocity, _maxSpeed / 5f, new Rect(-Vector2.one * 50, Vector2.one * 100));

        _insideBoxState.OnEndAvoid += _wonderState.Reset;
    }

    protected override List<UnityMovingState> GetMovingStates()
    {
        var c = Physics2D.OverlapCircleAll(transform.position, _detectionCirlceRadius)
                                            .Where(x=>!x.isTrigger).ToArray();
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