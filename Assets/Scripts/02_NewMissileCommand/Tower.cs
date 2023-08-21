using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int MaxHp => maxHp;
    public int MaxMissileCount => maxMissileCount;
    public void Attack(Vector3 _targetPos, voidListPoolingObjectDelegate _hitCallback = null)
    {
        if (attackCo != null)
            StopCoroutine(attackCo);

        attackCo = StartCoroutine(AttackCoroutine(_targetPos, _hitCallback));
    }

    public int Damage(int _dmg = 1)
    {
        curHp -= _dmg;
        return curHp;
        // UI를 위해서, 죽었는지 확인을 위해서 반환
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void Awake()
    {
        im = InputMouse.Instance;
        missileSpawnPoint = GetComponentInChildren<MissileSpawnPoint>();
    }

    public void Init(MissileStateDelegate _missileStateCallback)
    {
        for (int i = 0; i < maxMissileCount; ++i)
        {
            GameObject missileGo = Instantiate(missilePrefab);
            missileGo.name = "Missile_" + i.ToString("D2");
            missileGo.SetActive(false);

            Missile missile = missileGo.GetComponent<Missile>();
            missile.SetNumber(i);
            listMissile.Add(missile);
        }
        curHp = maxHp;
        missileStateCallback = _missileStateCallback;
    }

    public void Retry()
    {
        foreach (Missile missile in listMissile)
            missile.gameObject.SetActive(false);

        transform.rotation = Quaternion.identity;

        curHp = maxHp;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StopCoroutine("AttackCoroutine");
    }

    private IEnumerator AttackCoroutine(Vector3 _targetPos, voidListPoolingObjectDelegate _hitCallback)
    {
        float towerAngle = CalcAngleToTarget(transform.position, transform.forward);

        float targetAngle = CalcAngleToTarget(transform.position, _targetPos);

        float difAngle = Mathf.Abs(towerAngle - targetAngle);

        if (difAngle > 180)
        {
            if (targetAngle > 0)
                targetAngle -= 360;
            else
                targetAngle += 360;

            difAngle = 360 - difAngle;
        }

        float s = difAngle / 360f;

        float t = 0f;

        while (t < 1f)
        {
            if (!GameManager.IsPlaying())
                StopAllCoroutines();

            float angle = Mathf.Lerp(towerAngle, targetAngle, t);
            RotateYaw(transform, angle);

            t += (Time.deltaTime * rotateSpeed) / s;
            yield return null;
        }

        RotateYaw(transform, targetAngle);

        Missile missile = GetUsableMissile();
        if (missile != null)
        {
            missile.Init(missileSpawnPoint.GetSpawnPoint(), missileSpawnPoint.GetRotation(), _targetPos, missileStateCallback, _hitCallback);
        }
    }

    public static float CalcAngleToTarget(Vector3 _oriPos, Vector3 _targetPos)
    {
        Vector3 oriPos = _oriPos;
        oriPos.y = 0f;
        Vector3 targetPos = _targetPos;
        targetPos.y = 0f;

        Vector3 dirToTarget = (targetPos - oriPos).normalized;
        return Mathf.Atan2(dirToTarget.z, dirToTarget.x) * Mathf.Rad2Deg;
    }

    public static void SetRotation(Transform _tr, Vector3 _euler)
    {
        _tr.rotation = Quaternion.Euler(_euler);
    }

    public static void RotateYaw(Transform _tr, float _angle)
    {
        _tr.rotation = Quaternion.Euler(0f, -_angle + 90f, 0f);
    }

    //private void PickingSample()
    //{
    //    Vector3 point = Vector3.zero;
    //    if (im.Picking("Stage", ref point))
    //    {
    //        float theta = CalcAngleToTarget(transform.position, point);

    //        transform.rotation = Quaternion.Euler(0f, -theta + 90, 0f);
    //    }
    //}

    private Missile GetUsableMissile()
    {
        foreach (Missile missile in listMissile)
        {
            if (!missile.gameObject.activeSelf)
                return missile;
        }
        return null;
    }
    


    [SerializeField, Range(0.1f, 1f)]
    private float rotateSpeed = 0.5f;
    [SerializeField]
    private GameObject missilePrefab = null;
    [SerializeField]
    private int maxMissileCount = 3;

    [SerializeField]
    private int maxHp = 3;

    private int curHp = 0;

    private InputMouse im = null;
    private MissileSpawnPoint missileSpawnPoint = null;
    private Coroutine attackCo = null;
    private List<Missile> listMissile = new List<Missile>();

    private MissileStateDelegate missileStateCallback = null;
}