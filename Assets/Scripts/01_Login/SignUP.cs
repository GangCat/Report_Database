using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SignUP : MonoBehaviour
{
    public void OnClickSignUp(string _id, string _pw)
    {
        StartCoroutine(SignUpCoroutine(_id, _pw)); // signup��ư Ŭ���� �ڷ�ƾ ����
    }

    private void Awake()
    {
        uiLogin.OnClickSignUpCallback = OnClickSignUp; // �켱 delegate�� �Լ� ���
    }

    private IEnumerator SignUpCoroutine(string _id, string _pw)
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _id); // �ش� Ű ���� ���޹��� _id�� ����� ����
        form.AddField("PW", _pw); // �ش� Ű ���� ���޹��� _pw�� ����� ����

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest(); // ������ ��� ���������� ���

            if (www.result == UnityWebRequest.Result.Success)
            {
                string resultCode = www.downloadHandler.text; // ��ȯ���� text�� ����
                if (resultCode.Equals("-1")) // �ش� text�� ��Ȳ�� �´� �ȳ����� ���
                {
                    // �̹� ������ ���̵� ������
                    alertText.AlertMessage("Already same ID exist.");
                }
                else if(resultCode.Equals("1"))
                {
                    // ȸ������ ����
                    alertText.AlertMessage("Sign up Success.");
                }
                else if (resultCode.Equals("-10"))
                {
                    // ������ ������ ������
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