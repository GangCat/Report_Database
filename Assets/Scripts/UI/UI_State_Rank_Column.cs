using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_State_Rank_Column : MonoBehaviour
{
    public void Init(VoidStrStrDelegate _callback)
    {
        foreach (UIOrderButtonBase button in orderButtons)
            button.Init(_callback);
    }

    [SerializeField]
    private UIOrderButtonBase[] orderButtons = null;
}
