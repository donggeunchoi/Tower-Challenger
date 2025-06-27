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
        StageManager.instance.MiniGameResult(true);
    }
    
    
    public void HandleHit()
    {
        StageManager.instance.MiniGameResult(false);
        UpdateLpui();
    }
}
