using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Account_IdRule : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        idRuleTooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        idRuleTooltip.SetActive(false);
    }

    private void Start()
    {
        idRuleTooltip.SetActive(false);
    }

    [SerializeField]
    private GameObject idRuleTooltip = null;
}
