using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.Application;

public class UI_Account_Canvas : MonoBehaviour
{
    public void SetActive(bool _isActive)
    {
        gameObject.SetActive(_isActive);
    }

    public void Init(VoidStrStrDelegate _loginCallback)
    {
        loginCallback = _loginCallback;
        uiAccount.Init(OnClickSignUp, OnClickLogin);
    }

    public void OnClickSignUp(string _id, string _pw)
    {
        StartCoroutine(SignUpCoroutine(_id, _pw));
    }

    public void OnClickLogin(string _id, string _pw)
    {
        StartCoroutine(LoginCoroutine(_id, _pw));
    }


    private IEnumerator SignUpCoroutine(string _id, string _pw)
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _id); // 해당 키 값에 전달받은 _id를 밸류로 저장
        form.AddField("PW", _pw); // 해당 키 값에 전달받은 _pw를 밸류로 저장

        using (UnityWebRequest www = UnityWebRequest.Post(signupUri, form))
        {
            yield return www.SendWebRequest(); // 정보를 모두 보낼때까지 대기
            Debug.Log(www.result);


            if (www.result == UnityWebRequest.Result.Success)
            {
                string resultCode = www.downloadHandler.text;
                if (resultCode.Equals("-1"))
                {
                    // 이미 동일한 아이디가 존재함
                    alertText.AlertMessage("Already same ID exist.");
                }
                else if (resultCode.Equals("1"))
                {
                    // 회원가입 성공
                    alertText.AlertMessage("Sign up Success.");
                }
                else if (resultCode.Equals("-10"))
                {
                    // 서버와 연결이 끊어짐
                    alertText.AlertMessage("Server disconnected.");
                }

                Debug.Log(resultCode);
            }
        }
    }

    private IEnumerator LoginCoroutine(string _id, string _pw)
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _id);
        form.AddField("PW", _pw);

        using (UnityWebRequest www = UnityWebRequest.Post(loginUri, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string resultCode = www.downloadHandler.text;
                if (resultCode.Equals("1"))
                {
                    // 로그인 성공
                    loginCallback?.Invoke(_id, _pw);
                    alertText.AlertMessage("Login success.");
                }
                else if (resultCode.Equals("-1"))
                {
                    // 비밀번호 다름
                    alertText.AlertMessage("Wrong password.");
                }
                else if (resultCode.Equals("-2"))
                {
                    // 아이디가 존재하지 않음
                    alertText.AlertMessage("ID not found.");
                }
                else if (resultCode.Equals("-10"))
                {
                    // 서버와 연결이 끊어짐.
                    alertText.AlertMessage("Server disconnected.");
                }
            }
        }
    }

    [SerializeField]
    private UI_Account uiAccount = null;
    [SerializeField]
    private UI_Account_AlertText alertText = null;

    private VoidStrStrDelegate loginCallback = null;

    private readonly string signupUri = "http://127.0.0.1/Signup.php";
    private readonly string loginUri = "http://127.0.0.1/Login.php";
}