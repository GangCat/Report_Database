using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 유니티에서 네트워크와 관련된 무언가를 처리하기 위해 필요함
using UnityEngine.Networking;

public class Login2 : MonoBehaviour
{
    // 우리 php가 있는 웹상의 경로, 다른 컴퓨터면 그 컴퓨터의 ip가 들어가야함
    private readonly string loginUri = "http://127.0.0.1/login.php";

    private void Start()
    {
        StartCoroutine(LoginCoroutine("windy9612", "dbwowns5"));
        // 이거는 임의로 넣은거고 실제로는 입력창에서 입력받아서 여따가 넣어줘야함

        //Debug.Log(System.DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"));
        //Debug.Log(System.DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"));
        StartCoroutine(LoginCoroutine("windy9612", "dbwowns"));
        StartCoroutine(LoginCoroutine("windy9612", "dbwowns2"));
        StartCoroutine(LoginCoroutine("windy9312", "dbwowns5"));
    }

    private IEnumerator LoginCoroutine(string _username, string _password)
    {
        // 유니티에서 보낼때는 이렇게 폼으로 만들어서 보내준다.
        WWWForm form = new WWWForm();
        form.AddField("loginUser", _username); // 키, 벨류
        form.AddField("loginPass", _password); // 이렇게 넣는 자료구조 C+에는 맵, C#에는 딕셔너리
        // 우리가 이 값을 넣는 베이스가 딕셔너리 베이스라는 의미

        // 여기서 using은 네임스페이스를 뺴는게 아니라 using함수라고 불리는 녀석임.
        // using 나오고 () {}일 때 ()안에서 만들어진 변수가 {}를 벗어나면 자동으로 정리되게 해준다.
        // 우리가 네트워크를 통해서 뭔가를 하는 경우 3가지
        // 웹 db랑 통신하는거
        // 게임 다운받을 때 스토어에서 받고 리소스파일 더 받을 때 사용
        // 실제로 통신할 때
        // 웹은 통신이 끝나야 끝나기 때문에 코루틴이라해도 동작방식이 다르다. 언제 종료되어야할지 애매하다.
        // 그렇기 때문에 이렇게 사용한다.

        // Post를 보냄 -> 통지를 보낸다, 패킷을 던진다.
        // loginUri에 이 form을 던지겠다.(ㅣ로그인 유저, 롣그인 패스워드)
        // 서버에 보내는데 db에 바로 못보내서 php를 만든거고
        // 그 php에게 정보를 던지는데 그 php 가 서버에 있어야 한다.
        
        using(UnityWebRequest www = UnityWebRequest.Post(loginUri, form))
        {
            // 얘가 잘 도착했는지를 기다려야 한다.
            // 그래서 여기서 받았을때까지 기다려야한다.
            // 이거때문에 코루틴을 쓴다.

            // 정보를 보내는 방식은 다 똑같은데 유니티에서 웹과 소통하는 방법이 WebRequest이고 
            // 정보가 도착했는지를 확인하는 방식이 이런 코루틴 사용하는 방식일 뿐이다.
            yield return www.SendWebRequest();

            // 옛날엔 iserror같이 뭐 했는데 이제 이런 방식으로 enum과 비교한다.
            //if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            //    Debug.Log(www.error);
            //else
            //    Debug.Log(www.downloadHandler.text);

            // 위와 동일
            if(www.result == UnityWebRequest.Result.Success)
                // 접속에 성공했으면 이걸 던져준다.
                // php가 읽어들인 정보를 여기 text에 저장한다.
                // 우리가 던진 아이디와 비밀번호가 있으면 여기로 오고 아니면 아래로 간다.
                Debug.Log(www.downloadHandler.text);
            else
                // 이렇게 하면 무슨 문제로 에러가 발생했는지 알아서 띄워준다.
                Debug.Log(www.error);
        }
    }
}