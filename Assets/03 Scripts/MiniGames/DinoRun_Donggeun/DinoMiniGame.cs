using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class DinoMiniGame : MonoBehaviour
{
    
    [Header("Game Settings")]
    public float gameDuration = 10f;
    private float gameTimer;
    public bool isGameOver = false;
    public static DinoMiniGame instance;
    public GameObject MiniGameClearUI;
    public Canvas mainCanvas;
    private bool _clear = false;
    
    public float baseSpeed = 5f;

    public float speedIncreaseRate = 1f;
    public float CurrentSpeed { get;private set; }

    [Header("UI")]
    public GameObject[] lpIcon;
    public Image timeBarImage;
    public TMP_Text timerText;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (StageManager.instance != null && GameManager.Instance != null)
        {
            int difficulty = StageManager.instance.difficulty;

            MiniGameData data = GameManager.Instance.miniGameDataList.Find(x => x.name == "SlimeRun" && x.DifficultyLevel == difficulty);

            if (data != null)
            {
                baseSpeed = data.baseSpeed;
                gameDuration = data.gameDuration;
            }
        }
        
        gameTimer = gameDuration;
        
        UpdateLpui();
    }

    void Update()
    {
        if (isGameOver) return;

        // Timer
        gameTimer -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.CeilToInt(gameTimer).ToString();
        
        CurrentSpeed = baseSpeed + (gameDuration - gameTimer) * speedIncreaseRate;
        
        float percent = gameTimer / gameDuration;
        timeBarImage.fillAmount = percent;
        
        if (gameTimer <= 0 && !isGameOver)
        {
            Debug.Log("여기 들어오니?");
            GameClear();
        }
    }

    void UpdateLpui()
    {
        for (int i = 0; i < lpIcon.Length; i++)
        {
            //lpIcon[i].SetActive(i < StageManager.instance.stageLP.currentLP);
        }
    }

    void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
    }

    void GameClear()
    {
        GameObject miniGameClear = Instantiate(MiniGameClearUI,mainCanvas.transform);
        miniGameClear.transform.SetAsLastSibling();

        if (!_clear)
        {
            _clear = true;
            StartCoroutine(WaitinTime());
        }
        
    }
    
    IEnumerator WaitinTime()
    {
        if(_clear == false) yield break;
        
        yield return new WaitForSeconds(1f);
        
        if (StageManager.instance != null)
            StageManager.instance.MiniGameResult(true);
    }
    
    public void HandleHit()
    {
        if (StageManager.instance != null)
            StageManager.instance.MiniGameResult(false);
        UpdateLpui();
    }
}
