using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Account_AlertText : MonoBehaviour
{
    public void ShowAlert(string _message)
    {
        text.text = _message;
    }

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private TextMeshProUGUI text = null;
}
