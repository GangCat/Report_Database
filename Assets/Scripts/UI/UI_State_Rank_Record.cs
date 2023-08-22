using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_State_Rank_Record : MonoBehaviour
{
    public void UpdateRecord(SDataScore _dataScore)
    {
        rankId.UpdateText(_dataScore.id);
        rankScore.UpdateText(_dataScore.score.ToString("N0"));
        rankKillCnt.UpdateText(_dataScore.killCnt.ToString());
        rankTimeSec.UpdateText(_dataScore.timeSec.ToString());
    }

    [SerializeField]
    private UI_State_Rank_Id rankId = null;
    [SerializeField]
    private UI_State_Rank_Score rankScore = null;
    [SerializeField]
    private UI_State_Rank_KillCnt rankKillCnt = null;
    [SerializeField]
    private UI_State_Rank_TimeSec rankTimeSec = null;
}
