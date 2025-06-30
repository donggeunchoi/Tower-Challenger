using TMPro;
using UnityEngine;
using TMPro;

public class WalkTheStorkGameManager : MonoBehaviour
{
    [Header("회전 관련")]
    public float rotationSpeed = 400f;      // 키 입력에 따른 회전 가속도
    public float tiltAmount = 200f;         // 꺾기 입력 시 추가 회전량

    [Header("자동 흔들림")]
    public float autoTiltSpeed;             // 흔들림 속도 (주기)
    public float autoTiltRange;             // 흔들림 범위 (세기)
    private float tiltTimer;                // 시간 누적용 타이머

    [Header("기울기 중력 효과")]
    public float gravityTiltStrength = 10f; // 기울기에 따른 자연스러운 낙하 회전 효과

    [Header("감속 관련")]
    public float angularDamping = 1.5f;     // 회전 감속 계수 (마찰처럼 작용)

    [Header("부위")]
    public GameObject Man;
    public GameObject Body;                 // 몸통 회전 대상
    public GameObject Head;                 // 머리
    public GameObject Leg;                  // 다리
    public GameObject Hand;                 // 손

    [Header("출력 UI")]
    public GameObject PrintOut;             // 거리 출력용 UI 오브젝트

    // 내부 상태
    private float currentAngle = 0f;        // 현재 회전 각도
    private float angularVelocity = 0f;     // 현재 회전 속도

    // 바람/난이도 조절 관련
    private float autoRotate = 0f;          // 바람처럼 지속적으로 적용되는 회전력
    private float resetTimer = 0f;          // 난이도 조절용 타이머
    private float resetClock = 0f;          // 다음 난이도 조절까지 시간

    private float Goal;                     // 목표 거리 (난이도별)
    private TextMeshProUGUI printText;      // 출력 텍스트 캐싱

    private Vector3 manStartPos = new Vector3(-12f, -1.5f, 0f);
    private Vector3 manEndPos = new Vector3(-5f, -1.5f, 0f);
    private float moveDuration = 4f; // 이동 시간
    private float moveTimer = 0f;    // 이동 경과 시간

    private int Lv = 1;                     // 현재 난이도 레벨
    private bool GameStart = false;
    private void Start()
    {
        printText = PrintOut.GetComponent<TextMeshProUGUI>();
        // 난이도에 따라 목표 거리 설정
        Goal = Lv switch
        {    
            1 => 30,
            2 => 50,
            3 => 100
        };

        // UI 텍스트 캐싱
        
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        if (!GameStart)
        {
            if (Man != null)
            {
                moveTimer += deltaTime;
                float t = Mathf.Clamp01(moveTimer / moveDuration); // 0~1
                Man.transform.position = Vector3.Lerp(manStartPos, manEndPos, t);

                if (t >= 1f)
                {
                    GameStart = true; // 이동 완료 후 게임 시작
                }
            }
            return;
        }
        if (GameStart)
        {
            UpdateUI();                     // 현재 거리 표시
            HandleInput(deltaTime);        // 사용자 입력 처리
            ApplyAutoTilt(deltaTime);      // 자동 흔들림 적용
            ApplyGravityTilt(deltaTime);   // 기울기 중력 효과
            ApplyDamping(deltaTime);       // 회전 감속 적용
            UpdateAngle(deltaTime);        // 회전 각도 갱신 및 리미트 체크
            ApplyRotationToParts();        // 회전값을 각 오브젝트에 적용
            HandleDifficultyScaling(deltaTime); // 난이도 랜덤 변화
        }
    }

    // UI 업데이트
    private void UpdateUI()
    {
        printText.text = $"{tiltTimer:F1}/{Goal}";
    }

    // 키 입력에 따라 회전 가속도 적용
    private void HandleInput(float deltaTime)
    {
        float input = Input.GetAxisRaw("Horizontal");
        angularVelocity += -input * rotationSpeed * deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            angularVelocity += (tiltAmount + tiltTimer * 5f); // 시간 누적에 비례해 강도 증가
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            angularVelocity -= (tiltAmount + tiltTimer * 5f);
    }

    // 자동 흔들림에 따른 회전력 적용
    private void ApplyAutoTilt(float deltaTime)
    {
        tiltTimer += deltaTime;
        float autoTilt = Mathf.Sin(tiltTimer * autoTiltSpeed) * autoTiltRange;
        angularVelocity += autoTilt * deltaTime;
        angularVelocity += autoRotate; // 바람 효과
    }

    // 현재 각도에 따른 중력 효과 적용
    private void ApplyGravityTilt(float deltaTime)
    {
        angularVelocity += currentAngle * gravityTiltStrength * deltaTime;
    }

    // 회전 감속 적용 (점점 느려지게)
    private void ApplyDamping(float deltaTime)
    {
        angularVelocity *= Mathf.Exp(-angularDamping * deltaTime);
    }

    // 회전 각도 갱신 및 넘어짐 조건 체크
    private void UpdateAngle(float deltaTime)
    {
        currentAngle += angularVelocity * deltaTime;
        currentAngle = Mathf.Clamp(currentAngle, -179f, 179f);

        // 너무 기울어졌을 경우 리셋
        if (Mathf.Abs(currentAngle) >= 95f)
        {
            currentAngle = 0f;
            angularVelocity = 0f;
        }
    }

    // 회전값을 오브젝트들에 적용
    private void ApplyRotationToParts()
    {
        if (Body != null)
            Body.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);

        float minorTilt = currentAngle * -0.05f; // 부드러운 보조 움직임

        if (Head != null)
            Head.transform.rotation = Quaternion.Euler(0f, 0f, minorTilt);
        if (Leg != null)
            Leg.transform.rotation = Quaternion.Euler(0f, 0f, minorTilt);
        if (Hand != null)
            Hand.transform.rotation = Quaternion.Euler(0f, 0f, minorTilt);
    }

    // 일정 시간마다 자동 흔들림 강도와 바람 효과 갱신
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
}