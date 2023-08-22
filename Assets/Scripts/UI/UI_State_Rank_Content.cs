using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_State_Rank_Content : MonoBehaviour
{
    public void ClearContent()
    {
        RectTransform[] arrayRankRecordRt = GetComponentsInChildren<RectTransform>();

        if (arrayRankRecordRt.Length < 1) return;

        foreach (RectTransform rect in arrayRankRecordRt)
        {
            if (rect.Equals(GetComponent<RectTransform>())) continue;

            Destroy(rect.gameObject);
        }
    }

    public RectTransform GetRectTransform()
    {
        return GetComponent<RectTransform>();
    }
}
