using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContentScore : MonoBehaviour
{
    public void SetIdAndScore(string _id, int _score)
    {
        textId.text = _id;
        textScore.text = _score.ToString("N0");
    }


    [SerializeField]
    private TMP_Text textId = null;
    [SerializeField]
    private TMP_Text textScore = null;
}
