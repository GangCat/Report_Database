using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CanvasManager : MonoBehaviour
{
    public enum ECanvasType { None = -1, HUD, State, Rank, Account }

    #region HUD
    public void SetActiveHUD(bool _active)
    {
        uiHudCanvas.SetActive(_active);
    }


    public void UIHUDInitHP(int _maxHp)
    {
        uiHudCanvas.InitHp(_maxHp);
    }

    public void UIHUDInitMissile(int _maxMissileCnt)
    {
        uiHudCanvas.InitMissile(_maxMissileCnt);
    }

    public void UIHUDInitKillCount()
    {
        uiHudCanvas.InitKillCount();
    }


    public void UIHUDUpdateHp(int _curHp)
    {
        uiHudCanvas.UpdateHp(_curHp);
    }

    public void UIHUDUpdateMissileStateWithIndex(int _index, bool _isFill)
    {
        uiHudCanvas.UpdateMissile(_index, _isFill);
    }

    public void UIHUDUpdateKillCount(int _count)
    {
        uiHudCanvas.UpdateKillCount(_count);
    }

    public void UIHUDUpdateTimer(int _sec)
    {
        uiHudCanvas.UpdateTimer(_sec);
    }


    public void UIHUDFullHp()
    {
        uiHudCanvas.FullHp();
    }

    public void UIHUDReloadMissile()
    {
        uiHudCanvas.ReloadMissile();
    }
    #endregion

    #region State
    public void SetActiveState(bool _active)
    {
        uiStateCanvas.SetActive(_active);
    }

    public void OnReady()
    {
        uiStateCanvas.OnReady();
    }

    public void OnStart()
    {
        uiStateCanvas.OnStart();
    }

    public void OnGameOver(int _killCnt, int _timeSec, int _score)
    {
        uiStateCanvas.OnGameOver(_killCnt, _timeSec, _score);
    }

    public void SetRetryButtonCallback(VoidVoidDelegate _callback)
    {
        uiStateCanvas.SetRetryButtonCallback(_callback);
    }

    public void SetRankButtonCallback(VoidVoidDelegate _callback)
    {
        uiStateCanvas.SetRankButtonCallback(_callback);
    }
    #endregion

    #region Rank

    public void SetActiveRank(bool _active, int _score = 0, VoidVoidDelegate _enterCallback = null)
    {
        uiRankCanvas.SetActive(_active, _score, _enterCallback);
    }

    #endregion

    #region Account

    public void SetLoginDelegate(VoidVoidDelegate _loginCallback)
    {
        uiAccountCanvas.Init(_loginCallback);
    }


    #endregion

    [SerializeField]
    private UI_HUD_Canvas uiHudCanvas = null;
    [SerializeField]
    private UI_State_Canvas uiStateCanvas = null;
    [SerializeField]
    private UI_Rank_Canvas uiRankCanvas = null;
    [SerializeField]
    private UI_Account_Canvas uiAccountCanvas = null;


}
