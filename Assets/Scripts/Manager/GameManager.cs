using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum EGameState { None = -1, Login, Ready, Start, Play, GameOver, Rank }

    public static bool IsPlaying() { return gameState == EGameState.Play; }
    public static bool IsGameOver() { return gameState == EGameState.GameOver; }

    #region Callback
    public void HitCallback(List<IPoolingObject> _hitList)
    {
        enemyMng.SetDamages(_hitList);

        killCnt += _hitList.Count;

        uiCanvasMng.UIHUDUpdateKillCount(killCnt);
    }

    public void RankButtonCallback()
    {
        rankMng.ImportRank(accountMng.CurId, score, killCnt, timeSec);
        StartCoroutine("ExportRankingRecord");
    }

    private IEnumerator ExportRankingRecord()
    {
        while (!rankMng.IsRecordSucess)
            yield return null;

        rankMng.ExportRank(PrintRanking);
    }

    private void PrintRanking(List<SDataScore> _listDataScore)
    {
        uiCanvasMng.SetActiveHUD(false);
        uiCanvasMng.SetActiveAccount(false);

        uiCanvasMng.OnRank(_listDataScore);
        uiCanvasMng.SetActiveState(true);
    }

    public void RankEnterCallback()
    {
        RetryButtonCallback();
    }

    public void RetryButtonCallback()
    {
        OnReadyProcess(false);
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

    private void Init()
    {
        tower.Init(MissileStateCallback);
        enemyMng.Init(tower.gameObject, EnemyAttackCallback);

        uiCanvasMng.InitUIHUD(tower.MaxHp, tower.MaxMissileCount);
        uiCanvasMng.InitUIState(RetryButtonCallback, RankButtonCallback);
        uiCanvasMng.InitUIAccount(LoginProcess, SignupProcess);

        accountMng.Init(StartGame, ShowAlert);
    }

    private void LoginProcess(string _id, string _pw)
    {
        accountMng.LoginProcess(_id, _pw);
    }

    private void SignupProcess(string _id, string _pw)
    {
        accountMng.SignupProcess(_id, _pw);
    }

    private void ShowAlert(string _alert)
    {
        uiCanvasMng.ShowAlert(_alert);
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

        uiCanvasMng.ResetUIHUDHp();
        uiCanvasMng.ResetUIHUDMissile();
        uiCanvasMng.ResetUIHUDKillCount();
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
        gameState = EGameState.Login;
    }

    private void Start()
    {
        AccountProcess();
    }

    private void AccountProcess()
    {
        uiCanvasMng.SetActiveHUD(false);
        uiCanvasMng.SetActiveState(false);
        uiCanvasMng.SetActiveAccount(true);

        Init();
    }

    private void StartGame()
    {
        OnReadyProcess(true);
    }

    private void OnReadyProcess(bool _isFirstPlay)
    {
        StartCoroutine(ReadyCoroutine(_isFirstPlay));
    }

    private IEnumerator ReadyCoroutine(bool _isFirstPlay)
    {
        if (!_isFirstPlay)
            Retry();

        gameState = EGameState.Ready;
        uiCanvasMng.SetActiveHUD(false);
        uiCanvasMng.SetActiveAccount(false);

        uiCanvasMng.SetActiveState(true);
        uiCanvasMng.OnReady();
        yield return new WaitForSeconds(readyDelay);

        gameState = EGameState.Start;
        uiCanvasMng.OnStart();
        yield return new WaitForSeconds(startDelay);

        gameState = EGameState.Play;
        uiCanvasMng.SetActiveHUD(true);
        uiCanvasMng.SetActiveState(false);

        //if (_isFirstPlay) Init();

        StartCoroutine("TimerCoroutine");
    }



    private void Update()
    {
        if (!IsPlay()) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = Vector3.zero;
            if (inputMouse.Picking("Stage", ref point, 1 << LayerMask.NameToLayer("Stage")))
            {
                if (Vector3.SqrMagnitude(tower.GetPosition() - point) > 2f)
                    tower.Attack(point, HitCallback);
            }
        }
    }

    private bool IsPlay()
    {
        return gameState == EGameState.Play;
    }


    [SerializeField]
    private EnemyManager enemyMng = null;
    [SerializeField]
    private UI_CanvasManager uiCanvasMng = null;
    [SerializeField]
    private AccountManager accountMng = null;
    [SerializeField]
    private RankManager rankMng = null;

    private int killCnt = 0;
    private int timeSec = 0;
    private int score = 0;

    private float readyDelay = 1f;
    private float startDelay = 1f;

    private static EGameState gameState = EGameState.None;

    private InputMouse inputMouse = null;
    private Tower tower = null;
}
