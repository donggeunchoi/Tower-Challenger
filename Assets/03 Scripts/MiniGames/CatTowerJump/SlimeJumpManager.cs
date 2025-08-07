using System.Collections;
using TMPro;
using UnityEngine;

public class SlimeJumpManager : MonoBehaviour
{
    public static SlimeJumpManager Instance;
    
    [Header("References")]
    public Transform playerTransform;
    public TextMeshProUGUI TargetText;
    public TextMeshProUGUI CurrnetText;

    private float startY;
    [SerializeField]private float TargetY;
    
    [Header("미니게임 클리어 UI")]
    public GameObject miniGameClearUI;
    public Canvas mainCanvas;
    private bool _clear = false;
    
    
    private void Awake()
    {
       Instance = this;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (StageManager.instance != null && GameManager.Instance)
        {
            int difficulty = StageManager.instance.difficulty;

            if (difficulty == 1)
            {
                TargetY = 70;
            }
            else if (difficulty == 2)
            {
                TargetY = 90;
            }
            else if (difficulty == 3)
            {
                TargetY = 110;
            }
            else if (difficulty == 4)
            {
                TargetY = 130;
            }
            else if (difficulty == 5)
            {
                TargetY = 150;
            }
        }

        startY =playerTransform.position.y;
        UpdateText(TargetY);
    }

    // Update is called once per frame
    void Update()
    {
        float currentY = playerTransform.position.y;
        if (currentY > TargetY)
        {
            if (!_clear)
            {
                _clear = true;
                SoundManager.instance.PlaySound2D("MiniGameClear");
                ShowClearUI();
                StartCoroutine(WaitinTime());
            }
        }
        
        CurrnetText.text = $"{currentY.ToString("N0")}m";
        
    }
    private void UpdateText(float value)
    {
        int currentY = Mathf.FloorToInt(value);
        TargetText.text = $"{currentY.ToString()}m";
    }
    
    IEnumerator WaitinTime()
    {
        if(_clear == false) yield break;
        
        playerTransform.localScale = Vector3.zero;
        
        yield return new WaitForSeconds(2f);
        
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
        GameObject miniGameClear = Instantiate(miniGameClearUI,mainCanvas.transform);
        miniGameClear.transform.SetAsLastSibling();
        
        var rt = miniGameClear.GetComponent<RectTransform>();
        rt.pivot = new Vector2(0.5f, 0.5f);         // 하단 중앙
        rt.anchoredPosition = Vector2.zero;       // 캔버스 하단 중앙
        rt.localScale = Vector3.zero;             // 초기 스케일 0
        
        // 3) Scale 애니메이션
        StartCoroutine(ScaleUp(rt, 0.5f));        // 0.5초 동안
    }
}
