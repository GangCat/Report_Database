using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EP_RotateToTargetSmooth : EP_EnemyPatternBase
{
    public override void Init(GameObject _target)
    {
        base.Init(_target);

        calcType = ECalcType.Rotation;

        curRotRatio = rotRatio;

        Vector3 myPos = transform.position;
        Vector3 targetPos = target.transform.position;
        targetPos.y = myPos.y;
        Vector3 dir = myPos - targetPos;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    public override Quaternion RotatePatternProcess()
    {
        Vector3 myPos = transform.position;
        Vector3 targetPos = target.transform.position;
        targetPos.y = myPos.y;
        Vector3 dir = targetPos - myPos;

        Quaternion lerp = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), curRotRatio * 0.01f);
        curRotRatio += 0.0001f;

        return lerp;
    }

    [SerializeField, Range(0.1f, 1f)]
    private float rotRatio = 0.3f;

    private float curRotRatio = 0f;
}
