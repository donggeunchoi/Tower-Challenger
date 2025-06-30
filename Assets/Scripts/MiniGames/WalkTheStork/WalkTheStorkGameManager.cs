using UnityEngine;
using TMPro;

public class WalkTheStorkGameManager : MonoBehaviour
{
    [Header("회전 관련")]
    public float rotationSpeed = 400f;      // 키 입력에 따른 회전 가속도
    public float tiltAmount = 200f;         // 꺾기 입력 시 추가 회전량
    private float sensitivity = 5f;         //발쏠림 민감도

    [Header("자동 흔들림")]
    public float autoTiltSpeed;             // 흔들림 속도 (주기)
    public float autoTiltRange;             // 흔들림 범위 (세기)
    private float tiltTimer;                // 시간 누적용 타이머

    [Header("기울기 중력 효과")]
    public float gravityTiltStrength = 10f; // 기울기에 따른 자연스러운 낙하 회전 효과

    [Header("감속 관련")]
    public float angularDamping = 1.5f;     // 회전 감속 계수 (마찰처럼 작용)



    [Header("부위")]
    public GameObject Man;                  // 등장 연출용 캐릭터 전체
    public GameObject Body;                 // 몸통 회전 대상
    public GameObject Head;                 // 머리
    public GameObject Leg;                  // 다리
    public GameObject LF, RF;               // 왼발, 오른발
    public GameObject Hand;                 // 손

    [Header("출력 UI")]
    public GameObject PrintOut;             // 거리 출력 UI 오브젝트

    [Header("배경 UI")]
    public GameObject bg1;                  // 배경 이미지 1
    public GameObject bg2;                  // 배경 이미지 2
    public float backgroundScrollSpeed = 2f;// 배경 스크롤 속도
    private float backgroundWidth = 20f;    // 배경 한 장의 너비 (World 단위)

    // 내부 상태
    private float currentAngle = 0f;        // 현재 중심 각도
    private float angularVelocity = 0f;     // 회전 속도

    // 바람/난이도 조절 관련
    private float autoRotate = 0f;          // 지속 회전력 (난이도 요소)
    private float resetTimer = 0f;          // 난이도 갱신용 타이머
    private float resetClock = 0f;          // 다음 난이도 갱신까지 대기 시간

    private float Goal;                     // 목표 거리
    private TextMeshProUGUI printText;      // 텍스트 캐싱

    private Vector3 manStartPos = new Vector3(-12f, -1.5f, 0f); // 등장 시작 위치
    private Vector3 manEndPos = new Vector3(-5f, -1.5f, 0f);    // 등장 완료 위치
    private float moveDuration = 4f;                            // 등장 연출 시간
    private float moveTimer = 0f;                               // 등장 경과 시간

    private int Lv = 1;                    // 난이도 레벨
    private bool GameStart = false;        // 게임 시작 여부

    private void Start()
    {
        // 거리 텍스트 연결
        printText = PrintOut.GetComponent<TextMeshProUGUI>();

        // 목표 거리 설정 (난이도에 따라)
        Goal = Lv switch
        {
            1 => 30,
            2 => 50,
            3 => 100,
            _ => 30
        };

        // 캐릭터 등장 위치 초기화
        if (Man != null)
            Man.transform.position = manStartPos;

        // 배경 스프라이트 크기 계산
        if (bg1 != null)
        {
            SpriteRenderer sr = bg1.GetComponent<SpriteRenderer>();
            if (sr != null)
                backgroundWidth = sr.bounds.size.x;
        }

        // 배경 위치 정렬 (bg1 왼쪽, bg2 오른쪽)
        if (bg1 != null && bg2 != null)
        {
            Vector3 pos1 = new Vector3(0f, bg1.transform.position.y, bg1.transform.position.z);
            Vector3 pos2 = new Vector3(backgroundWidth, bg2.transform.position.y, bg2.transform.position.z);
            bg1.transform.position = pos1;
            bg2.transform.position = pos2;
        }
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        // 게임 시작 전 등장 연출 처리
        if (!GameStart)
        {
            if (Man != null)
            {
                moveTimer += deltaTime;
                float t = Mathf.Clamp01(moveTimer / moveDuration);
                Man.transform.position = Vector3.Lerp(manStartPos, manEndPos, t);

                if (t >= 1f)
                    GameStart = true;
            }
            return;
        }

        // 배경 무한 스크롤 처리
        ScrollBackground(deltaTime);

        // 게임 진행 로직
        UpdateUI();               // UI 업데이트
        HandleInput(deltaTime);   // 키 입력 처리
        ApplyAutoTilt(deltaTime); // 자동 흔들림
        ApplyGravityTilt(deltaTime); // 기울기 중력 효과
        ApplyDamping(deltaTime);  // 회전 감속
        UpdateAngle(deltaTime);   // 각도 갱신
        ApplyRotationToParts();   // 몸 부위에 회전 적용
        HandleDifficultyScaling(deltaTime); // 난이도 조절
    }

    // 거리 출력 텍스트 갱신
    private void UpdateUI()
    {
        printText.text = $"{tiltTimer:F1}/{Goal}";
    }

    // 좌우 화살표 키 입력 처리
    private void HandleInput(float deltaTime)
    {
        float input = Input.GetAxisRaw("Horizontal");
        angularVelocity += -input * rotationSpeed * deltaTime;

        // 왼발의 Z축 회전 각도 (로컬, 오일러 기준)
        float balanceShift = LF.transform.localEulerAngles.z;

        // -180 ~ 180 범위로 보정
        if (balanceShift > 180f)
            balanceShift -= 360f;

        // 왼쪽으로 꺾기: 오른발 뜨기
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // 왼쪽으로 꺾기 → 왼발이 음수일수록 더 많이 흔들려야 하므로 balanceShift가 음수이면 +로 작용하게
            angularVelocity += (tiltAmount + tiltTimer * 5f) + balanceShift * sensitivity;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // 오른쪽으로 꺾기 → 왼발이 양수일수록 더 많이 흔들려야 하므로 balanceShift가 양수이면 +로 작용하게
            angularVelocity -= (tiltAmount + tiltTimer * 5f) + -balanceShift * sensitivity;
        }
    }

    public void TiltLeftBtnClick()
    {
      if (GameStart)
        {
         float balanceShift = LF.transform.localEulerAngles.z;
        if (balanceShift > 180f)
            balanceShift -= 360f;
        angularVelocity += (tiltAmount + tiltTimer * 5f) + balanceShift * sensitivity;
        }
        
    }

    public void TiltRightBtnClick()
    {
        if (GameStart)
        {
            float balanceShift = LF.transform.localEulerAngles.z;
            if (balanceShift > 180f)
                balanceShift -= 360f;
            angularVelocity -= (tiltAmount + tiltTimer * 5f) + -balanceShift * sensitivity;
        }
    }
    // 자동 흔들림 처리 (사인파 기반)
    private void ApplyAutoTilt(float deltaTime)
    {
        
        tiltTimer += deltaTime;
        float autoTilt = Mathf.Sin(tiltTimer * autoTiltSpeed) * autoTiltRange;
        angularVelocity += autoTilt * deltaTime;
        angularVelocity += autoRotate;
    }

    // 기울기에 따른 중력 회전 효과
    private void ApplyGravityTilt(float deltaTime)
    {
        angularVelocity += currentAngle * gravityTiltStrength * deltaTime;
    }

    // 회전 속도 점점 감속시키기
    private void ApplyDamping(float deltaTime)
    {
        angularVelocity *= Mathf.Exp(-angularDamping * deltaTime);
    }

    // 현재 중심 각도 갱신
    private void UpdateAngle(float deltaTime)
    {
        currentAngle += angularVelocity * deltaTime;
        currentAngle = Mathf.Clamp(currentAngle, -179f, 179f);

        // 넘어진 상태라 판단되면 리셋
        if (Mathf.Abs(currentAngle) >= 95f)
        {
            currentAngle = 0f;
            angularVelocity = 0f;
        }
    }

    // 몸/머리/다리/손에 회전 적용
    private void ApplyRotationToParts()
    {
        if (Body != null)
            Body.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);

        float minorTilt = currentAngle * -0.05f;

        if (Head != null)
            Head.transform.rotation = Quaternion.Euler(0f, 0f, minorTilt);
        if (Hand != null)
            Hand.transform.rotation = Quaternion.Euler(0f, 0f, minorTilt);
        if (Leg != null)
            Leg.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle * -0.30f);
    }

    // 난이도 자동 조절 (바람처럼 회전 유도)
    private void HandleDifficultyScaling(float deltaTime)
    {
        resetTimer += deltaTime;
        if (resetTimer > resetClock)
        {
            resetTimer = 0f;
            autoTiltSpeed = Random.Range(5f, 15f);
            autoTiltRange = Random.Range(25f, 75f);
            autoRotate = Random.Range(-0.1f, 0.1f);
            resetClock = Random.Range(1.5f, 5.5f);
        }
    }

    // 배경 무한 스크롤 구현
    private void ScrollBackground(float deltaTime)
    {
        if (bg1 == null || bg2 == null) return;

        bg1.transform.position += Vector3.left * backgroundScrollSpeed * deltaTime;
        bg2.transform.position += Vector3.left * backgroundScrollSpeed * deltaTime;

        if (bg1.transform.position.x <= -backgroundWidth)
            bg1.transform.position += new Vector3(backgroundWidth * 2f, 0f, 0f);
        if (bg2.transform.position.x <= -backgroundWidth)
            bg2.transform.position += new Vector3(backgroundWidth * 2f, 0f, 0f);
    }

}
