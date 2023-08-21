using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Rank_Canvas : MonoBehaviour
{
    public void SetActive(bool _isActive, int _score = 0, VoidVoidDelegate _enterCallback = null)
    {
        gameObject.SetActive(_isActive);
        uiRankEnterName.SetScore(_score);
        ShowEnterName(_enterCallback);
    }

    public void ShowEnterName(VoidVoidDelegate _enterCallback)
    {
        uiRankEnterName.SetActive(true, _enterCallback);
    }

    public void ShowRanking()
    {
        uiRankEnterName.SetActive(false);
    }




    [SerializeField]
    private UI_Rank_EnterName uiRankEnterName = null;
}
