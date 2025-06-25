using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [Header("정보")]
    public StageTimer stageTimer;
    public StageLP stageLP;

    [Header("게임 상태")]
    public bool isGameActive = false;
    private bool _isGameOver = false;

    [Header("진행 정보")]
    public int floor = 1;
    public int bestFloor = 1;
    public int stageCount = 0;
    public int totalStageCount = 1;
    private float timerMultiplier = 1f;
    private Vector3 playerPosition;
    private string currentSceneName;

    [Header("미니게임 데이터")]
    public MiniGameData[] miniGameDatas;
    private List<MiniGameData> ableMiniGames = new List<MiniGameData>();

    private const string _mainSceneName = "VillageScene";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;  //씬전환후 추가 작업이 필요한 경우 실행
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        isGameActive = false;
    }

    public void StartGame()
    {
        _isGameOver = false;
        isGameActive = true;
        stageLP.ResetLP();
        stageTimer.SetTimer();
        floor = 1;
        totalStageCount = 0;
        timerMultiplier = 1f;
        UpdateAvailableMiniGames(); // 게임 시작 시 랜덤 스테이지 생성

        //다초기화하고 미니게임 장소로 이동
    }

    public void SavePlayerPosition(Vector3 position) => playerPosition = position;  //현재 위치 저장

    private void UpdateAvailableMiniGames()  //랜덤한 스테이지 생성
    {
        ableMiniGames.Clear();
        foreach (var game in miniGameDatas)
        {
            if (game.allStage || (floor >= game.minStage && floor <= game.maxStage))
                ableMiniGames.Add(game);
        }
    }

    public void StartNextMiniGame()  //미니게임시작
    {
        currentSceneName = SceneManager.GetActiveScene().name;

        Debug.Log(currentSceneName);

        UpdateAvailableMiniGames();
        if (ableMiniGames.Count == 0)
        {
            Debug.LogWarning("No available mini-games!");
            return;
        }

        var selectedGame = ableMiniGames[Random.Range(0, ableMiniGames.Count)];
        SceneManager.LoadScene(selectedGame.sceneName);
    }  

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != _mainSceneName) return;
    }

    public void MiniGameResult(bool result) //게임이 끝나면 호출해줘야 하는 함수
    {
        if (!result)
        {
            stageLP.LPdown();
            if (stageLP.currentLP <= 0) GameOver();
        }
        else
        {
            SceneManager.LoadScene(currentSceneName);
            Debug.Log("Mini-game success!");
        }

        RestorePlayerPosition();
    }

    private void RestorePlayerPosition() //플레이어를 찾아 해당 포지션으로 이동
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player) player.transform.position = playerPosition;
    }

    private void GameOver()  //게임오버가 되면 로비씬으로 이동
    {
        if (!isGameActive || _isGameOver) return;

        if (stageTimer.timer <= 0 || stageLP.currentLP <= 0)

        _isGameOver = true;
        isGameActive = false;
        if (floor > bestFloor) bestFloor = floor;
        SceneManager.LoadScene(_mainSceneName);
    }

    public void NextFloor()  //다음층이 되었을때 시간초기화 및 스테이지 갯수 시간배속 계산
    {
        floor++;
        stageTimer.timer = 60f;

        if (floor % 5 == 0)
            totalStageCount = Mathf.Min(totalStageCount + 1, 4);

        if (floor % 10 == 0)
            timerMultiplier *= 1.2f;

        UpdateAvailableMiniGames();
    }
    
}