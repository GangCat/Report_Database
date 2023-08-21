using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIAccount : MonoBehaviour
{
    public enum EInputType { ID, PW }
    public delegate void OnClickSignUpDelegate(string _id, string _pw);
    public delegate void OnClickLoginDelegate(string _id, string _pw);

    public OnClickSignUpDelegate OnClickSignUpCallback
    {
        set { onClickSignUpCallback = value; }
    }

    public OnClickLoginDelegate OnClickLoginCallback
    {
        set { onClickLoginCallback = value; }
    }

    public void OnChangedIdText(string _id)
    {
        // 한 글자 추가될때마다 갱신
        id = _id;
    }

    public void Init(OnClickSignUpDelegate _onClickSignUpCallback, OnClickLoginDelegate _onClickLoginCallback)
    {
        TMP_InputField[] inputFields = GetComponentsInChildren<TMP_InputField>();

        dicIF.Add("ID", inputFields[0]);
        dicIF.Add("PW", inputFields[1]);

        Button btnSignUp = GetComponentInChildren<ButtonSignUp>().transform.GetComponent<Button>();
        btnSignUp.onClick.AddListener(
            () =>
            {
                _onClickSignUpCallback?.Invoke(dicIF["ID"].text, dicIF["PW"].text);
            }
            );

        Button btnSignIn = GetComponentInChildren<ButtonSignIn>().transform.GetComponent<Button>();
        btnSignIn.onClick.AddListener(
            () =>
            {
                _onClickLoginCallback?.Invoke(dicIF["ID"].text, dicIF["PW"].text);
            }
            );
    }



    private Dictionary<string, TMP_InputField> dicIF = new Dictionary<string, TMP_InputField>();

    private string id = null;
    private OnClickSignUpDelegate onClickSignUpCallback = null;
    private OnClickLoginDelegate onClickLoginCallback = null;
}