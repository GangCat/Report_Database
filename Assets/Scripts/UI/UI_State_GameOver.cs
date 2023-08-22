using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_State_GameOver : MonoBehaviour
{
    public void SetActive(bool _isActive)
    {
        gameObject.SetActive(_isActive);
    }

    public void Init(
        VoidVoidDelegate _retryBtnCallback,
        VoidVoidDelegate _rankBtnCallback)
    {
        btnRetry.onClick.AddListener(
        () =>
        {
            _retryBtnCallback?.Invoke();
        }
        );

        btnRank.onClick.AddListener(
        () =>
        {
            _rankBtnCallback?.Invoke();
        }
        );
    }

    public void SetInfo(int _killCnt, int _time, int _score)
    {
        int min = _time / 60;
        int sec = _time % 60;

        textScore.text = string.Format("{0}\n{1:D2}:{2:D2}\n{3:N0}", _killCnt, min, sec, _score);
    }

    [SerializeField]
    private TMP_Text textScore = null;
    [SerializeField]
    private Button btnRetry = null;
    [SerializeField]
    private Button btnRank = null;


}