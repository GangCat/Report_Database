using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HUD_Missile : MonoBehaviour
{
    public void UpdateMissileStateWithIndex(int _missileIdx, bool _isFill)
    {
        if (_missileIdx < 0) return;
        if (_missileIdx >= listMissile.Count) return;

        if (_isFill)
            listMissile[_missileIdx].Fill();
        else
            listMissile[_missileIdx].Empty();
    }

    public void Init(int _maxMissileCnt)
    {
        if (_maxMissileCnt < 1) _maxMissileCnt = 1;

        for (int i = 0; i < _maxMissileCnt; ++i)
        {
            GameObject missileGo = Instantiate(missilePrefab, missileGroupTr);
            UI_HUD_Missile_Missile missile = missileGo.GetComponent<UI_HUD_Missile_Missile>();

            missile.SetLocalPosition(new Vector3(i * missileOffset, 0f, 0f));
            listMissile.Add(missile);
        }
    }

    public void ReloadAll()
    {
        foreach (UI_HUD_Missile_Missile missile in listMissile)
            missile.Fill();
    }

    [SerializeField]
    private RectTransform missileGroupTr = null;
    [SerializeField]
    private GameObject missilePrefab = null;

    private float missileOffset = 60f;

    private List<UI_HUD_Missile_Missile> listMissile =
        new List<UI_HUD_Missile_Missile>();
}
