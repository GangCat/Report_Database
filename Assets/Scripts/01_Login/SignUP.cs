using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SignUP : MonoBehaviour
{
    public void OnClickSignUp(string _id, string _pw)
    {
        StartCoroutine(SignUpCoroutine(_id, _pw)); // signup버튼 클릭시 코루틴 실행
    }

    private void Awake()
    {
        uiLogin.OnClickSignUpCallback = OnClickSignUp; // 우선 delegate에 함수 등록
    }

    private IEnumerator SignUpCoroutine(string _id, string _pw)
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _id); // 해당 키 값에 전달받은 _id를 밸류로 저장
        form.AddField("PW", _pw); // 해당 키 값에 전달받은 _pw를 밸류로 저장

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest(); // 정보를 모두 보낼때까지 대기

            if (www.result == UnityWebRequest.Result.Success)
            {
                string resultCode = www.downloadHandler.text; // 반환받은 text를 저장
                if (resultCode.Equals("-1")) // 해당 text의 상황에 맞는 안내문구 출력
                {
                    // 이미 동일한 아이디가 존재함
                    alertText.AlertMessage("Already same ID exist.");
                }
                else if(resultCode.Equals("1"))
                {
                    // 회원가입 성공
                    alertText.AlertMessage("Sign up Success.");
                }
                else if (resultCode.Equals("-10"))
                {
                    // 서버와 연결이 끊어짐
                    alertText.AlertMessage("Server disconnected.");
                }
            }
        }
    }


    [SerializeField]
    private UIAccount uiLogin = null;
    [SerializeField]
    private AlertText alertText;

    private readonly string uri = "http://127.0.0.1/SignUp.php";

}