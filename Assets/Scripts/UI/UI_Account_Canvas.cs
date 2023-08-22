using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UI_Account_Canvas : MonoBehaviour
{
    public void SetActive(bool _isActive)
    {
        gameObject.SetActive(_isActive);
    }

    public void Init(
        VoidStrStrDelegate _onSignupCallback,
        VoidStrStrDelegate _onLoginCallback)
    {
        uiAccount.Init(_onSignupCallback, _onLoginCallback);
    }

    public void ShowAlert(string _alert)
    {
        alertText.ShowAlert(_alert);
    }


    [SerializeField]
    private UI_Account uiAccount = null;
    [SerializeField]
    private UI_Account_AlertText alertText = null;

    private VoidStrStrDelegate loginCallback = null;
}