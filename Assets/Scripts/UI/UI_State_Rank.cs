using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_State_Rank : MonoBehaviour
{
    public void SetActive(bool _isActive)
    {
        gameObject.SetActive(_isActive);
    }

    public void Init(VoidVoidDelegate _retryBtnCallback, VoidStrStrDelegate _orderButtonCallback)
    {
        btnRetry.onClick.AddListener(
        () =>
        {
            _retryBtnCallback?.Invoke();
        }
        );

        uiRankColumn.Init(_orderButtonCallback);
    }

    public void ShowRanking(List<SDataScore> _listDataScore)
    {
        //List<GameObject> listData = GetComponentsInChildren<>
        uiRankScrollView.AddRankRecord(_listDataScore);

        SetActive(true);
    }


    [SerializeField]
    private UI_State_Rank_ScrollView uiRankScrollView = null;
    [SerializeField]
    private UI_State_Rank_Column uiRankColumn = null;
    [SerializeField]
    private Button btnRetry = null;
}
