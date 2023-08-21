using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    public void OnClickLogin(string _id, string _pw)
    {
        StartCoroutine(LoginCoroutine(_id, _pw));
    }

    private void Awake()
    {
        uiLogin.OnClickLoginCallback = OnClickLogin;
    }


    private IEnumerator LoginCoroutine(string _id, string _pw)
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _id);
        form.AddField("PW", _pw);

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string result = www.downloadHandler.text;
                if (result.Equals("1"))
                {
                    // �α��� ����
                    alertText.AlertMessage("Login success.");
                }
                else if (result.Equals("-1"))
                {
                    // ��й�ȣ �ٸ�
                    alertText.AlertMessage("Wrong password.");
                }
                else if (result.Equals("-2"))
                {
                    // ���̵� �������� ����
                    alertText.AlertMessage("ID not found.");
                }
                else if (result.Equals("-10"))
                {
                    // ������ ������ ������.
                    alertText.AlertMessage("Server disconnected.");
                }
            }
        }
    }

    [SerializeField]
    private UIAccount uiLogin = null;
    [SerializeField]
    private AlertText alertText;

    private readonly string uri = "http://127.0.0.1/Login.php";
}
