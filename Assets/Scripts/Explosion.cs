using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public delegate void FinishCallback();

    public void Activate(FinishCallback _finishCallback = null, voidListPoolingObjectDelegate _hitEnemyListCallback = null) // default parameter
    {
        if (isActivate) return;

        StartCoroutine(ActivateCoroutine(_finishCallback, _hitEnemyListCallback));
    }

    public void Init()
    {
        transform.localScale = Vector3.zero;
    }

    private Explosion()
    {
        maxScale = 10f;
    }

    private IEnumerator ActivateCoroutine(FinishCallback _finishCallback, voidListPoolingObjectDelegate _hitEnemyListCallback)
    {
        transform.localScale = Vector3.zero;
        float t = 0f;
        Vector3 from = transform.localScale;
        Vector3 to = Vector3.one * maxScale;
        Vector3 velocity = Vector3.one;

        while (t < 1f)
        {
            if (!GameManager.IsPlaying())
                StopAllCoroutines();

            transform.localScale = Vector3.Lerp(from, to, t);
            t += Time.deltaTime * speed;
            yield return null;
        }

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, maxScale * 0.5f, Vector3.forward, 0f);

        if (hits.Length > 0)
        {
            List<IPoolingObject> hitList = new List<IPoolingObject>();
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    hitList.Add(hit.transform.GetComponent<IPoolingObject>());
                }
            }
            _hitEnemyListCallback?.Invoke(hitList);
        }

        _finishCallback?.Invoke();
        
        transform.localScale = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, maxScale * 0.5f);
    }

    [SerializeField, Range(0.1f, 5f)]
    private float speed = 5f;

    private readonly float maxScale;

    private bool isActivate = false;

}
