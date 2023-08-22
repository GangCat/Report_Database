using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Account : MonoBehaviour
{
    public enum EInputType { ID, PW }

    public void Init(VoidStrStrDelegate _onClickLoginCallback, VoidStrStrDelegate _onClickSignUpCallback)
    {
        TMP_InputField[] inputFields = GetComponentsInChildren<TMP_InputField>();

        dicIF.Add("ID", inputFields[0]);
        dicIF.Add("PW", inputFields[1]);

        Button btnLogIn = GetComponentInChildren<UI_Account_LoginButton>().transform.GetComponent<Button>();
        btnLogIn.onClick.AddListener(
            () =>
            {
                _onClickLoginCallback?.Invoke(dicIF["ID"].text, dicIF["PW"].text);
            }
            );

        Button btnSignUp = GetComponentInChildren<UI_Account_SignupButton>().transform.GetComponent<Button>();
        btnSignUp.onClick.AddListener(
            () =>
            {
                _onClickSignUpCallback?.Invoke(dicIF["ID"].text, dicIF["PW"].text);
            }
            );
    }

    private Dictionary<string, TMP_InputField> dicIF = new Dictionary<string, TMP_InputField>();
}