using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stage_GameOver : MonoBehaviour
{
    public void SetActive(bool _isActive)
    {
        gameObject.SetActive(_isActive);
    }

    public void SetInfo(int _killCnt, int _time, int _score)
    {
        //StringBuilder sb = null;
        //sb.AppendLine(_killCnt.ToString());
        //sb.Append(_time.ToString());
        //scoreText.text += sb.ToString();
        //sb.Clear();

        int min = _time / 60;
        int sec = _time % 60;

        // $나 @를 맨 앞에 넣기도 하는데 유니코드를 쓸건지 ASCII코드를 쓸건지 이런거 
        textScore.text = string.Format("{0}\n{1:D2}:{2:D2}\n{3:N0}", _killCnt, min, sec, _score);
    }

    public void SetRetryButtonCallback(VoidVoidDelegate _retryCallback)
    {
        // 장점-> 문제가 발생하지 않음.
        // 단점-> 그 문제가 어디에서 문제가 생기는지 안보임.
        //if (btnRetry == null) return;

        // 인스펙터에 넣어야하는데 안넣어서 발생하는 에러는 차라리 에러 나오게 드는게 문제 찾기도 쉽고 좋더라.
        // 다만 getcomponent같이 동적할당을 하는 과정에서 문제가 생기는지는 직접 예외처리해서 확인해주는게 좋다.

        btnRetry.onClick.AddListener(
            () =>
            {
                _retryCallback?.Invoke();
            }
            );
    }

    public void SetRankButtonCallback(VoidVoidDelegate _rankCallback)
    {
        btnRank.onClick.AddListener(
            () =>
            {
                _rankCallback?.Invoke();
            }
            );
    }

    [SerializeField]
    private TMP_Text textScore = null;
    [SerializeField]
    private Button btnRetry = null;
    [SerializeField]
    private Button btnRank = null;

    
}