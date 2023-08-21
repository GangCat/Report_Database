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

        // $�� @�� �� �տ� �ֱ⵵ �ϴµ� �����ڵ带 ������ ASCII�ڵ带 ������ �̷��� 
        textScore.text = string.Format("{0}\n{1:D2}:{2:D2}\n{3:N0}", _killCnt, min, sec, _score);
    }

    public void SetRetryButtonCallback(VoidVoidDelegate _retryCallback)
    {
        // ����-> ������ �߻����� ����.
        // ����-> �� ������ ��𿡼� ������ ������� �Ⱥ���.
        //if (btnRetry == null) return;

        // �ν����Ϳ� �־���ϴµ� �ȳ־ �߻��ϴ� ������ ���� ���� ������ ��°� ���� ã�⵵ ���� ������.
        // �ٸ� getcomponent���� �����Ҵ��� �ϴ� �������� ������ ��������� ���� ����ó���ؼ� Ȯ�����ִ°� ����.

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