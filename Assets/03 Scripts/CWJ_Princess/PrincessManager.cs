using TMPro;
using UnityEngine;

public class PrincessManager : MonoBehaviour
{
    public static PrincessManager princessInstance;

    public ShieldMove shieldMove;
    public EnemyPos enemyPos;
    public SpriteRenderer spriteRenderer;

    [Header("시간")]
    public float clearTime;
    public float currentTime;

    public bool clear;

    [Header("UI")]
    public TextMeshProUGUI timeText;

    [Header("박쥐 직선 추가 이동속도")]
    public float batStraightSpeed;
    [Header("박쥐 이동속도")]
    public float batSpeed;
    [Header("박쥐가 최대로 올라가는 y값")]
    public float maxY;
    [Header("박쥐다 최대로 내려가는 y값")]
    public float minY;
    [Header("박쥐의 y값 이동의 속도")]
    public float frequency;
    [Header("몬스터가 생성되는 주기")]
    public float enemyTime;

    [Header("불덩이 이동속도")]
    public float fireSpeed;
    
    private void Awake()
    {
        if (princessInstance == null)
        {
            princessInstance = this;
        }

    }

    private void Start()
    {
        clear = false;
        spriteRenderer.gameObject.SetActive(false);

        timeText.text = Mathf.Max(0f, (clearTime - currentTime)).ToString("F1");
    }

    private void Update()
    {
        if (!clear)
        {
            currentTime += Time.deltaTime;
            timeText.text = Mathf.Max(0f, (clearTime - currentTime)).ToString("F1");
        }

    }
}
