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
    public float PatternSpeed = 1.5f;

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
    private Animator animator;
    private Animator animator2;

    private GameObject currentBallPrefab;
    bool i = false;

    void Start()
    {
        animator = Boss.GetComponent<Animator>();
        animator2 = Boss2.GetComponent<Animator>();

        stageManager = StageManager.instance;
        printText = PrintOut.GetComponent<TextMeshProUGUI>();
        if (speedInputField)
        {
            speedInputField = SpeedInput.GetComponent<TMP_InputField>();
        }

        Boss2.SetActive(false);

        if (TeleportPoint.Length < 4)
        {
            Debug.LogError("TeleportPoint 배열 길이가 4 이상이어야 합니다.");
            return;
        }

        StartCoroutine(GameRoutine());
    }

    private void Update()
    {
        if (GameStart)
        {
            tiltTimer += Time.deltaTime;
            UpdateUI();

            if (tiltTimer >= 60)
            {
                stageManager.MiniGameResult(true);
                GameStart = false;
            }
            else if (tiltTimer >= 50)
            {
                moveSpeed = 7f;
            }
            else if (tiltTimer >= 40)
            {
                moveSpeed = 6f;
            }
            else
            {
                moveSpeed = 4f;
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
            int randomIndex1 = Random.Range(0, TeleportPoint.Length);
            int randomIndex2 = Random.Range(0, TeleportPoint.Length);

            // 보스1 애니메이션
            TriggerBossAnimation(animator, randomIndex1);
            Boss.transform.position = TeleportPoint[randomIndex1].transform.position;

            if (tiltTimer >= 50)
            {
                Boss2.SetActive(true);
                yield return null;

                if (randomIndex1 == randomIndex2)
                    randomIndex2 = (randomIndex2 + 1) % TeleportPoint.Length;
    
                TriggerBossAnimation(animator2, randomIndex2);
                Boss2.transform.position = TeleportPoint[randomIndex2].transform.position;

               
               

            }

            yield return new WaitForSeconds(0.5f);

            currentBallPrefab = BallPreFab[Random.Range(0, BallPreFab.Length)];
            BossShootAtPlayer(Boss);

            if (tiltTimer >= 50)
            {
                if (i)
                {
                    BossShootAtPlayer(Boss2);
                }
                else
                {
                    i = true;
                }
            }

            yield return new WaitForSeconds(PatternSpeed - 0.5f);
        }
    }

    private void TriggerBossAnimation(Animator anim, int index)
    {
        anim.ResetTrigger("BossT");
        anim.ResetTrigger("BossR");
        anim.ResetTrigger("BossD");
        anim.ResetTrigger("BossL");

        switch (index)
        {
            case 0: anim.SetTrigger("BossT"); break;
            case 1: anim.SetTrigger("BossR"); break;
            case 2: anim.SetTrigger("BossD"); break;
            case 3: anim.SetTrigger("BossL"); break;
        }
    }

    private void BossShootAtPlayer(GameObject boss)
    {
        Vector3 direction = (Player.transform.position - boss.transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);

        GameObject ball = PoolManager.Instance.GetObject(currentBallPrefab, boss.transform.position, rotation);

        Ball ballScript = ball.GetComponent<Ball>();
        if (ballScript != null)
        {
            ballScript.SetSpeed(moveSpeed);
        }
    }
}