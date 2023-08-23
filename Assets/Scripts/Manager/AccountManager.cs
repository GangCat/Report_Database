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
                    // �α��� ����
                    curId_str = _id;
                    alertCallback?.Invoke("Login Success.");
                    loginCallback?.Invoke();
                }
                else if (resultCode.Equals("-1"))
                {
                    // ��й�ȣ �ٸ�
                    alertCallback?.Invoke("Wrong Password.");
                }
                else if (resultCode.Equals("-2"))
                {
                    // ���̵� �������� ����
                    alertCallback?.Invoke("ID Not Found.");
                }
                else if (resultCode.Equals("-10"))
                {
                    // ������ ������ ������.
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
        form.AddField("ID", _id); // �ش� Ű ���� ���޹��� _id�� ����� ����
        form.AddField("PW", _pw); // �ش� Ű ���� ���޹��� _pw�� ����� ����

        using (UnityWebRequest www = UnityWebRequest.Post(signupUri, form))
        {
            yield return www.SendWebRequest(); // ������ ��� ���������� ���


            if (www.result == UnityWebRequest.Result.Success)
            {
                string resultCode = www.downloadHandler.text;
                if (resultCode.Equals("1"))
                {
                    // ȸ������ ����
                    alertCallback?.Invoke("Signup Success.");
                }
                else if (resultCode.Equals("-1"))
                {
                    // �̹� ������ ���̵� ������
                    alertCallback?.Invoke("Already Has Same ID Exist.");
                }
                else if (resultCode.Equals("-10"))
                {
                    // ������ ������ ������
                    alertCallback?.Invoke("Server Disconnected.");
                }
            }
        }
    }

    private bool CheckPw(string _pw)
    {
        if (_pw == null) return false;

        // ����1�̻�, ����1�̻�, Ư������1�̻�
        // 4~12��
        Regex regexPass = new Regex(@"^(?=.*?[a-zA-Z])(?=.*?[0-9])(?=.*?[!@#$%^&*()]).{4,12}$", RegexOptions.IgnorePatternWhitespace);
        return regexPass.IsMatch(_pw);
    }

    private bool CheckId(string _id)
    {
        if (_id == null) return false;

        if (BadWordFilter.Filter(_id)) return false;

        // ���� Ȥ�� ����1�̻�, Ư����ȣ�� ���� �ȵ�.
        // 6~20��
        Regex regexPass = new Regex(@"^(?=.*?[0-9a-zA-Z])(?!.*?[#?!@$%^&*-]).{6,20}$", RegexOptions.IgnorePatternWhitespace);
        return regexPass.IsMatch(_id);
    }



    private readonly string signupUri = "http://127.0.0.1/Signup.php";
    private readonly string loginUri = "http://127.0.0.1/Login.php";

    private string curId_str = null;

    private VoidVoidDelegate loginCallback = null;
    private VoidStrDelegate alertCallback = null;
}
