using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;
using Newtonsoft.Json;

// �ڷ����� �ٸ� �͵��� db�� �����Ҷ��� ���ڿ��� �����.
// �����ͼ� ������ �׿� �´� �ڷ������� �����;���.
// �׷��� ��ȯ���ִ°� json���� �Ұ���.
// newtonsoft�� ���� ���� json�� ����
// json�� ����
// ����Ƽ �������� ��Ű���� ������� json�� �Ƹ� newtonsoft�ϰ���.
// ��� ��Ű���� ����Ƽ�� �������� ���ԵǾ�����.

// json�� �츮�� ��Ʈ��ũ�� ������ ���� ������� ��ȯ�� ������.
// �ٵ� �ڷ����� �ϳ��ϳ��� ��ȯ���ִ°� ������
// �׷��� �Ʒ�ó�� ����ü�� ������� int string ���·� �����ϰ� ��ȯ�ϵ��� ��.

public class DB_Score : MonoBehaviour
{
    // json�� ���� �а� ���ؾ��ؼ� �̷��� ����
    public class DataScore
    {
        public string id { get; set; }
        public int score { get; set; }
    }


    private void Start()
    {
        // ������ �߰��ϴ� �ڷ�ƾ
        StartCoroutine(AddScoreCoroutine("yj3", 5));
        // ������ �������� �ڷ�ƾ
        StartCoroutine(GetScoreCoroutine());

        // �̰Ŵ� ����� �ε带 �����ϴ� ���, �ϳ��� �Լ��� ó���� ���� ����.
    }

    private IEnumerator AddScoreCoroutine(string _id, int _score/*�ٸ� �߰��Ҹ��� ������ ������ �߰��ص� ��. ų�� or �ɸ� �ð� ���*/)
        // ��� ���ӵ��� ���� �ɼǿ� ������ �ɼ� ������ �÷���Ÿ��, ���� ��, ų �� ���� ��ϵǾ��ִ� ��찡 �����ϴµ� �װ� ���� ������ �߰��Ǹ� 
        // ���� �������� �ʾƵ� ������ �����ϰ� ����ϴ� ���
    {
        // ������ ���¸� �����ϴ� ���� ������ ������� ������.
        // ������ �����Ǿ��� �� ������ ������ �״�� ����Ǿ��ִٴ��� ���
        WWWForm form = new WWWForm();
        form.AddField("id", _id);
        form.AddField("score", _score);
        // post, ���޹޴� ��Ŀ��� "id" "score"�� Ű������ ���� ���� �����ϱ� ���� ����.

        using (UnityWebRequest www =
            UnityWebRequest.Post("http://127.0.0.1/addscore.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
                Debug.Log("AddScore Success : " + _id + "(" + _score + ")");
        }
    }

    private IEnumerator GetScoreCoroutine()
    {
        using (UnityWebRequest www =
            UnityWebRequest.Post("http://127.0.0.1/getscore.php", ""))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // �⺻������ �����ͺ��̽����� ������ ��ȸ�ؿ��� �ٹٲ޾��� �޿��� ��������.
                // �̰Ÿ� �츮�� ����� �� �ִ� �����ͱ����� ��ȯ�������.
                // �����͸� ����ȭ�� ���� �ٽ� �������Ѿ���
                // �̰� �� ����ȭ��� ��. deserialize
                // ��� �츮�� �̹� ������ü�� ��� �����Ǿ��ִ��� �˱⶧���� �������� �ڵ带 ¥�� �Ǵµ� 
                // ���� ��� �����ϱ� ����
                // �׷��� ���ϰ� �ϰ� ���ִ� ������ �ϴ°� json�̴�.
                Debug.Log(www.downloadHandler.text);
                string data = www.downloadHandler.text;

                List<DataScore> dataScores =
                   JsonConvert.DeserializeObject<List<DataScore>>(data);

                foreach (DataScore dataScore in dataScores)
                {
                    Debug.Log(dataScore.id + " : " + dataScore.score);

                    svScore.AddContentScore(dataScore.id, dataScore.score);
                }
            }
        }
    }


    [SerializeField]
    private ScrollViewScore svScore = null;
}