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
    public bool isDie;

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
        if (StageManager.instance != null)
        {
            int difficulty = StageManager.instance.difficulty;

            if (difficulty == 1)
            {
                batSpeed = 8f; enemyTime = 1.5f; batStraightSpeed = 15f; fireSpeed = 7f;
            }
            else if (difficulty == 2)
            {
                batSpeed = 8f; enemyTime = 1.3f; batStraightSpeed = 15f; fireSpeed = 7f;
            }
            else if (difficulty == 3)
            {
                batSpeed = 10f; enemyTime = 1.15f; batStraightSpeed = 18f; fireSpeed = 7f;
            }
            else if (difficulty == 4)
            {
                batSpeed = 10f; enemyTime = 1f; batStraightSpeed = 21f; fireSpeed = 9f;
            }
            else if (difficulty == 5)
            {
                batSpeed = 12f; enemyTime = 0.75f; batStraightSpeed = 23f; fireSpeed = 11f;
            }
        }

        clear = false;
        isDie = false;

        timeText.text = Mathf.Max(0f, (clearTime - currentTime)).ToString("F1");
    }

    private void Update()
    {
        if (!clear)
        {
            if (!isDie) currentTime += Time.deltaTime;
            timeText.text = Mathf.Max(0f, (clearTime - currentTime)).ToString("F1");
        }

    }
}
