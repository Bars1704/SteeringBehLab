using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPursuitable 
{
    Vector3 Velocity { get; }
    Vector3 Position { get; }
}
