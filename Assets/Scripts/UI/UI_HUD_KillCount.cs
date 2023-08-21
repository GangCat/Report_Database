using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_HUD_KillCount : MonoBehaviour
{
    public void SetKillCount(int _cnt)
    {
        hundNumImage.sprite = spriteNumbers[_cnt / 100];
        _cnt = _cnt % 100;

        tenNumImage.sprite = spriteNumbers[_cnt / 10];
        _cnt = _cnt % 10;

        oneNumImage.sprite = spriteNumbers[_cnt];
    }

    [SerializeField]
    private Image hundNumImage = null;
    [SerializeField]
    private Image tenNumImage = null;
    [SerializeField]
    private Image oneNumImage = null;
    [SerializeField]
    private Sprite[] spriteNumbers = null;
}
