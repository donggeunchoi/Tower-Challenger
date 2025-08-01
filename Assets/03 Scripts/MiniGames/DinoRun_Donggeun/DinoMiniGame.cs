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

        if (!_clear)
        {
            _clear = true;
            ShowClearUI();
            StartCoroutine(WaitinTime());
        }
        
    }
    
    IEnumerator WaitinTime()
    {
        SoundManager.instance.PlaySound2D("MiniGameClear");
        if (_clear == false) yield break;
        
        yield return new WaitForSeconds(1.5f);
        
        if (StageManager.instance != null)
            StageManager.instance.MiniGameResult(true);
    }
    
    IEnumerator ScaleUp(RectTransform rect, float duration)
    {
        float time = 0f;
        Vector3 from = Vector3.zero;
        Vector3 to = Vector3.one;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, time / duration);
            rect.localScale = Vector3.Lerp(from, to, t);
            yield return null;
        }
        
        rect.localScale = to;
    }

    void ShowClearUI()
    {
        GameObject miniGameClear = Instantiate(MiniGameClearUI,mainCanvas.transform);
        miniGameClear.transform.SetAsLastSibling();
        
        var rt = miniGameClear.GetComponent<RectTransform>();
        rt.pivot = new Vector2(0.5f, 0.5f);         // 하단 중앙
        rt.anchoredPosition = Vector2.zero;       // 캔버스 하단 중앙
        rt.localScale = Vector3.zero;             // 초기 스케일 0
        
        // 3) Scale 애니메이션
        StartCoroutine(ScaleUp(rt, 0.5f));        // 0.5초 동안
    }
    
    public void HandleHit()
    {
        if (StageManager.instance != null)
            StageManager.instance.MiniGameResult(false);
        UpdateLpui();
    }
}
