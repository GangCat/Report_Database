using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

public class AccountManager : MonoBehaviour
{
    public string CurId => curId_str;

    public void Init(
        VoidVoidDelegate _loginCallback,
        VoidStrDelegate _alertCallback)
    {
        loginCallback = _loginCallback;
        alertCallback = _alertCallback;
    }

    public void LoginProcess(string _id, string _pw)
    {
        StartCoroutine(LoginCoroutine(_id, _pw));
    }

    public void SignupProcess(string _id, string _pw)
    {
        StartCoroutine(SignUpCoroutine(_id, _pw));
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
                    curId_str = _id;
                    alertCallback?.Invoke("Login Success.");
                    loginCallback?.Invoke();
                }
                else if (resultCode.Equals("-1"))
                {
                    // 비밀번호 다름
                    alertCallback?.Invoke("Wrong Password.");
                }
                else if (resultCode.Equals("-2"))
                {
                    // 아이디가 존재하지 않음
                    alertCallback?.Invoke("ID Not Found.");
                }
                else if (resultCode.Equals("-10"))
                {
                    // 서버와 연결이 끊어짐.
                    alertCallback?.Invoke("Server Disconnected.");
                }
            }
        }
    }

    private IEnumerator SignUpCoroutine(string _id, string _pw)
    {
        if (!CheckId(_id))
        {
            alertCallback?.Invoke("Please Check The ID Rules Again");
            yield break;
        }

        if (!CheckPw(_pw))
        {
            alertCallback?.Invoke("Please Check The Password Rules Again");
            yield break;
        }


        WWWForm form = new WWWForm();
        form.AddField("ID", _id); // 해당 키 값에 전달받은 _id를 밸류로 저장
        form.AddField("PW", _pw); // 해당 키 값에 전달받은 _pw를 밸류로 저장

        using (UnityWebRequest www = UnityWebRequest.Post(signupUri, form))
        {
            yield return www.SendWebRequest(); // 정보를 모두 보낼때까지 대기


            if (www.result == UnityWebRequest.Result.Success)
            {
                string resultCode = www.downloadHandler.text;
                if (resultCode.Equals("1"))
                {
                    // 회원가입 성공
                    alertCallback?.Invoke("Signup Success.");
                }
                else if (resultCode.Equals("-1"))
                {
                    // 이미 동일한 아이디가 존재함
                    alertCallback?.Invoke("Already Has Same ID Exist.");
                }
                else if (resultCode.Equals("-10"))
                {
                    // 서버와 연결이 끊어짐
                    alertCallback?.Invoke("Server Disconnected.");
                }
            }
        }
    }

    private bool CheckPw(string _pw)
    {
        if (_pw == null) return false;

        // 숫자1이상, 영문1이상, 특수문자1이상
        // 4~12자
        Regex regexPass = new Regex(@"^(?=.*?[a-zA-Z])(?=.*?[0-9])(?=.*?[!@#$%^&*()]).{4,12}$", RegexOptions.IgnorePatternWhitespace);
        return regexPass.IsMatch(_pw);
    }

    private bool CheckId(string _id)
    {
        if (_id == null) return false;

        if (BadWordFilter.Filter(_id)) return false;

        // 숫자 혹은 영문1이상, 특수기호는 들어가면 안됨.
        // 6~20자
        Regex regexPass = new Regex(@"^(?=.*?[0-9a-zA-Z])(?!.*?[#?!@$%^&*-]).{6,20}$", RegexOptions.IgnorePatternWhitespace);
        return regexPass.IsMatch(_id);
    }



    private readonly string signupUri = "http://127.0.0.1/Signup.php";
    private readonly string loginUri = "http://127.0.0.1/Login.php";

    private string curId_str = null;

    private VoidVoidDelegate loginCallback = null;
    private VoidStrDelegate alertCallback = null;
}
