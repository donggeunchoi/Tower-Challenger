using System.Collections;
using TMPro;
using UnityEngine;


public class ProGameManager : MonoBehaviour
{
    public GameObject Boss;
    public GameObject Boss2;
    public GameObject[] TeleportPoint;
    public GameObject SpeedInput;

    [Header("속도")]
    public float moveSpeed = 5f;

    [Header("다음 패턴")]
    public float PatternSpeed = 1.5f; // 1.5초 간격

    [Header("공 패턴")]
    public GameObject[] BallPreFab;

    [Header("플레이어")]
    public GameObject Player;

    [Header("출력 UI")]
    public GameObject PrintOut;

    private TextMeshProUGUI printText;
    private TMP_InputField speedInputField;
    private bool GameStart = false;
    private float tiltTimer;
    private float end = 60;
    private StageManager stageManager;

    // 현재 사용 중인 발사체 프리팹
    private GameObject currentBallPrefab;

    void Start()
    {
        stageManager = StageManager.instance;
        printText = PrintOut.GetComponent<TextMeshProUGUI>();

        if(speedInputField)
        {
            speedInputField = SpeedInput.GetComponent<TMP_InputField>();
        }
            Boss2.SetActive(false);
            StartCoroutine(GameRoutine());
    }

    private void Update()
    {
        if (GameStart)
        {
            tiltTimer += Time.deltaTime;
            UpdateUI();

            // 속도 증가
            if (tiltTimer >= 60)
            {
                stageManager.MiniGameResult(true);
                GameStart = false;
            }
            else if (tiltTimer >= 50)
            {
                moveSpeed = 4f;
            }
            else if (tiltTimer >= 40)
            {
                moveSpeed = 7f;
            }
            else
            {
                moveSpeed = 6f;
            }
        }
    }

    private void UpdateUI()
    {
        printText.text = $"{tiltTimer:F1}/{end}";
    }

    private IEnumerator GameRoutine()
    {
        yield return new WaitForSeconds(3f);
        GameStart = true;

        while (GameStart)
        {
            // 새로운 한 바퀴가 시작될 때 무작위 발사체 선택
            currentBallPrefab = BallPreFab[Random.Range(0, BallPreFab.Length)];

            for (int i = 0; i < TeleportPoint.Length; i++)
            {
                int randomIndex1 = Random.Range(0, TeleportPoint.Length);
                int randomIndex2 = Random.Range(0, TeleportPoint.Length);

                // 보스1 순간이동
                Boss.transform.position = TeleportPoint[randomIndex1].transform.position;

                // 보스2 활성화 및 순간이동
                if (tiltTimer >= 50)
                {
                    Boss2.SetActive(true);
                    Boss2.transform.position = TeleportPoint[randomIndex2].transform.position;
                }

                // 0.5초 대기 후 발사
                yield return new WaitForSeconds(0.5f);

                BossShootAtPlayer(Boss);

                if (tiltTimer >= 50)
                {
                    BossShootAtPlayer(Boss2);
                }

                // 나머지 대기 시간
                yield return new WaitForSeconds(PatternSpeed - 0.5f);
            }
        }
    }

    private void BossShootAtPlayer(GameObject boss)
    {
        Vector3 direction = (Player.transform.position - boss.transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);

        GameObject ball = PoolManager.Instance.GetObject(currentBallPrefab, boss.transform.position, rotation);

        Ball ballScript = ball.GetComponent<Ball>();
        if (ballScript)
        {
            ballScript.SetSpeed(moveSpeed);
        }
    }
}