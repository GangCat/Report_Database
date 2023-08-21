using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_HUD_Timer : MonoBehaviour
{
    public void SetTime(int _sec)
    {
        int min = _sec / 60;
        int sec = _sec % 60;

        min_OneNumImage.sprite = spriteNumbers[min % 10];
        min_TenNumImage.sprite = spriteNumbers[min / 10];

        sec_OneNumImage.sprite = spriteNumbers[sec % 10];
        sec_TenNumImage.sprite = spriteNumbers[sec / 10];
    }

    [SerializeField]
    private Image min_OneNumImage = null;
    [SerializeField]
    private Image min_TenNumImage = null;
    [SerializeField]
    private Image sec_OneNumImage = null;
    [SerializeField]
    private Image sec_TenNumImage = null;
    [SerializeField]
    private Sprite[] spriteNumbers = null;
}
