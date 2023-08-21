using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public void Init(Vector3 _spawnPos, Quaternion _spawnRot, Vector3 _targetPos,
        MissileStateDelegate _missileStateCallback ,
        voidListPoolingObjectDelegate _hitEnemyListCallback = null)
    {
        transform.position = _spawnPos;
        transform.rotation = _spawnRot;
        targetPos = _targetPos;

        targetPos.y = transform.position.y;
        moveDir = (targetPos - transform.position).normalized;

        missileStateCallback = _missileStateCallback;
        hitEnemyListCallback = _hitEnemyListCallback;

        isInit = true;
        gameObject.SetActive(true);
    }

    public void ExplosionFinish()
    {
        gameObject.SetActive(false);
    }

    public void SetNumber(int _number)
    {
        number = _number;
    }

    public int GetNumber()
    {
        return number;
    }

    private void Awake()
    {
        explosion = GetComponentInChildren<Explosion>();
    }

    private void OnEnable()
    {
        missileStateCallback?.Invoke(number, false);
    }

    private void OnDisable()
    {
        missileStateCallback?.Invoke(number, true);
        explosion.Init();
    }

    private void Update()
    {
        if (!GameManager.IsPlaying() || !isInit) return;

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            explosion.Activate(ExplosionFinish, hitEnemyListCallback);
            isInit = false;
        }
    }


    private Vector3 targetPos = Vector3.zero;
    private Vector3 moveDir = Vector3.zero;

    [SerializeField, Range(1f, 30f)]
    private float moveSpeed = 10f;

    private int number = 0;

    private bool isInit = false;

    private Explosion explosion = null;

    private MissileStateDelegate missileStateCallback = null;
    private voidListPoolingObjectDelegate hitEnemyListCallback = null;
}