using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public void RespawnEnemy(List<Enemy> _enemyList)
    {
        for (int i = 0; i < maxEnemyCount; ++i)
        {
            foreach (Enemy enemy in _enemyList)
            {
                if (enemy == enemyList[i])
                {
                    enemy.transform.position = ComputeRandomPosWithCircleRange(Vector3.zero);
                }
            }
        }
    }

    // 보통 이럴때는 몇 번째 적이 맞았는지를 아는 것이 좋기 때문에
    // int배열같은 것을 받는다.
    // 가공도 안되고 속도도 빠르기 때문
    public void SetDamages(List<IPoolingObject> _hitList)
    {
        foreach (IPoolingObject enemy in arrayEnemy)
        {
            foreach(IPoolingObject hitEnemy in _hitList)
            {
                // equals가 object형일때는 조금 미묘하긴 한데 대부분의 경우 equals가 더 빠르다.
                if(enemy.Equals(hitEnemy))
                {
                    // Release
                    //Destroy(enemy.gameObject);
                    enemy.Release();

                    continue;
                }
            }
        }
    }



    public void Init(GameObject _target, AttackDelegate _attackCallback = null)
    {
        target = _target;
        attackCallback = _attackCallback;

        arrayEnemy = new IPoolingObject[maxEnemyCount];

        for(int i = 0; i < maxEnemyCount; ++i)
        {
            GameObject go = Instantiate(enemyPrefab, ComputeRandomPosWithCircleRange(Vector3.zero), Quaternion.identity, transform);
            go.name = "Enemy_" + i;

            arrayEnemy[i] = go.GetComponent<IPoolingObject>();
            ((Enemy)arrayEnemy[i]).Release();
        }

        StartCoroutine(RespawnCoroutine());
    }

    public void Retry()
    {
        foreach (Enemy enemy in arrayEnemy)
            enemy.Release();
    }


    private void Awake()
    {
        enemyPrefab = Resources.Load<GameObject>("Prefabs\\P_Enemy");
    }

    private Vector3 RandomPosition()
    {
        Vector2 rnd = Random.insideUnitCircle * 20f;
        return new Vector3(rnd.x, 0f, rnd.y);
    }

    private Vector3 ComputeRandomPosWithCircleRange(Vector3 _centerPos)
    {
        if (outerCircleRad < innerCircleRad)
        {
            Debug.LogError("OuterCircleRad must bigger than InnerCircleRad");
            Debug.Break();
            return Vector3.zero;
        }

        float rndLength = Random.Range(outerCircleRad, innerCircleRad);
        float rndAngle = Random.Range(-180, 180);

        return _centerPos + new Vector3(rndLength * Mathf.Cos(rndAngle), 0f, rndLength * Mathf.Sin(rndAngle));
    }

    private IEnumerator RespawnCoroutine()
    {
        while (true)
        {
            if(GameManager.IsPlaying())
            {
                foreach (Enemy enemy in arrayEnemy)
                {
                    if (!enemy.isAlive())
                    {
                        enemy.SetPosition(ComputeRandomPosWithCircleRange(Vector3.zero));
                        enemy.AddPatterns();
                        enemy.Init(target, attackCallback);
                        break;
                    }
                }
            }

            yield return new WaitForSeconds(enemySpawnRate);
        }
    }


    [SerializeField]
    private int     maxEnemyCount = 20;
    [SerializeField]
    private int     innerCircleRad = 20;
    [SerializeField]
    private int     outerCircleRad = 20;
    [SerializeField]
    private float   enemySpawnRate = 3f;

    [SerializeField]
    private EP_EnemyPatternBase[] enemyPatters = null;

    private List<Enemy> enemyList = new List<Enemy>();
    private IPoolingObject[] arrayEnemy = null;

    private GameObject enemyPrefab = null;
    private GameObject target = null;
    private AttackDelegate attackCallback = null;

}