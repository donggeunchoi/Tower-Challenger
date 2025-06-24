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
    public TextMeshProUGUI StageText;
    public TextMeshProUGUI FloorText;


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
        StageText.text = currentStage + "";
        FloorText.text = floor + "";

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
    }

    public void NextFloor()
    {
        currentStage = 1;
        floor++;

        RandomStage();
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
            return;
        }
    }

    public void ReportGameResult(bool isSuccess)
    {
        if (!isGameActive)
        {
            Destroy(currentMiniGame);
            return; 
        }

        new WaitForSeconds(3f);

        if (isSuccess)
        {
            if (currentMiniGame != null)
            {
                Destroy(currentMiniGame);
            }
            currentStageIndex++;
        }
        else
        {
            LPdown();
        }
    }

    private void StartNextMiniGame() //다음미니게임
    {
        Debug.Log("게임 시작!");

        MiniGameData nextGame = stageMiniGames[currentStageIndex];
        currentMiniGame = Instantiate(nextGame.miniGamePrefab, miniGameSlot);
    }

    private void GameOver()  //게임오버
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
