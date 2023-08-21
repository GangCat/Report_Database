using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlertText : MonoBehaviour
{
    public void AlertMessage(string _message)
    {
        text.text = _message;
    }

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private TextMeshProUGUI text = null;
}
