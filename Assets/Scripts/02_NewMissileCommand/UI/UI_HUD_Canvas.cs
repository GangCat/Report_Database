using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HUD_Canvas : MonoBehaviour
{
    public void SetActive(bool _active)
    {
        gameObject.SetActive(_active);
    }
    #region InitUI
    public void InitHp(int _maxHp)
    {
        uiHudHp.Init(_maxHp);
    }

    public void InitMissile(int _maxMissileCnt)
    {
        uiHudMissile.Init(_maxMissileCnt);
    }

    public void InitKillCount()
    {
        uiHudKillCount.SetKillCount(0);
    }
    #endregion

    #region UpdateUI
    public void UpdateHp(int _curHp)
    {
        uiHudHp.UpdateHp(_curHp);
    }

    public void UpdateMissile(int _index, bool _isFill)
    {
        uiHudMissile.UpdateMissileStateWithIndex(_index, _isFill);
    }

    public void UpdateKillCount(int _count)
    {
        uiHudKillCount.SetKillCount(_count);
    }

    public void UpdateTimer(int _sec)
    {
        uiHudTimer.SetTime(_sec);
    }
    #endregion

    public void FullHp()
    {
        uiHudHp.FullHp();
    }

    public void ReloadMissile()
    {
        uiHudMissile.ReloadAll();
    }


    [SerializeField]
    private UI_HUD_HP uiHudHp = null;
    [SerializeField]
    private UI_HUD_Missile uiHudMissile = null;
    [SerializeField]
    private UI_HUD_KillCount uiHudKillCount = null;
    [SerializeField]
    private UI_HUD_Timer uiHudTimer = null;
}
