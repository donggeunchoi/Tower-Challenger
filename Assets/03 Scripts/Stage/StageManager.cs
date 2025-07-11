﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    public GameObject gameOver;      //게임 오버 창
    [Header("매니저")]
    private UIManager uiManager;

    [Header("정보")]
    public StageTimer stageTimer;     //스테이지 타이머
    public StageLP stageLP;           //스테이지 LP
    public GameObject infoUI;         //각종 인포메이션이 들어갈 공간 (타이머 LP등)[추후 UI매니저로 이동]
    public int difficulty;              //난이도

    [Header("게임 상태")]
    public bool isGameActive = false;  //현재 게임이 실행되고 있는지 여부
    private bool isGameOver = false;  //게임오버가 됬는지
    public bool isTest = false;

    [Header("진행 정보")]
    public const int FIRST_FLOOR = 1;
    public const int BOSS_FLOOR = 10;
    public const int MAX_FLOOR = 40;
    public int floor = 1;              //현재층
    public int bestFloor = 1;          //최고 기록
    public int totalStageCount = 1;    //현재 깨야하는 스테이지
    public float timerMultiplier = 1f;//타이머 배속
    private Vector3 playerPosition;    //플레이어 포지션 저장
    private int layerNumber;           //플레이어 레이어 저장
    private string currentSceneName;   //현재 씬 이름
    public List<int> stageClearPortal = new List<int>(); //여기에 활성화 되는 포탈 인덱스 값만 저장

    [Header("미니게임 데이터")]
    public MiniGameDatas[] miniGameDatas;    //미니게임 데이터
    private List<MiniGameDatas> randomGames = new List<MiniGameDatas>();  //랜덤으로 미니게임 배열이 들어갈 공간
    
    [SerializeField] string[] mapScenes;  //맵씬 모음

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        uiManager = UIManager.Instance;
        isGameActive = false;

        infoUI = uiManager.notDestroyCanvus.gameObject;

        if (infoUI != null)
            infoUI.SetActive(isGameActive);

        stageTimer = uiManager.timerUI;
        stageLP = uiManager.stageLPUI;
    }

    #region MiniGameCall
    public void StartGame()
    {
        isGameOver = false;  //게임오버상태 초기화
        isGameActive = true;  //현재 게임 시작

        if(infoUI != null)
        infoUI.SetActive(isGameActive);

        ResetInfo();
        
        floor = FIRST_FLOOR;            //현재층 1층

        if (isTest) //테스트 코드
            floor = 1;

        totalStageCount = 1;  //현재 스테이지 카운트 초기화
        timerMultiplier = 1f; //배속 초기화
        SetFloorInfo();
        RandomStage(); // 게임 시작 시 랜덤 스테이지 생성
        LoadRandomMap(); //랜덤 맵

        StartCoroutine(StartGameLoad(false));
    }

    private IEnumerator StartGameLoad(bool isBoss)
    {
        AsyncOperation ansynLoad;

        if (isBoss)
            ansynLoad = SceneManager.LoadSceneAsync("BossRoom");
        else
            ansynLoad = SceneManager.LoadSceneAsync(LoadRandomMap()); //어씬크

        while (!ansynLoad.isDone)
        {
            yield return null;
        }

        Map map = FindAnyObjectByType<Map>();
        map.SetRandomPortal();
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

        if (randomGames.Count == 0)
        {
            return;
        }

        if (stageClearPortal.Count >= 0 && stageClearPortal.Count < randomGames.Count && randomGames[stageClearPortal.Count] != null)
        {
            MiniGameDatas selectedGame = randomGames[stageClearPortal.Count];
            // if (floor <= 9)
            // {
            //     if (uiManager != null)
            //     {
            //         uiManager.InstantiateUI(selectedGame.miniGameInfoUI);
            //     }
            // }
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
            GameManager.Instance.playerData.SaveData();

        if (floor % BOSS_FLOOR == 0) //10층마다 보스
        {
            stageTimer.StopTimer();
            totalStageCount = 1;
            BossStage();
            StartCoroutine (StartGameLoad(true));
            return;
        }

        if (floor >= MAX_FLOOR)
        {
            Debug.Log("GameClear");
            floor = MAX_FLOOR;
            //임시로 게임오버 판넬
            GameObject gameOverPanel = Instantiate(gameOver, infoUI.transform);
            return;
        }

        SetFloorInfo();

        Debug.Log(timerMultiplier);

        RandomStage();
        
        StartCoroutine(StartGameLoad(false));
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
        randomGames.Clear();

        List<MiniGameDatas> gameList = new List<MiniGameDatas>();  //사용가능한 배열생성
        for (int i = 0; i < miniGameDatas.Length; i++)  //사용가능 한 미니게임 리스트 생성
        {
            MiniGameDatas game = miniGameDatas[i];
            if (!game.isBoss)
            {
                if (game.allStage || (floor >= game.minStage && floor <= game.maxStage))
                {
                    gameList.Add(game);
                }
            }
        }

        if (gameList.Count == 0)
        {
            Debug.Log("사용가능한 게임이 없습니다/ gameList가 비었습니다");
            return;
        }

        gameList = gameList.OrderBy(x => Random.value).ToList();
        for (int k = 0; k < Mathf.Min(gameList.Count, totalStageCount); k++)
        {
            randomGames.Add(gameList[k]);
        }

        while (randomGames.Count < totalStageCount);
        {
            randomGames.Add(gameList[Random.Range(0, gameList.Count)]);
        }
    }

    public void BossStage()
    {
        randomGames.Clear();

        if (uiManager != null)
        {
            if (uiManager.timerUI != null)
                uiManager.timerUI.ResetTimer();
        }

        List<MiniGameDatas> gameList = new List<MiniGameDatas>();  //사용가능한 배열생성
        for (int i = 0; i < miniGameDatas.Length; i++)  //사용가능 한 미니게임 리스트 생성
        {
            MiniGameDatas game = miniGameDatas[i];
            if (game.isBoss)
            {
                gameList.Add(game);
            }
        }

        if (floor == 0)
            return;

        int index = floor / BOSS_FLOOR;

        if (index < gameList.Count)
        {
            if (gameList[floor / 5] != null)
                randomGames.Add(gameList[floor / BOSS_FLOOR]);
        }

        randomGames.AddRange(gameList);
        
        if (randomGames.Count <= 0) //보스게임이 없으면 아무거나라도
        {
            if (gameList.Count >= 1)
            {
                randomGames.Add(gameList[Random.Range(0, gameList.Count)]);
            }
            else
            {
                RandomStage();
            }
        }
        StartGameLoad(true);
    }

    public string LoadRandomMap()
    {
        int randomSceneNum = Random.Range(0, mapScenes.Length);
        if (mapScenes == null)
        {
            return mapScenes[0];
        }
        string mapName = mapScenes[randomSceneNum];
        return mapName;
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
            StartCoroutine(LatePlayerData());  //플레이어를 원레 위치로
        }
    }
    public void GameOver()  //게임오버가 되면 로비씬으로 이동
    {
        if (!isGameActive || isGameOver) //현재 게임이 실행상태가 아니거나 게임오버 상태가 아니라면 돌아가기
            return;

        if (stageTimer.timer <= 0 || stageLP.currentLP <= 0)  //타이머가 0이되거나 LP가 0이되면 게임오버
        {
            isGameOver = true;
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
    private IEnumerator LatePlayerData() //플레이어를 찾아 해당 포지션으로 이동 (오류나서 코루틴으로 변경)
    {
        AsyncOperation ansynLoad = SceneManager.LoadSceneAsync(currentSceneName); //어씬크
        while (!ansynLoad.isDone)
        {
            yield return null;
        }

        if (stageClearPortal.Count != 0)
        {
            for (int i = 0; i < stageClearPortal.Count; i++)
            {
                Debug.Log(stageClearPortal[i]);
            }
        }

        if (stageClearPortal.Count == 0 && isGameActive)
        {
            Map map = FindAnyObjectByType<Map>();
            map.AllClearFloor();
        }

        PlayerSetting();  //플레이어 놓기
        
        if (isGameActive)
        {
            if (stageClearPortal.Count == 0)
                ResetClearPortal();
        }
    }
    
    public void PlayerSetting()
    {
        GameObject player = null;
        while (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");  //플레이어를 찾아서 
        }

        if (player != null)
        {
            player.layer = layerNumber;
            string LayerName;
            switch (layerNumber)
            {
                case 20:
                    LayerName = "Layer 1";
                    break;
                case 21:
                    LayerName = "Layer 2";
                    break;
                default:
                    LayerName = "Layer 3";
                    break;
            }
            player.GetComponent<SpriteRenderer>().sortingLayerName = LayerName;
            player.transform.position = playerPosition;   //플레이어 위치를 수정
        }
    }
    #endregion
}