using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_State_Rank_ScrollView : MonoBehaviour
{
    public void AddRankRecord(List<SDataScore> _listDataScore)
    {
        for (int i = 0; i < _listDataScore.Count; ++i)
        {
            GameObject go = Instantiate(rankRecordPrefab);
            go.GetComponent<UI_State_Rank_Record>().UpdateRecord(_listDataScore[i]);
            go.transform.SetParent(content.GetRectTransform());
        }
    }

    public void ResetContent()
    {
        content.ResetContent();
    }

    [SerializeField]
    private GameObject rankRecordPrefab = null;
    [SerializeField]
    private UI_State_Rank_Content content = null;
}
