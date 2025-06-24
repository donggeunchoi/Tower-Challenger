using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class DinoMiniGame : MonoBehaviour
{
    
    [Header("Game Settings")]
    public int LP = 4;
    public float gameDuration = 10f;
    private float gameTimer;
    public bool isGameOver = false;
    public static DinoMiniGame Instance;
    
    public float baseSpeed = 5f;
    public float speedIncreaseRate = 1f;
    public float currentSpeed { get;private set; }

    [Header("UI")]
    public GameObject[] lpIcon;
    public Image timeBarImage;
    public TMP_Text timerText;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameTimer = gameDuration;
        
        UpdateLPUI();
    }

    void Update()
    {
        if (isGameOver) return;

        // Timer
        gameTimer -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.CeilToInt(gameTimer).ToString();
        
        currentSpeed = baseSpeed + (gameDuration - gameTimer) * speedIncreaseRate;
        
        float percent = gameTimer / gameDuration;
        timeBarImage.fillAmount = percent;
        
        if (gameTimer <= 0 && !isGameOver)
        {
            GameClear();
        }
    }

    void UpdateLPUI()
    {
        for (int i = 0; i < lpIcon.Length; i++)
        {
            lpIcon[i].SetActive(i < LP);
        }
    }

    void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
    }

    void GameClear()
    {
        isGameOver = true;
        Time.timeScale = 0f;
    }
    
    
    public void HandleHit()
    {
        LP = Mathf.Max(LP - 1);
        UpdateLPUI();

        if (LP <= 0)
        {
            GameOver();
        }
    }
}
