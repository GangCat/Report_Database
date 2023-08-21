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
        form.AddField("ID", _id); // �ش� Ű ���� ���޹��� _id�� ����� ����
        form.AddField("PW", _pw); // �ش� Ű ���� ���޹��� _pw�� ����� ����

        using (UnityWebRequest www = UnityWebRequest.Post(signupUri, form))
        {
            yield return www.SendWebRequest(); // ������ ��� ���������� ���
            Debug.Log(www.result);


            if (www.result == UnityWebRequest.Result.Success)
            {
                string resultCode = www.downloadHandler.text;
                if (resultCode.Equals("-1"))
                {
                    // �̹� ������ ���̵� ������
                    alertText.AlertMessage("Already same ID exist.");
                }
                else if (resultCode.Equals("1"))
                {
                    // ȸ������ ����
                    alertText.AlertMessage("Sign up Success.");
                }
                else if (resultCode.Equals("-10"))
                {
                    // ������ ������ ������
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
                    // �α��� ����
                    loginCallback?.Invoke(_id, _pw);
                    alertText.AlertMessage("Login success.");
                }
                else if (resultCode.Equals("-1"))
                {
                    // ��й�ȣ �ٸ�
                    alertText.AlertMessage("Wrong password.");
                }
                else if (resultCode.Equals("-2"))
                {
                    // ���̵� �������� ����
                    alertText.AlertMessage("ID not found.");
                }
                else if (resultCode.Equals("-10"))
                {
                    // ������ ������ ������.
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