using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_State_Rank_Record : MonoBehaviour
{
    public void UpdateRecord(SDataScore _dataScore, int _rank)
    {
        rank.UpdateText(_rank.ToString());
        id.UpdateText(_dataScore.id);
        score.UpdateText(_dataScore.score.ToString("N0"));
        killCnt.UpdateText(_dataScore.killCnt.ToString());
        timeSec.UpdateText(_dataScore.timeSec.ToString());
    }

    [SerializeField]
    private UI_State_Rank_Rank rank = null;
    [SerializeField]
    private UI_State_Rank_Id id = null;
    [SerializeField]
    private UI_State_Rank_Score score = null;
    [SerializeField]
    private UI_State_Rank_KillCnt killCnt = null;
    [SerializeField]
    private UI_State_Rank_TimeSec timeSec = null;
}
