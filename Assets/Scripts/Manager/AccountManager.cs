using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountManager : MonoBehaviour
{
    public string CurId => curId_string;
    public string CurPw => curPw_string;


    public void UpdateCurAccount(string _id, string _pw)
    {
        curId_string = _id;
        curPw_string = _pw;
    }


    private string curId_string = null;
    private string curPw_string = null;
}
