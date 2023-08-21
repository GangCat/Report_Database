using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EP_EnemyPatternBase : MonoBehaviour
{
    public enum ECalcType { None = -1, Anchor, NonAnchor, Rotation }
    public ECalcType CalcType => calcType;

    public bool IsAnchorType
    {
        get => calcType == ECalcType.Anchor;
    }
    public bool IsNonAnchorType
    {
        get => calcType == ECalcType.NonAnchor;
    }
    public bool IsRotationType
    {
        get => calcType == ECalcType.Rotation;
    }
    
    public virtual void Init(GameObject _target)
    {
        target = _target;
        isInit = true;
    }

    public virtual Vector3 MovePatternProcess()
    {
        return Vector3.zero;
    }

    public virtual Quaternion RotatePatternProcess()
    {
        return Quaternion.identity;
    }

    protected bool IsInit => isInit;

    protected GameObject target = null;
    protected ECalcType calcType = ECalcType.None;

    private bool isInit = false;
}
