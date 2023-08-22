using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIOrderButtonBase : MonoBehaviour
{
    [System.Serializable]
    protected enum EOrderStandard { None = -1, score, kill_cnt, time_sec }
    public void Init(VoidStrStrDelegate _orderCallback)
    {
        GetComponent<Button>().onClick.AddListener(
            () =>
            {
                string orderType = isOrderTypeASC ? "ASC" : "DESC";
                _orderCallback?.Invoke(orderStandard.ToString(), orderType);
                isOrderTypeASC = !isOrderTypeASC;
            }
            );
    }

    [SerializeField]
    protected EOrderStandard orderStandard = EOrderStandard.None;

    protected bool isOrderTypeASC = false;
}
