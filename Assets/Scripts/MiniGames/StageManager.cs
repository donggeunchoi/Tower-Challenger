using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    StageTimer stageTimer;
    StageLP stageLP;

    [Header("게임 정보")]
    
    private int bestFloor;  //나중에 최고기록 저장
    private int currentStage;  //현재 스테이지
    private int floor;        //현재 층
    public float gameSpeed;   //게임 속도
    private int currentFloorStage; //현재 층 스테이지
    private GameObject currentMiniGame;  //현재 게임

    [Header("미니게임 데이터")]
    public MiniGameData[] miniGameDatas;
    public int totalStageCount;
    private List<MiniGameData> stageMiniGames = new List<MiniGameData>();
    public Transform miniGameSlot;
    public bool isGameActive = true;

    [Header("타이머")]
    
    private float timerMultiplier = 1f; // 타이머 가속 배율

    [Header("테스트 텍스트")]
    public TextMeshProUGUI LPText;
    public TextMeshProUGUI StageText;
    public TextMeshProUGUI FloorText;
    public TextMeshProUGUI TimerText;
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
        stageTimer = GetComponent<StageTimer>();
        stageLP = GetComponent<StageLP>();
        isGameActive = false;
    }

    private void Update()
    {
        if (LPText != null)
            LPText.text = "LP" + stageLP.currentLP;
        if (LPText != null)
            StageText.text = "Stage : " + currentStage ;
        if (LPText != null)
            FloorText.text = "Floor : " + floor;
        if (TimerText != null)
            TimerText.text = "Timer : " + stageTimer.timer;

        
    }

    public void StartGame()
    {
        isGameActive = true;

        stageLP.ResetLP();
        currentStage = 1;
        floor = 1;

        RandomStage();
    }

    public void NextFloor()
    {
        currentStage = 0;
        currentFloorStage = 0;
        floor++;
        stageTimer.timer = 60f;
        UpdateFloorLogic();
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
        if (currentMiniGame != null)
            Destroy(currentMiniGame);

        currentStage++;
        RandomStage();
        ShowPopupMiniGame(stageMiniGames[currentFloorStage].miniGamePrefab);
    }

    public void LPdown()
    {
        if (!stageLP.IsLPdown())
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

        if (isSuccess)
        {
            if (currentMiniGame != null)
            {
                Destroy(currentMiniGame);
            }
            currentFloorStage++;
        }
        else
        {
            LPdown();
        }
    }

    private void StartNextMiniGame() //다음미니게임
    {
        Debug.Log("게임 시작!");

        MiniGameData nextGame = stageMiniGames[currentFloorStage];
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

        currentFloorStage = 0;
    }
    public void ShowPopupMiniGame(GameObject prefab)
    {
        float distance = 2f;
        Vector3 spawnPos = Camera.main.transform.position + Camera.main.transform.forward * distance;
        Quaternion spawnRot = Quaternion.LookRotation(-Camera.main.transform.forward);
        GameObject popup = Instantiate(prefab, spawnPos, spawnRot);
    }
}
