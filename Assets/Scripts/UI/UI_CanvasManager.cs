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

    public void InitUIHUD(int _maxHp, int _maxMissileCnt)
    {
        uiHudCanvas.InitHp(_maxHp);
        uiHudCanvas.InitMissile(_maxMissileCnt);
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


    public void ResetUIHUDHp()
    {
        uiHudCanvas.FullHp();
    }

    public void ResetUIHUDMissile()
    {
        uiHudCanvas.ReloadMissile();
    }

    public void ResetUIHUDKillCount()
    {
        uiHudCanvas.InitKillCount();
    }
    #endregion

    #region State
    public void SetActiveState(bool _active)
    {
        uiStateCanvas.SetActive(_active);
    }

    public void InitUIState(
        VoidVoidDelegate _retryBtnCallback,
        VoidVoidDelegate _rankBtnCallback,
        VoidStrStrDelegate _orderButtonCallback)
    {
        uiStateCanvas.Init(_retryBtnCallback, _rankBtnCallback, _orderButtonCallback);
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

    public void OnRank(List<SDataScore> _listDataScore)
    {
        uiStateCanvas.OnRank(_listDataScore);
    }

    #endregion

    #region Account

    public void SetActiveAccount(bool _isActive)
    {
        uiAccountCanvas.SetActive(_isActive);
    }

    public void InitUIAccount
        (VoidStrStrDelegate _onClickSignupCallback,
        VoidStrStrDelegate _onClickLoginCallback)
    {
        uiAccountCanvas.Init(_onClickSignupCallback, _onClickLoginCallback);
    }

    public void UpdateScore(int _score)
    {

    }

    public void ShowAlert(string _alert)
    {
        uiAccountCanvas.ShowAlert(_alert);
    }


    #endregion

    [SerializeField]
    private UI_HUD_Canvas uiHudCanvas = null;
    [SerializeField]
    private UI_State_Canvas uiStateCanvas = null;
    [SerializeField]
    private UI_Account_Canvas uiAccountCanvas = null;


}
