using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    [Header("정보")]
    public StageTimer stageTimer;
    public StageLP stageLP;
    //public TextMeshProUGUI LPText;
    //public TextMeshProUGUI TimerText;

    [Header("게임 상태")]
    public bool isGameActive = true;
    private string _currentMiniGameScene;

    [Header("진행 정보")]
    public int floor = 1;
    public int bestFloor = 1;
    public int totalStageCount = 1;
    private float timerMultiplier = 1f;

    [Header("미니게임 데이터")]
    public MiniGameData[] miniGameDatas;
    private List<MiniGameData> availableMiniGames = new List<MiniGameData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        UpdateAvailableMiniGames();
    }

    private void Start()
    {

        Debug.Log($"초기화 완료: 사용 가능한 미니게임 {availableMiniGames.Count}개");
    }

    private void Update()
    {
        //LPText.text = "LP: " + stageLP.currentLP;
        //TimerText.text = "Time: " + Mathf.Floor(stageTimer.timer);

        //if (isGameActive)
        //{
        //    stageTimer.timer -= Time.deltaTime * timerMultiplier;

        //    if (stageTimer.timer <= 0 || stageLP.currentLP <= 0)
        //    {
        //        GameOver();
        //    }
        //}
    }

    private void UpdateAvailableMiniGames()
    {
        availableMiniGames.Clear();
        foreach (var game in miniGameDatas)
        {
            if (game.allStage || (floor >= game.minStage && floor <= game.maxStage))
            {
                availableMiniGames.Add(game);
                Debug.Log($"추가된 미니게임: {game.name}");
            }
        }
    }

    public void StartRandomMiniGame(GameObject portal)
    {
        Destroy(portal);

        if (availableMiniGames.Count == 0)
        {
            Debug.LogWarning("사용 가능한 미니게임이 없습니다!");
            return;
        }

        int randomIndex = Random.Range(0, availableMiniGames.Count);
        MiniGameData selectedGame = availableMiniGames[randomIndex];
        _currentMiniGameScene = selectedGame.sceneName;

        SceneController.Instance.DisableMainScene();

        SceneManager.LoadScene(_currentMiniGameScene, LoadSceneMode.Additive);

        Debug.Log($"미니게임 시작: {selectedGame.name}");
    }

    public void EndMiniGame(bool isSuccess)
    {
        if (!string.IsNullOrEmpty(_currentMiniGameScene))
        {
            SceneManager.UnloadSceneAsync(_currentMiniGameScene);
            Debug.Log($"미니게임 종료: {_currentMiniGameScene}");
        }

        SceneController.Instance.EnableMainScene();

        if (!isSuccess)
        {
            stageLP.LPdown();
            if (stageLP.currentLP <= 0)
            {
                GameOver();
            }
        }
        else
        {
            Debug.Log("미니게임 성공!");
        }
    }

    private void GameOver()
    {
        isGameActive = false;
        if (floor > bestFloor) bestFloor = floor;
        SceneManager.LoadScene("VillageScene");
    }

    public void NextFloor()
    {
        floor++;
        stageTimer.timer = 60f;

        if (floor % 5 == 0)
        {
            totalStageCount = Mathf.Min(totalStageCount + 1, 4);
        }

        if (floor % 10 == 0)
        {
            timerMultiplier *= 1.2f;
        }

        UpdateAvailableMiniGames();
    }
}