using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class DinoMiniGame : MonoBehaviour
{
    
    [Header("Game Settings")]
    public int lifePoints = 4;
    public float gameDuration = 60f;
    private float gameTimer;
    public bool isGameOver = false;
    public static DinoMiniGame Instance;

    [Header("UI")]
    public TMP_Text lpText;
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

        if (gameTimer <= 0 && !isGameOver)
        {
            GameClear();
        }
    }

    void UpdateLPUI()
    {
        lpText.text = "LP: " + lifePoints;
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
        lifePoints--;
        UpdateLPUI();

        if (lifePoints <= 0)
        {
            GameOver();
        }
    }
}
