using UnityEngine;

public interface IPoolingObject
{
    void Init(GameObject _target, AttackDelegate _attackCallback);
    void Release();
}
