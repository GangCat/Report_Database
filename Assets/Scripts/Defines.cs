using System.Collections.Generic;

public delegate void VoidVoidDelegate();
public delegate void VoidStrDelegate(string _valueStr);
public delegate void VoidStrStrDelegate(string _leftValue, string _rightValue);
public delegate int IntvoidDelegate();
public delegate int IntIntDelegate(int _value);
public delegate void voidListPoolingObjectDelegate(List<IPoolingObject> _listEnemy);

public delegate void VoidListDataScoreDelegate(List<SDataScore> _arrayScore);

public delegate void AttackDelegate(int _dmg = 1);
// 이렇게도 된다
// public 해줘야한다.
public delegate void MissileStateDelegate(int _missileIdx, bool _isFill);

public class SDataScore
{
    public string id;
    public int score;
    public int killCnt;
    public int timeSec;
}