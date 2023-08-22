using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_State_Rank : MonoBehaviour
{
    public void SetActive(bool _isActive)
    {
        gameObject.SetActive(_isActive);
    }

    public void ShowRanking(List<SDataScore> _listDataScore)
    {
        uiRankScrollView.AddRankRecord(_listDataScore);

        SetActive(true);
    }


    [SerializeField]
    private UI_State_Rank_ScrollView uiRankScrollView = null;
}
