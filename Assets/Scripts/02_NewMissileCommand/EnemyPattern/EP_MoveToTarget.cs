using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EP_MoveToTarget : EP_EnemyPatternBase
{
    public override void Init(GameObject _target)
    {
        base.Init(_target);

        calcType = ECalcType.NonAnchor;

        // MoveToTarget √ ±‚»≠
        if (isRandomSpeed)
        {
            moveSpeed = Random.Range(5f, 10f);
        }
    }


    public override Vector3 MovePatternProcess()
    {
        if (!IsInit) return Vector3.zero;

        return transform.forward * moveSpeed * Time.deltaTime;
    }

    [SerializeField]
    private bool isRandomSpeed = true;
    [SerializeField, Range(5f, 10f)]
    private float moveSpeed = 1f;
}
