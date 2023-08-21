using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum EGameState { None = -1, Ready, Start, Play, GameOver, Rank }

    public static bool IsPlaying() { return gameState == EGameState.Play; }
    public static bool IsGameOver() { return gameState == EGameState.GameOver; }
    
    #region Callback
    public void HitCallback(List<IPoolingObject> _hitList)
    {
        enemyMng.SetDamages(_hitList);

        killCnt += _hitList.Count;

        uiCanvasMng.UIHUDUpdateKillCount(killCnt);
    }

    public void RetryButtonCallback()
    {
        OnReadyProcess(false);
    }

    public void RankButtonCallback()
    {
        uiCanvasMng.SetActiveHUD(false);
        uiCanvasMng.SetActiveState(false);
        uiCanvasMng.SetActiveRank(true, score, RankEnterCallback);
    }

    public void RankEnterCallback()
    {
        RetryButtonCallback();
    }


    private void EnemyAttackCallback(int _dmg = 1)
    {
        int curHp = tower.Damage(_dmg);
        if (curHp < 0) return;

        uiCanvasMng.UIHUDUpdateHp(curHp);
        if (curHp == 0)
        {
            gameState = EGameState.GameOver;
            uiCanvasMng.SetActiveHUD(false);
            uiCanvasMng.SetActiveState(true);
            uiCanvasMng.OnGameOver(killCnt, timeSec, CalcScore(killCnt, timeSec));
        }
    }

    private void MissileStateCallback(int _missileIdx, bool _isFill)
    {
        uiCanvasMng.UIHUDUpdateMissileStateWithIndex(_missileIdx, _isFill);
    }
    #endregion

    private int CalcScore(int _killCnt, int _timeSec)
    {
        return score = _killCnt * 1000 + _timeSec * 500;
    }

    private IEnumerator ReadyCoroutine(bool _isFirstPlay)
    {
        if (!_isFirstPlay)
            Retry();

        gameState = EGameState.Ready;
        uiCanvasMng.SetActiveHUD(false);
        uiCanvasMng.SetActiveState(true);
        uiCanvasMng.SetActiveRank(false);

        uiCanvasMng.OnReady();
        yield return new WaitForSeconds(readyDelay);

        gameState = EGameState.Start;
        uiCanvasMng.OnStart();
        yield return new WaitForSeconds(startDelay);

        gameState = EGameState.Play;
        uiCanvasMng.SetActiveHUD(true);
        uiCanvasMng.SetActiveState(false);

        if (_isFirstPlay) Init();

        StartCoroutine("TimerCoroutine");
    }

    private void Init()
    {
        tower.Init(MissileStateCallback);
        enemyMng.Init(tower.gameObject, EnemyAttackCallback);

        uiCanvasMng.UIHUDInitHP(tower.MaxHp);
        uiCanvasMng.UIHUDInitMissile(tower.MaxMissileCount);
        uiCanvasMng.UIHUDInitKillCount();

        uiCanvasMng.SetRetryButtonCallback(RetryButtonCallback);
        uiCanvasMng.SetRankButtonCallback(RankButtonCallback);
    }

    private void Retry()
    {
        // 체력 리셋
        // 미사일 리셋
        tower.Retry();

        // 적 모두 릴리즈
        enemyMng.Retry();

        // 시간 초기화
        // 킬 카운트 초기화
        killCnt = 0;
        timeSec = 0;
        score = 0;

        uiCanvasMng.UIHUDFullHp();
        uiCanvasMng.UIHUDReloadMissile();
        uiCanvasMng.UIHUDInitKillCount();
        uiCanvasMng.UIHUDUpdateTimer(timeSec);

        StopCoroutine("TimerCoroutine");
    }

    private IEnumerator TimerCoroutine()
    {
        while (true)
        {
            uiCanvasMng.UIHUDUpdateTimer(timeSec);
            yield return new WaitForSeconds(1f);
            ++timeSec;
        }
    }

    private void Awake()
    {
        tower = FindAnyObjectByType<Tower>();
        inputMouse = InputMouse.Instance;
        gameState = EGameState.Ready;
    }

    private void Start()
    {
        OnReadyProcess(true);
    }

    private void OnReadyProcess(bool _isFirstPlay)
    {
        StartCoroutine(ReadyCoroutine(_isFirstPlay));
    }

    private void Update()
    {
        if (gameState != EGameState.Play) return;
        // 이렇게 enum을 직접 비교하는거보다  뭐 isPlay같은 메소드 만드는게 더 좋다.
        // if(!IsPlay) reutrn;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = Vector3.zero;
            if (inputMouse.Picking("Stage", ref point, 1 << LayerMask.NameToLayer("Stage")))
            {
                if(Vector3.SqrMagnitude(tower.GetPosition() - point) > 2f)
                tower.Attack(point, HitCallback);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
            Debug.Log(killCnt);
    }


    [SerializeField]
    private EnemyManager enemyMng = null;
    [SerializeField]
    private UI_CanvasManager uiCanvasMng = null;

    private int killCnt = 0;
    private int timeSec = 0;
    private int score = 0;

    private float readyDelay = 1f;
    private float startDelay = 1f;

    private static EGameState gameState = EGameState.None;

    private InputMouse inputMouse = null;
    private Tower tower = null;
}
