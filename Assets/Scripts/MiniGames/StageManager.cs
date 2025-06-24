using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [Header("게임 정보")]
    public const int DEFALT_LP = 4;
    public int bonusLP;
    private int currentLP;
    private int bestStage;
    private int currentStage;
    private int floor;
    public float gameSpeed;
    public TextMeshProUGUI LPText;

    private int currentStageIndex;
    private GameObject currentMiniGame;

    [Header("미니게임 데이터")]
    public MiniGameData[] miniGameDatas;
    public int totalStageCount;
    private List<MiniGameData> stageMiniGames = new List<MiniGameData>();
    public Transform miniGameSlot;
    private bool isGameActive = true;

    [Header("타이머")]
    public float stageTimer = 60f;
    private float timerMultiplier = 1f; // 타이머 가속 배율

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
        isGameActive = false;
    }

    private void Update()
    {
        LPText.text = currentLP + "";

        if (isGameActive)
            stageTimer -= Time.deltaTime;
    }

    public void StartGame()
    {
        isGameActive = true;

        currentLP = DEFALT_LP + bonusLP;
        currentStage = 1;
        floor = 1;

        RandomStage();
        StartNextMiniGame();
    }

    private void UpdateFloorLogic()
    {
        if (floor % 5 == 0)
        {
            totalStageCount++;
        }

        if (floor % 10 == 0)
        {
            timerMultiplier *= 1.2f;
        }
        floor++;
    }

    public void NextStage()
    {
        Destroy(currentMiniGame);
        stageTimer = 60f;
        currentStage++;
        UpdateFloorLogic();
        RandomStage();
        currentStageIndex = 0;
        StartNextMiniGame();
    }

    public void LPdown()
    {
        currentLP = Mathf.Max(currentLP - 1, 0);
        Debug.Log($"LP 감소! 현재 LP: {currentLP}");

        if (currentLP <= 0)
        {
            GameOver();
            return; // 추가 실행 방지
        }
    }

    public void ReportGameResult(bool isSuccess)
    {
        if (!isGameActive) return;

        new WaitForSeconds(3f);

        if (isSuccess)
        {
            currentStageIndex++;

            if (currentStageIndex >= stageMiniGames.Count)
            {
                NextStage(); // 모든 미니게임 성공 → 다음 스테이지
            }
            else
            {
                StartNextMiniGame(); // 동일 스테이지 내 다음 미니게임
            }
        }
        else
        {
            // 스테이지 실패 처리
            HandleStageFailure();
        }
    }

    private void HandleStageFailure()
    {
        LPdown();

        // 게임 오버 시 추가 처리 중단
        if (!isGameActive) return;

        if (currentLP > 0)
        {
            if (currentStageIndex < stageMiniGames.Count - 1)
            {
                currentStageIndex++;
                StartNextMiniGame();
            }
            else
            {
                currentStageIndex = 0;
                StartNextMiniGame();
            }
        }
    }

    private void StartNextMiniGame()
    {
        if (currentMiniGame != null)
        {
            Destroy(currentMiniGame);
        }

        Debug.Log("게임 시작!");

        MiniGameData nextGame = stageMiniGames[currentStageIndex];
        currentMiniGame = Instantiate(nextGame.miniGamePrefab, miniGameSlot);
    }

    private void GameOver()
    {
        isGameActive = false;

        if (currentMiniGame != null)
        {
            Destroy(currentMiniGame);
            currentMiniGame = null;
        }

        Debug.Log("게임 오버");
    }

    private void RandomStage()
    {
        stageMiniGames.Clear();
        List<MiniGameData> availableGames = new List<MiniGameData>(miniGameDatas);

        for (int i = 0; i < totalStageCount; i++)
        {
            if (availableGames.Count == 0)
            {
                availableGames = new List<MiniGameData>(miniGameDatas);
            }

            int randomIndex = Random.Range(0, availableGames.Count);
            MiniGameData selected = availableGames[randomIndex];
            availableGames.RemoveAt(randomIndex);
            stageMiniGames.Add(selected);
        }

        currentStageIndex = 0;
    }
}
