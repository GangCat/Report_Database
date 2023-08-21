using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HUD_HP : MonoBehaviour
{
    //private void Start()
    //{
    //    Init(3);
    //}

    public void UpdateHp(int _curHp)
    {
        // HP가 몇개 남았는지 알 필요는 없음
        if (_curHp >= listHeart.Count) return;
        listHeart[_curHp].Empty();
    }

    public void Init(int _maxHp)
    {
        if (_maxHp < 1) _maxHp = 1;

        for (int i = 0; i < _maxHp; ++i)
        {
            GameObject heartGo = Instantiate(heartPrefab, heartGroupTr);
            UI_HUD_HP_Heart heart = heartGo.GetComponent<UI_HUD_HP_Heart>();

            heart.SetLocalPosition(new Vector3(i * heartOffset, 0f, 0f));
            listHeart.Add(heart);
        }
    }

    public void FullHp()
    {
        foreach (UI_HUD_HP_Heart heart in listHeart)
        {
            heart.Fill();
            heart.ResetScale();
        }
    }


    [SerializeField]
    private RectTransform heartGroupTr = null;
    [SerializeField]
    private GameObject heartPrefab = null;

    private float heartOffset = 60f;

    private List<UI_HUD_HP_Heart> listHeart =
        new List<UI_HUD_HP_Heart>();
}