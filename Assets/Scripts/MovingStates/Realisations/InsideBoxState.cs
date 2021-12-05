using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InsideBoxState : SeekableMovingState
{
    private readonly Rect _box;
    private readonly List<Vector3> _corners;

    public InsideBoxState(Transform transform, Vector3 startVelocity, float maxSpeed, Rect box) :
        base(transform, startVelocity, maxSpeed)
    {
        _box = box;

        _corners = new List<Vector3>
        {
            new Vector3(_box.xMin, _box.yMax, CurrentPos.z),
            new Vector3(_box.xMax, _box.yMin, CurrentPos.z),
            new Vector3(_box.xMin, _box.yMin, CurrentPos.z),
            new Vector3(_box.xMax, _box.yMax, CurrentPos.z),
        };
    }

    private List<Vector3> GetDistractionPoints()
    {
        if (_box.Contains(CurrentPos))
            return new List<Vector3>();

        var distances = _corners.Select(x => CurrentPos - x).ToArray();
        var minDistance = distances.Min(x => x.sqrMagnitude);
        var closestCorner = distances.First(x => Mathf.Approximately(x.sqrMagnitude, minDistance));

        return new List<Vector3>()
        {
            new Vector3(CurrentPos.x, closestCorner.y, CurrentPos.z),
            new Vector3(closestCorner.x, CurrentPos.y, CurrentPos.z)
        };
    }

    public override Vector3 GetSpeed()
    {
        var result = Vector3.zero;
        GetDistractionPoints().ForEach(x => result += Seek(x));
        return result;
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_box.center, _box.size);
        Gizmos.color = Color.magenta;
        GetDistractionPoints().ForEach(x => Gizmos.DrawLine(CurrentPos, CurrentPos + Seek(x)));
        GetDistractionPoints().ForEach(x => Gizmos.DrawWireSphere(x, 0.5f));
    }
}