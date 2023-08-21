using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EP_RotateToTarget : EP_EnemyPatternBase
{
    public override void Init(GameObject _target)
    {
        base.Init(_target);

        calcType = ECalcType.Rotation;
    }

    public override Quaternion RotatePatternProcess()
    {
        Vector3 myPos = transform.position;
        Vector3 targetPos = target.transform.position;
        targetPos.y = myPos.y;
        Vector3 dir = (targetPos - myPos).normalized;

        return Quaternion.LookRotation(dir);
    }
}
