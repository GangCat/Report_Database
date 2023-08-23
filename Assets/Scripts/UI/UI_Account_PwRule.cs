using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Account_PwRule : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        pwRuleTooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pwRuleTooltip.SetActive(false);
    }

    private void Start()
    {
        pwRuleTooltip.SetActive(false);
    }

    [SerializeField]
    private GameObject pwRuleTooltip = null;
}
