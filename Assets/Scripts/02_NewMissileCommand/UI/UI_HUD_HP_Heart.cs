using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HUD_HP_Heart : MonoBehaviour
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
        StartCoroutine("EmptyAnimCoroutine");
    }

    public void ResetScale()
    {
        fillGo.transform.localScale = Vector3.one;
    }

    private IEnumerator EmptyAnimCoroutine()
    {
        while (fillGo.transform.localScale.x > 0.01f)
        {
            fillGo.transform.localScale = Vector3.Lerp(fillGo.transform.localScale, Vector3.zero, 0.05f);

            yield return null;
        }

        fillGo.SetActive(false);
        fillGo.transform.localScale = Vector3.one;
    }

    [SerializeField]
    private GameObject fillGo = null;
}
