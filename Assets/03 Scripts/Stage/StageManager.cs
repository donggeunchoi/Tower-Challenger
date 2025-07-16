using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    public GameObject gameOver;      //게임 오버 창

    [Header("정보")]
    public StageTimer stageTimer;     //스테이지 타이머
    public StageLP stageLP;           //스테이지 LP
    public GameObject infoUI;         //각종 인포메이션이 들어갈 공간 (타이머 LP등)[추후 UI매니저로 이동]

    private HashSet<string> shownMiniGameUIs = new HashSet<string>();

    public int difficulty { get; private set; }              //난이도

    [Header("게임 상태")]
    public bool isGameActive = false;  //현재 게임이 실행되고 있는지 여부

    [Header("진행 정보")]
    public const int FIRST_FLOOR = 1;
    public const int BOSS_FLOOR = 10;
    public const int MAX_FLOOR = 30;
    public int floor = 1;              //현재층
    public int bestFloor = 1;          //최고 기록
    public int totalStageCount = 1;    //현재 깨야하는 스테이지
    public float timerMultiplier = 1f;//타이머 배속
    public Vector3 playerPosition;    //플레이어 포지션 저장
    public int layerNumber;           //플레이어 레이어 저장
    public string currentSceneName;   //현재 씬 이름
    public List<int> stageClearPortal = new List<int>(); //여기에 활성화 되는 포탈 인덱스 값만 저장

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        isGameActive = false;

        if (UIManager.Instance != null)
        {
            infoUI = UIManager.Instance.notDestroyCanvus.gameObject;
            if (infoUI != null)
                infoUI.SetActive(isGameActive);
            stageTimer = UIManager.Instance.timerUI;
            stageLP = UIManager.Instance.stageLPUI;
        }
    }

    #region MiniGameCall
    public void StartGame()
    {
        isGameActive = true;  //현재 게임 시작

        if(infoUI != null)
        infoUI.SetActive(isGameActive);

        ResetInfo();
        
        floor = FIRST_FLOOR;            //현재층 1층

        totalStageCount = 1;  //현재 스테이지 카운트 초기화
        timerMultiplier = 1f; //배속 초기화
        SetFloorInfo();
        RandomStage(); // 게임 시작 시 랜덤 스테이지 생성
        LoadRandomMap(); //랜덤 맵

        StartGameLoad(false);
    }

    public void StartGameLoad(bool isBoss)
    {
        StartCoroutine(TowerManager.Instance.LoadBossStage(isBoss));
    }

    //씬로드가 완료가 됬을때 동작

    private void ResetInfo()
    {
        if (stageTimer != null)
            stageTimer.SetTimer();

        if (stageTimer != null)
            stageLP.ResetLP();
    }

    public void StartNextMiniGame()  //미니게임시작
    {
        currentSceneName = SceneManager.GetActiveScene().name;  //현재 씬 이름 가져오기

        if (MiniGameManager.instance.randomGames.Count == 0)
        {
            return;
        }

        if (stageClearPortal.Count >= 0 && stageClearPortal.Count < MiniGameManager.instance.randomGames.Count && MiniGameManager.instance.randomGames[stageClearPortal.Count] != null)
        {
            MiniGameDatas selectedGame = MiniGameManager.instance.randomGames[stageClearPortal.Count];
            if (UIManager.Instance != null && selectedGame.miniGameInfoUI != null)
            {
                string miniGameID = selectedGame.name; // 또는 selectedGame.sceneName;

                if (!shownMiniGameUIs.Contains(miniGameID))
                {
                    shownMiniGameUIs.Add(miniGameID);
                    UIManager.Instance.InstantiateUI(selectedGame.miniGameInfoUI);
                }
                else
                {
                    Debug.Log($"[MiniGame UI] 이미 본 UI입니다: {miniGameID}, 다시 띄우지 않음.");
                }
            }
            SceneManager.LoadScene(selectedGame.sceneName);
        }
    }

    public void NextFloor()  //다음층이 되었을때 시간초기화 및 스테이지 갯수 시간배속 계산
    {
        ResetInfo();
        floor++;

        if (floor >= bestFloor)
            bestFloor = floor;

        if (GameManager.Instance != null)
            GameManager.Instance.SaveData();

        if (floor % BOSS_FLOOR == 0) //10층마다 보스
        {
            stageTimer.StopTimer();
            totalStageCount = 1;
            BossStage();
            StartGameLoad(true);
            return;
        }

        if (floor >= MAX_FLOOR)
        {
            floor = MAX_FLOOR;
            GameObject gameOverPanel = Instantiate(gameOver, infoUI.transform);  //임시로 게임오버 판넬 추후 클리어판넬 변경
            return;
        }

        SetFloorInfo();

        RandomStage();
        
        StartGameLoad(false);
    }

    private void SetFloorInfo()
    {
        difficulty = Mathf.Clamp((floor - 1) / 10 + 1, 1, 4);

        totalStageCount = Mathf.Clamp((floor - 1) / 5 + 1, 1, 4);

        if (floor < 11)
            timerMultiplier = 1;
        else
            timerMultiplier = Mathf.Pow(1.2f, (floor - 11) / 10 + 1);
    }
    #endregion
    #region MiniGameSet
    private void RandomStage()  //랜덤한 스테이지 생성
    {
        if (MiniGameManager.instance != null)
            MiniGameManager.instance.RandomStage();
    }

    public void BossStage()
    {
        if (MiniGameManager.instance != null)
            MiniGameManager.instance.BossStage();
    }

    public string LoadRandomMap()
    {
        if (TowerManager.Instance != null)
        {
            return TowerManager.Instance.LoadRandomMap();
        }
        else
            return "TopScene-1";
    }

    public void AddPortal(int portal)
    {
        stageClearPortal.Add(portal);
    }

    public void SaveClearPortal(int clear)
    {
        stageClearPortal.Remove(clear);
    }

    public void ResetClearPortal()
    {
        stageClearPortal.Clear();
    }

    public void MiniGameResult(bool result) //게임이 끝나면 호출해줘야 하는 함수
    {
        if (!result)  //실패
        {
            if (stageLP != null)
            {
                stageLP.LPdown();
                if (stageLP.currentLP <= 0)
                    GameOver();
            }
        }
        else  //성공
        {
            if (isGameActive)
            LatePlayerData();  //플레이어를 원레 위치로
        }
    }
    public void GameOver()  //게임오버가 되면 로비씬으로 이동
    {
        if (!isGameActive) //현재 게임이 실행상태가 아니거나 게임오버 상태가 아니라면 돌아가기
            return;

        if (stageTimer.timer <= 0 || stageLP.currentLP <= 0)  //타이머가 0이되거나 LP가 0이되면 게임오버
        {
            isGameActive = false;
            if (floor > bestFloor)
                bestFloor = floor;

            GameObject gameOverPanel = Instantiate(gameOver, infoUI.transform);
        }
    }

    #endregion
    #region Player
    public void SavePlayerPosition(Vector3 position, int layer)
    {
        layerNumber = layer;
        playerPosition = position;
    }
    private void LatePlayerData() //플레이어를 찾아 해당 포지션으로 이동 (오류나서 코루틴으로 변경)
    {
        StartCoroutine(TowerManager.Instance.LatePlayerData());
    }
    #endregion
}