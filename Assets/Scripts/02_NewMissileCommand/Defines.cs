using System.Collections.Generic;

public delegate void VoidVoidDelegate();
delegate int IntvoidDelegate();
delegate int IntIntDelegate(int _value);

public delegate void voidListPoolingObjectDelegate(List<IPoolingObject> _enemyList);

public delegate void AttackDelegate(int _dmg = 1);
// �̷��Ե� �ȴ�
// public ������Ѵ�.
public delegate void MissileStateDelegate(int _missileIdx, bool _isFill);