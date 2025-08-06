using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FallingBlockPlayer : MonoBehaviour
{
    [Header("플레이어")]
    private bool isInvincible = false;
    private float invincibleTimer = 0f;
    [SerializeField] private float invincibleDuration = 2.0f;
    [SerializeField] Sprite[] playerImage;
    private bool isLeft;
    private bool isRight;

    [Header("타이머")]
    [SerializeField] private Image timerBar;
    [SerializeField] private float currentTime;
    [SerializeField] private float clearTime = 10f;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("오브젝트")]
    [SerializeField] private RectTransform player;  //플레이어
    [SerializeField] private RectTransform ground;  //땅
    [SerializeField] private float playerSpeed;     //이동속도
    [SerializeField] private float gravity = -10f;  //중력
    private float velocityY = 0f;
    
    [Header("캔버스")]
    [SerializeField] private RectTransform backGround;  //백그라운드

    [Header("장애물")]
    [SerializeField] private GameObject[] obstacle;  //장애물 종류
    [SerializeField] private float spawnDelay = 2f;  //장애물이 스폰되는 시간
    [SerializeField] private float spawnDiffcultDelay = 2f;  //난이도가 증가하는 딜레이시간
    [SerializeField] private float gravityScale;

    private List<RectTransform> obstacleList = new List<RectTransform>();  //스폰된 장애물이 보관되는 리스트

    [Header("미니게임 클리어 UI")]
    public GameObject miniGameClearUI;
    public Canvas mainCanvas;
    private bool _clear = false;
    
    private void Start()
    {
        if (StageManager.instance != null && GameManager.Instance)
        {
            int difficulty = StageManager.instance.difficulty;

            if (difficulty == 1)
            {
                gravity = -800; spawnDelay = 0.5f;
            }
            else if (difficulty == 2)
            {
                gravity = -900; spawnDelay = 0.375f;
            }
            else if (difficulty == 3)
            {
                gravity = -1000; spawnDelay = 0.285f;
            }
            else if (difficulty == 4)
            {
                gravity = -1000; spawnDelay = 0.25f;
            }
            else if (difficulty == 5)
            {
                gravity = -800; spawnDelay = 0.1f;
            }
        }

        currentTime = 0f;
        StartCoroutine(ObstacleSpawnRoutine());
        StartCoroutine(AddDifficult());
    }

    private void Update()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0f)
            {
                this.GetComponent<Image>().sprite = playerImage[0];
                isInvincible = false; 
            }
        }

        UpdateTimer();
        ClearStage();          //클리어 판정여부
        PlayerGroundCollider();//바닥 콜라이더 검사
        PlayerMove();          //플레이어 이동
        PlayerWindowCollider();//좌우측 콜라이더 검사
        DestroyObstacle();     //범위밖 장애물 파괴
        CheckCollision();      //충돌검사
    }
    private IEnumerator ObstacleSpawnRoutine()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void OnEventLeft(bool left)
    {
        this.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
        isLeft = left;
    }

    public void OnEventRight(bool right)
    {
        this.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        isRight = right;
    }

    private void PlayerMove()
    {
        if (isLeft)
            player.anchoredPosition += Vector2.left * playerSpeed * Time.deltaTime;
        if (isRight)
            player.anchoredPosition += Vector2.right * playerSpeed * Time.deltaTime;
    }

    private void PlayerGroundCollider()
    {
        velocityY += gravity * Time.deltaTime;                                //중력 시간당 gravity만큼 떨어짐
        player.anchoredPosition += new Vector2(0, velocityY * Time.deltaTime);

        float playerBottom = player.anchoredPosition.y - player.rect.height * 0.5f;                             //플레이어의 중앙에서 플레이어의 높이의 반을 빼준것
        float groundTop = -backGround.anchoredPosition.y - (backGround.rect.height / 2f) + ground.rect.height;  //위와동일한 수식으로 최하단부에서 그라운드높이를 더해준것

        if (playerBottom < groundTop)  //플레이어의 하단부가 땅보다 낮을경우
        {
            player.anchoredPosition = new Vector2(player.anchoredPosition.x, groundTop + (player.rect.height / 2f));  //플레이어의 위치는 콜라이더 Y좌표를 벗어날수 없음

            velocityY = 0f;    //중력 0
        }
    }

    private void PlayerWindowCollider()
    {
        float leftLimit = -backGround.rect.width / 2f + player.rect.width * 0.5f;
        float rightLimit = backGround.rect.width / 2f - player.rect.width * 0.5f;

        Vector2 position = player.anchoredPosition;
        position.x = Mathf.Clamp(position.x, leftLimit, rightLimit);
        player.anchoredPosition = position;
    }

    private void SpawnObstacle()
    {
        float leftLimit = -backGround.rect.width / 2f;
        float rightLimit = backGround.rect.width / 2f;

        float spawnX = Random.Range(leftLimit, rightLimit);
        float spawnY = backGround.rect.height * 0.5f + 50f;

        GameObject obs = Instantiate(obstacle[Random.Range(0,obstacle.Length)], backGround);
        RectTransform obsRct = obs.GetComponent<RectTransform>();
        obsRct.anchoredPosition = new Vector2(spawnX, spawnY);

        obstacleList.Add(obsRct);
    }

    private void DestroyObstacle()
    {
        for (int i = obstacleList.Count - 1; i >= 0; i--)
        {
            RectTransform obs = obstacleList[i];
            obs.anchoredPosition += Vector2.down * -gravity * Time.deltaTime;
            
            if (obs.anchoredPosition.y < -backGround.rect.height * 0.5f - 100f)  // 화면 아래로 나가면 삭제
            {
                Destroy(obs.gameObject);
                obstacleList.RemoveAt(i);
            }
        }
    }

    private void CheckCollision()
    {
        if (currentTime > clearTime)
            return;

        Rect playerRect = new Rect(
        player.anchoredPosition.x - player.rect.width * player.pivot.x,
        player.anchoredPosition.y - player.rect.height * player.pivot.y,
        player.rect.width,
        player.rect.height);

        foreach (RectTransform obs in obstacleList)
        {
            Rect obsRect = new Rect(
            obs.anchoredPosition.x - obs.rect.width * obs.pivot.x,
            obs.anchoredPosition.y - obs.rect.height * obs.pivot.y,
            obs.rect.width,
            obs.rect.height);

            if (!isInvincible && playerRect.Overlaps(obsRect))
            {
                Debug.Log("아야!");
                this.GetComponent<Image>().sprite = playerImage[1];
                isInvincible = true;
                invincibleTimer = invincibleDuration;

                if (StageManager.instance != null)
                    StageManager.instance.MiniGameResult(false);
                break;
            }
        }
    }

    private void ClearStage()
    {
        currentTime += Time.deltaTime;
        if (currentTime > clearTime)
        {
            if (!_clear)
            {
                _clear = true;
                ShowClearUI();
                StartCoroutine(WaitinTime());
            }
            
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
        GameObject miniGameClear = Instantiate(miniGameClearUI,mainCanvas.transform);
        miniGameClear.transform.SetAsLastSibling();
        
        var rt = miniGameClear.GetComponent<RectTransform>();
        rt.pivot = new Vector2(0.5f, 0.5f);         // 하단 중앙
        rt.anchoredPosition = Vector2.zero;       // 캔버스 하단 중앙
        rt.localScale = Vector3.zero;             // 초기 스케일 0
        
        // 3) Scale 애니메이션
        StartCoroutine(ScaleUp(rt, 0.5f));        // 0.5초 동안
    }

    private void UpdateTimer()
    {
        timerText.text = Mathf.Max(0f, (clearTime - currentTime)).ToString("F2");
        timerBar.fillAmount = (clearTime - currentTime) / clearTime;
    }

    private IEnumerator AddDifficult()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDiffcultDelay);
            gravity -= gravityScale;
        }
    }
}
