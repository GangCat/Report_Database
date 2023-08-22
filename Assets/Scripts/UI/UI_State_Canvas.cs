using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_State_Canvas : MonoBehaviour
{
    public enum EState { None = -1, Ready, Start, GameOver, Rank }

    public void Init(
    VoidVoidDelegate _retryBtnCallback,
    VoidVoidDelegate _rankBtnCallback,
    VoidStrStrDelegate _orderButtonCallback)
    {
        gameOver.Init(_retryBtnCallback, _rankBtnCallback);
        rank.Init(_retryBtnCallback, _orderButtonCallback);            
    }

    public void SetActive(bool _active)
    {
        gameObject.SetActive(_active);
    }

    // 나중에 해당 타이밍에 효과음을 넣거나 등등 할 수 있기 때문에 미리 만들어둠.
    public void OnReady()
    {
        UpdateVIsible(EState.Ready);
    }

    public void OnStart()
    {
        UpdateVIsible(EState.Start);
    }

    public void OnGameOver(int _killCnt, int _time, int _score)
    {
        gameOver.SetInfo(_killCnt, _time, _score);
        UpdateVIsible(EState.GameOver);
    }

    public void OnRank(List<SDataScore> _listDataScore)
    {
        uiRankScrollView.AddRankRecord(_listDataScore);

        UpdateVIsible(EState.Rank);
    }

    private void UpdateVIsible(EState _state)
    {
        readyGo.SetActive(false);
        startGo.SetActive(false);
        gameOver.SetActive(false);
        rank.SetActive(false);

        switch (_state)
        {
            case EState.Ready:
                readyGo.SetActive(true);
                break;
            case EState.Start:
                startGo.SetActive(true);
                break;
            case EState.GameOver:
                gameOver.SetActive(true);
                break;
            case EState.Rank:
                rank.SetActive(true);
                break;
        }
    }

    [SerializeField]
    private GameObject readyGo = null;
    [SerializeField]
    private GameObject startGo = null;
    [SerializeField]
    private UI_State_GameOver gameOver = null;
    [SerializeField]
    private UI_State_Rank rank = null;
    [SerializeField]
    private UI_State_Rank_ScrollView uiRankScrollView = null;
}
