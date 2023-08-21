using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewScore : MonoBehaviour
{
    public void AddContentScore(string _id, int _score)
    {
        GameObject go = Instantiate(contentScorePrefab);
        go.GetComponent<ContentScore>().SetIdAndScore(_id, _score);
        go.transform.SetParent(contentTr);
        go.transform.localPosition = Vector3.zero;
    }

    [SerializeField]
    private GameObject contentScorePrefab = null;
    [SerializeField]
    private RectTransform contentTr = null;
}
