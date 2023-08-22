using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;



public class RankManager : MonoBehaviour
{
    public void ImportRank(string _id, int _score, int _killCnt, int _timeSec)
    {
        StartCoroutine(PushRankCoroutine(_id, _score, _killCnt, _timeSec));
    }

    public void ExportRank(VoidListDataScoreDelegate _exportCallback)
    {
        StartCoroutine("PullRankCoroutine", _exportCallback);
    }


    public bool IsRecordSucess => isRecordSucess;

    private IEnumerator PushRankCoroutine(string _id, int _score, int _killCnt, int _timeSec)
    {
        isRecordSucess = false;

        WWWForm form = new WWWForm();
        form.AddField("ID", _id);
        form.AddField("SCORE", _score);
        form.AddField("KILL_CNT", _killCnt);
        form.AddField("TIME_SEC", _timeSec);

        using (UnityWebRequest www = UnityWebRequest.Post(pushRankUri, form))
        {
            yield return www.SendWebRequest(); // 정보를 모두 보낼때까지 대기
            Debug.Log(www.result);


            if (www.result == UnityWebRequest.Result.Success)
            {
                string resultCode = www.downloadHandler.text;
                if (resultCode.Equals("-10"))
                {
                    isRecordSucess = false;
                }

                isRecordSucess = true;
            }
        }
    }

    private IEnumerator PullRankCoroutine(VoidListDataScoreDelegate _exportCallback)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(pullRankUri, ""))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ProtocolError)
                Debug.Log(www.error);
            else
            {
                Debug.Log(www.downloadHandler.text);
                string data = www.downloadHandler.text;

                List<SDataScore> listDataScore = JsonConvert.DeserializeObject<List<SDataScore>>(data);

                _exportCallback?.Invoke(listDataScore);


                foreach (SDataScore datas in listDataScore)
                    Debug.Log(datas.id);
            }
        }
    }

    private bool isRecordSucess = false;

    private readonly string pushRankUri = "http://127.0.0.1/PushRank.php";
    private readonly string pullRankUri = "http://127.0.0.1/PullRank.php";
}
