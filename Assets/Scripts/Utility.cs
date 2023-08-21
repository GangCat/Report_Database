using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static float DistanceWIthoutYAxis(Vector3 _lhs, Vector3 _rhs)
    {
        _lhs.y = 0f;
        _rhs.y = 0f;
        return (_lhs - _rhs).magnitude;
    }
}
