using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HUD_Missile_Missile : MonoBehaviour
{
    public void SetLocalPosition(Vector3 _localPos)
    {
        transform.localPosition = _localPos;
    }

    public void Fill()
    {
        fillGo.SetActive(true);
    }

    public void Empty()
    {
        fillGo.SetActive(false);
    }

    [SerializeField]
    private GameObject fillGo = null;
}
