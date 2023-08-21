using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����Ƽ���� ��Ʈ��ũ�� ���õ� ���𰡸� ó���ϱ� ���� �ʿ���
using UnityEngine.Networking;

public class Login2 : MonoBehaviour
{
    // �츮 php�� �ִ� ������ ���, �ٸ� ��ǻ�͸� �� ��ǻ���� ip�� ������
    private readonly string loginUri = "http://127.0.0.1/login.php";

    private void Start()
    {
        StartCoroutine(LoginCoroutine("windy9612", "dbwowns5"));
        // �̰Ŵ� ���Ƿ� �����Ű� �����δ� �Է�â���� �Է¹޾Ƽ� ������ �־������

        //Debug.Log(System.DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"));
        //Debug.Log(System.DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"));
        StartCoroutine(LoginCoroutine("windy9612", "dbwowns"));
        StartCoroutine(LoginCoroutine("windy9612", "dbwowns2"));
        StartCoroutine(LoginCoroutine("windy9312", "dbwowns5"));
    }

    private IEnumerator LoginCoroutine(string _username, string _password)
    {
        // ����Ƽ���� �������� �̷��� ������ ���� �����ش�.
        WWWForm form = new WWWForm();
        form.AddField("loginUser", _username); // Ű, ����
        form.AddField("loginPass", _password); // �̷��� �ִ� �ڷᱸ�� C+���� ��, C#���� ��ųʸ�
        // �츮�� �� ���� �ִ� ���̽��� ��ųʸ� ���̽���� �ǹ�

        // ���⼭ using�� ���ӽ����̽��� ���°� �ƴ϶� using�Լ���� �Ҹ��� �༮��.
        // using ������ () {}�� �� ()�ȿ��� ������� ������ {}�� ����� �ڵ����� �����ǰ� ���ش�.
        // �츮�� ��Ʈ��ũ�� ���ؼ� ������ �ϴ� ��� 3����
        // �� db�� ����ϴ°�
        // ���� �ٿ���� �� ������ �ް� ���ҽ����� �� ���� �� ���
        // ������ ����� ��
        // ���� ����� ������ ������ ������ �ڷ�ƾ�̶��ص� ���۹���� �ٸ���. ���� ����Ǿ������ �ָ��ϴ�.
        // �׷��� ������ �̷��� ����Ѵ�.

        // Post�� ���� -> ������ ������, ��Ŷ�� ������.
        // loginUri�� �� form�� �����ڴ�.(�ӷα��� ����, ����� �н�����)
        // ������ �����µ� db�� �ٷ� �������� php�� ����Ű�
        // �� php���� ������ �����µ� �� php �� ������ �־�� �Ѵ�.
        
        using(UnityWebRequest www = UnityWebRequest.Post(loginUri, form))
        {
            // �갡 �� �����ߴ����� ��ٷ��� �Ѵ�.
            // �׷��� ���⼭ �޾��������� ��ٷ����Ѵ�.
            // �̰Ŷ����� �ڷ�ƾ�� ����.

            // ������ ������ ����� �� �Ȱ����� ����Ƽ���� ���� �����ϴ� ����� WebRequest�̰� 
            // ������ �����ߴ����� Ȯ���ϴ� ����� �̷� �ڷ�ƾ ����ϴ� ����� ���̴�.
            yield return www.SendWebRequest();

            // ������ iserror���� �� �ߴµ� ���� �̷� ������� enum�� ���Ѵ�.
            //if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            //    Debug.Log(www.error);
            //else
            //    Debug.Log(www.downloadHandler.text);

            // ���� ����
            if(www.result == UnityWebRequest.Result.Success)
                // ���ӿ� ���������� �̰� �����ش�.
                // php�� �о���� ������ ���� text�� �����Ѵ�.
                // �츮�� ���� ���̵�� ��й�ȣ�� ������ ����� ���� �ƴϸ� �Ʒ��� ����.
                Debug.Log(www.downloadHandler.text);
            else
                // �̷��� �ϸ� ���� ������ ������ �߻��ߴ��� �˾Ƽ� ����ش�.
                Debug.Log(www.error);
        }
    }
}