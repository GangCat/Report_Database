using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.Application;

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
        WWWForm form = new WWWForm();
        form.AddField("ID", _id); // �ش� Ű ���� ���޹��� _id�� ����� ����
        form.AddField("PW", _pw); // �ش� Ű ���� ���޹��� _pw�� ����� ����

        using (UnityWebRequest www = UnityWebRequest.Post(signupUri, form))
        {
            yield return www.SendWebRequest(); // ������ ��� ���������� ���
            Debug.Log(www.result);


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


    private readonly string signupUri = "http://127.0.0.1/Signup.php";
    private readonly string loginUri = "http://127.0.0.1/Login.php";

    private string curId_str = null;

    private VoidVoidDelegate loginCallback = null;
    private VoidStrDelegate alertCallback = null;
}
