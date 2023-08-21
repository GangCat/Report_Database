using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;
using Newtonsoft.Json;

// 자료형이 다른 것들을 db에 저장할때는 문자열로 저장됨.
// 가져와서 쓸때는 그에 맞는 자료형으로 가져와야함.
// 그렇게 변환해주는걸 json으로 할거임.
// newtonsoft는 많이 쓰는 json의 종류
// json은 흔함
// 유니티 전용으로 패키지로 만들어진 json이 아마 newtonsoft일것임.
// 얘는 패키지로 유니티에 정식으로 포함되어있음.

// json은 우리가 네트워크로 보내기 좋은 방법으로 변환을 시켜줌.
// 근데 자료형을 하나하나를 변환해주는건 별로임
// 그래서 아래처럼 구조체로 만ㄷ르어서 int string 형태로 저장하고 반환하도록 함.

public class DB_Score : MonoBehaviour
{
    // json이 쓰고 읽고 다해야해서 이렇게 만듬
    public class DataScore
    {
        public string id { get; set; }
        public int score { get; set; }
    }


    private void Start()
    {
        // 점수를 추가하는 코루틴
        StartCoroutine(AddScoreCoroutine("yj3", 5));
        // 점수를 가져오는 코루틴
        StartCoroutine(GetScoreCoroutine());

        // 이거는 저장과 로드를 따로하는 방식, 하나의 함수로 처리할 수도 있음.
    }

    private IEnumerator AddScoreCoroutine(string _id, int _score/*다른 추가할만한 사항이 있으면 추가해도 됨. 킬수 or 걸린 시간 등등*/)
        // 몇몇 게임들을 보면 옵션에 숨겨진 옵션 가보면 플레이타임, 데스 수, 킬 수 등이 기록되어있는 경우가 존재하는데 그게 위의 변수에 추가되며 
        // 굳이 보여지지 않아도 되지만 세세하게 기록하는 경우
    {
        // 게임의 상태를 저장하는 경우는 점수의 저장과는 별개임.
        // 게임이 강종되었을 때 마지막 순간이 그대로 저장되어있다던가 등등
        WWWForm form = new WWWForm();
        form.AddField("id", _id);
        form.AddField("score", _score);
        // post, 전달받는 방식에서 "id" "score"를 키값으로 뒤의 값을 저장하기 위해 던짐.

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
                // 기본적으로 데이터베이스에서 정보를 조회해오면 줄바꿈없이 쭈우우욱 적혀있음.
                // 이거를 우리가 사용할 수 있는 데이터구조로 변환해줘야함.
                // 데이터를 직렬화한 것을 다시 복구시켜야함
                // 이걸 역 직렬화라고 함. deserialize
                // 사실 우리는 이미 구조ㅓ체가 어떻게 구성되어있는지 알기때문에 수동으로 코드를 짜도 되는데 
                // 편한 방법 있으니까 ㅇㅇ
                // 그런걸 편하게 하게 해주는 역할을 하는게 json이다.
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