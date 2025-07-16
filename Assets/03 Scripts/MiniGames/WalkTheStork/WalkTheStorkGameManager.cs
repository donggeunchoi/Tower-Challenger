using UnityEngine;
using TMPro;
using System.Collections;

public class WalkTheStorkGameManager : MonoBehaviour
{
    // 게임 상태 정의
    private enum GameState { Intro, Playing, Outro, Ended }
    private GameState currentState = GameState.Intro;

    [Header("회전 관련")]
    public float rotationSpeed = 1000f;          // 좌우 입력 회전 속도
    public float tiltAmount = 500f;             // 기울기 기본 값
    private float sensitivity = 17f;             // 민감도 (기울기 보정용)

    [Header("자동 흔들림")]
    public float autoTiltSpeed = 500;                 // 자동 흔들림 속도 (sin 함수 주기)
    public float autoTiltRange = 500;                 // 자동 흔들림 범위
    private float tiltTimer;                    // 게임 경과 시간 및 sin 계산용

    [Header("기울기 중력 효과")]
    public float gravityTiltStrength = 200f;     // 기울기에 의한 중력 효과 강도

    [Header("감속 관련")]
    public float angularDamping = 5f;         // 감속 계수

    [Header("부위")]
    public GameObject Man;                      // 주인공 캐릭터 오브젝트
    public GameObject Head, Leg, LF, RF, Hand, High; // 캐릭터의 각 부위 오브젝트

    [Header("출력 UI")]
    public GameObject PrintOut;                 // 경과 시간 텍스트 UI

    [Header("배경 UI")]
    public GameObject BG, BG2;                  // 배경 스프라이트 오브젝트
    public float backgroundScrollSpeed = 2f;    // 배경 스크롤 속도
    private float backgroundWidth = 20f;        // 배경 하나의 너비

    private float currentAngle = 0f;            // 현재 회전 각도
    private float angularVelocity = 0f;         // 회전 속도

    private float autoRotate = 0f;              // 랜덤 회전 영향값
    private float resetTimer = 0f;              // 난이도 리셋 타이머
    private float resetClock = 0f;              // 다음 난이도 리셋까지 대기 시간


    private TextMeshProUGUI printText;          // 텍스트 출력용

    private Vector3 manStartPos = new Vector3(-12f, -1.5f, 0f);  // 인트로 게임 시작 위치
    private Vector3 manEndPos = new Vector3(0, -1.5f, 0f);       // 게임 진행 위치
    private Vector3 manEndPos2 = new Vector3(15f, -1.5f, 0f);    // 게임 클리어 퇴장 위치
    private float moveDuration = 4f;            // 인트로/아웃트로 애니 이동 시간

    private float moveTimer = 0f;               // 시간 흐름
    private float Goal = 30;                         // 목표 시간

    private StageManager stageManager;          // 외부 StageManager 참조

    private void Start()
    {
        // StageManager, UI 설정
        stageManager = StageManager.instance;
        printText = PrintOut.GetComponent<TextMeshProUGUI>();

        // 목표 설정


        // 주인공 초기 위치 설정
        if (Man)
            Man.transform.position = manStartPos;

        // 배경 너비 계산
        if (BG)
        {
            SpriteRenderer sr = BG.GetComponent<SpriteRenderer>();
            if (sr)
                backgroundWidth = sr.bounds.size.x;
        }

        // 배경 초기 위치 설정 (무한 스크롤용)
        if (BG && BG2)
        {
            BG.transform.position = new Vector3(0f, BG.transform.position.y, BG.transform.position.z);
            BG2.transform.position = new Vector3(backgroundWidth, BG2.transform.position.y, BG2.transform.position.z);
        }
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        // 상태에 따라 기능 분기
        switch (currentState)
        {
            case GameState.Intro:
                HandleIntro(deltaTime); break;

            case GameState.Playing:
                if (tiltTimer >= Goal)
                {
                    moveTimer = 0f;
                    currentState = GameState.Outro;
                }
                else
                {
                    UpdateUI();
                    HandleInput(deltaTime);
                    ApplyAutoTilt(deltaTime);
                    ApplyGravityTilt(deltaTime);
                    ApplyDamping(deltaTime);
                    UpdateAngle(deltaTime);
                    ApplyRotationToParts();
                    HandleDifficultyScaling(deltaTime);
                    ScrollBackground(deltaTime);
                }
                break;

            case GameState.Outro:
                HandleOutro(deltaTime);
                stageManager.MiniGameResult(true);
                break;

            case GameState.Ended:
                // 아무것도 하지 않음
                break;
        }
    }

    // 인트로: 등장 애니메이션 처리
    private void HandleIntro(float deltaTime)
    {
        if (Man == null) return;

        moveTimer += deltaTime;
        float t = Mathf.Clamp01(moveTimer / moveDuration);
        Man.transform.position = Vector3.Lerp(manStartPos, manEndPos, t);

        if (t >= 1f)
            currentState = GameState.Playing;
    }

    // 아웃트로: 퇴장 애니메이션 처리
    private void HandleOutro(float deltaTime)
    {
        if (Man == null) return;

        moveTimer += deltaTime;
        float t = Mathf.Clamp01(moveTimer / moveDuration);
        Man.transform.position = Vector3.Lerp(manEndPos, manEndPos2, t);

        if (t >= 1f)
        {
            stageManager.MiniGameResult(true);
            enabled = false;
        }
    }

    // UI 업데이트 (경과 시간)
    private void UpdateUI()
    {
        printText.text = $"{tiltTimer:F1}/{Goal}";
    }

    // 키보드 입력 처리 (좌우 화살표)
    private void HandleInput(float deltaTime)
    {
        float input = Input.GetAxisRaw("Horizontal");
        angularVelocity += -input * rotationSpeed * deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            ApplyTiltInput(true);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            ApplyTiltInput(false);
    }

    // 버튼 클릭 처리용
    public void TiltLeftBtnClick()
    {
        if (currentState == GameState.Playing)
            ApplyTiltInput(true);
    }

    public void TiltRightBtnClick()
    {
        if (currentState == GameState.Playing)
            ApplyTiltInput(false);
    }

    // 실제 회전 입력 적용 (기울기 계산 포함)
    private void ApplyTiltInput(bool isLeft)
    {
        float balanceShift = LF.transform.localEulerAngles.z;
        if (balanceShift > 180f)
            balanceShift -= 360f;

        float tiltPower = (tiltAmount + tiltTimer * 5f) + Mathf.Abs(balanceShift) * sensitivity;

        angularVelocity += isLeft ? tiltPower : -tiltPower;
    }

    // 자동 흔들림 (sin 기반)
    private void ApplyAutoTilt(float deltaTime)
    {
        tiltTimer += deltaTime;
        float autoTilt = Mathf.Sin(tiltTimer * autoTiltSpeed) * autoTiltRange;
        angularVelocity += autoTilt * deltaTime;
        angularVelocity += autoRotate;
    }

    // 기울기 중력 적용
    private void ApplyGravityTilt(float deltaTime)
    {
        angularVelocity += currentAngle * gravityTiltStrength * deltaTime;
    }

    // 감속 처리
    private void ApplyDamping(float deltaTime)
    {
        angularVelocity *= Mathf.Exp(-angularDamping * deltaTime);
    }

    // 각도 업데이트 및 낙하 처리
    private void UpdateAngle(float deltaTime)
    {
        currentAngle += angularVelocity * deltaTime;
        currentAngle = Mathf.Clamp(currentAngle, -179f, 179f);

        bool isOverLimit = currentAngle >= 95f || currentAngle <= -95f;
        if (!isOverLimit) return;

        // 앞뒤로 넘어졌을 때의 처리
        if (currentAngle <= -95f)
        {
            if (StageManager.instance != null)
            {
                if (StageManager.instance.stageLP.currentLP == 1)
                {
                    stageManager.MiniGameResult(false);
                    currentState = GameState.Ended;

                    if (LF) LF.GetComponent<Animator>().enabled = false;
                    if (RF) RF.GetComponent<Animator>().enabled = false;

                    if (Leg) StartCoroutine(SmoothRotate(Leg, new Vector3(0f, 0f, -70f), 1f));
                    if (High) StartCoroutine(SmoothRotate(High, new Vector3(0f, 0f, -140), 1f));
                    if (Man)
                    {
                        Vector3 targetEuler = new Vector3(0f, 0f, -140f);
                        Vector3 targetPos = new Vector3(Man.transform.position.x + 4, Man.transform.position.y - 1.5f, Man.transform.position.z);
                        StartCoroutine(SmoothRotateAndMove(Man, targetEuler, targetPos, 1f));
                    }
                }
            }
            else
            {
                Vector3 targetPos = new Vector3(Man.transform.position.x, -3f, Man.transform.position.z);
                currentAngle = 0f;
                angularVelocity = 0f;
                if (StageManager.instance != null)
                {
                    stageManager.MiniGameResult(false);
                }
            }
        }

        if (currentAngle >= 95f)
        {
            if (StageManager.instance != null)
            {
                if (StageManager.instance.stageLP.currentLP == 1)
                {
                    stageManager.MiniGameResult(false);
                    currentState = GameState.Ended;

                    if (LF) LF.GetComponent<Animator>().enabled = false;
                    if (RF) RF.GetComponent<Animator>().enabled = false;

                    if (Leg) StartCoroutine(SmoothRotate(Leg, new Vector3(0f, 0f, 90), 1f));
                    if (Head) StartCoroutine(SmoothRotate(Head, new Vector3(0f, 0f, 90), 1f));
                    if (Man)
                    {
                        Vector3 targetEuler = new Vector3(0f, 0f, 0);
                        Vector3 targetPos = new Vector3(Man.transform.position.x - 3, Man.transform.position.y - 2.5f, Man.transform.position.z);
                        StartCoroutine(SmoothRotateAndMove(Man, targetEuler, targetPos, 1f));
                    }
                }
            }
            else
            {
                Vector3 targetPos = new Vector3(Man.transform.position.x, -3f, Man.transform.position.z);
                currentAngle = 0f;
                angularVelocity = 0f;
                if (StageManager.instance != null)
                {
                    StageManager.instance.MiniGameResult(false);
                }
            }
        }
    }

    // 부드러운 회전 코루틴
    private IEnumerator SmoothRotate(GameObject obj, Vector3 toEuler, float duration)
    {
        if (obj == null) yield break;

        Quaternion startRotation = obj.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(toEuler);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            obj.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.transform.rotation = targetRotation;
    }

    // 부드러운 회전 + 이동 코루틴
    private IEnumerator SmoothRotateAndMove(GameObject obj, Vector3 toEuler, Vector3 toPosition, float duration)
    {
        if (obj == null) yield break;

        Quaternion startRot = obj.transform.rotation;
        Quaternion targetRot = Quaternion.Euler(toEuler);
        Vector3 startPos = obj.transform.position;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            obj.transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            obj.transform.position = Vector3.Lerp(startPos, toPosition, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.transform.rotation = targetRot;
        obj.transform.position = toPosition;
    }

    // 부위들에 각도 반영
    private void ApplyRotationToParts()
    {
        if (High)
            High.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);

        float minorTilt = currentAngle * -0.05f;

        if (Head)
            Head.transform.rotation = Quaternion.Euler(0f, 0f, minorTilt);
        if (Hand)
            Hand.transform.rotation = Quaternion.Euler(0f, 0f, minorTilt);
        if (Leg)
            Leg.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle * -0.30f);
    }

    // 일정 주기로 자동 흔들림 난이도 조절
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

    // 배경 무한 스크롤 처리
    private void ScrollBackground(float deltaTime)
    {
        if (BG || BG2)
        {
            BG.transform.position += Vector3.left * backgroundScrollSpeed * deltaTime;
            BG2.transform.position += Vector3.left * backgroundScrollSpeed * deltaTime;

            if (BG.transform.position.x <= -backgroundWidth)
                BG.transform.position += new Vector3(backgroundWidth * 2f, 0f, 0f);
            if (BG2.transform.position.x <= -backgroundWidth)
                BG2.transform.position += new Vector3(backgroundWidth * 2f, 0f, 0f);
        }
    }
}
