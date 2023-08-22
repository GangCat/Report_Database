using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_State_Rank_ScrollView : MonoBehaviour
{
    public void AddRankRecord(List<SDataScore> _listDataScore)
    {
        content.ClearContent();

        int maxRecord = 10;

        if (_listDataScore.Count < 10)
            maxRecord = _listDataScore.Count;

        for (int i = 0; i < maxRecord; ++i)
        {
            GameObject go = Instantiate(rankRecordPrefab);
            go.GetComponent<UI_State_Rank_Record>().UpdateRecord(_listDataScore[i], i + 1);
            go.transform.SetParent(content.GetRectTransform());
        }
    }

    [SerializeField]
    private GameObject rankRecordPrefab = null;
    [SerializeField]
    private UI_State_Rank_Content content = null;
}
