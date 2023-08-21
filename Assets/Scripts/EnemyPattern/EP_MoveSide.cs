using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EP_MoveSide : EP_EnemyPatternBase
{
    public override void Init(GameObject _target)
    {
        base.Init(_target);

        calcType = ECalcType.Anchor;
    }

    public override Vector3 MovePatternProcess()
    {
        if (!IsInit) return Vector3.zero;

        // �պ���� ���� �ڻ����� Ȱ���ϸ� �ȴ�.
        float sin = Mathf.Sin(Time.time * moveSpeed);
        float pos = sin * (moveWidth * 0.5f);
        return transform.right * pos;
    }


    [SerializeField, Range(10, 20f)]
    private float moveSpeed = 15f;
    [SerializeField, Range(1f, 5f)]
    private float moveWidth = 1f;
}
