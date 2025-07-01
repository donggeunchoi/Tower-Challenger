using UnityEngine;
using TMPro;

public class WalkTheStorkGameManager : MonoBehaviour
{
    private enum GameState { Intro, Playing, Outro, Ended }
    private GameState currentState = GameState.Intro;

    [Header("회전 관련")]
    public float rotationSpeed = 400f;
    public float tiltAmount = 200f;
    private float sensitivity = 5f;

    [Header("자동 흔들림")]
    public float autoTiltSpeed;
    public float autoTiltRange;
    private float tiltTimer;

    [Header("기울기 중력 효과")]
    public float gravityTiltStrength = 10f;

    [Header("감속 관련")]
    public float angularDamping = 1.5f;

    [Header("부위")]
    public GameObject Man;
    public GameObject Body, Head, Leg, LF, RF, Hand;

    [Header("출력 UI")]
    public GameObject PrintOut;

    [Header("배경 UI")]
    public GameObject bg1, bg2;
    public float backgroundScrollSpeed = 2f;
    private float backgroundWidth = 20f;

    private float currentAngle = 0f;
    private float angularVelocity = 0f;

    private float autoRotate = 0f;
    private float resetTimer = 0f;
    private float resetClock = 0f;

    private float Goal;
    private TextMeshProUGUI printText;

    private Vector3 manStartPos = new Vector3(-12f, -1.5f, 0f);
    private Vector3 manEndPos = new Vector3(-5f, -1.5f, 0f);
    private Vector3 manEndPos2 = new Vector3(15f, -1.5f, 0f);
    private float moveDuration = 4f;
    private float moveTimer = 0f;

    private int Lv = 1;

    private StageManager stageManager;

    private void Start()
    {
        stageManager = StageManager.instance;
        printText = PrintOut.GetComponent<TextMeshProUGUI>();

        Goal = Lv switch
        {
            1 => 30,
            2 => 50,
            3 => 100,
            _ => 30
        };

        if (Man != null)
            Man.transform.position = manStartPos;

        if (bg1 != null)
        {
            SpriteRenderer sr = bg1.GetComponent<SpriteRenderer>();
            if (sr != null)
                backgroundWidth = sr.bounds.size.x;
        }

        if (bg1 != null && bg2 != null)
        {
            bg1.transform.position = new Vector3(0f, bg1.transform.position.y, bg1.transform.position.z);
            bg2.transform.position = new Vector3(backgroundWidth, bg2.transform.position.y, bg2.transform.position.z);
        }
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        switch (currentState)
        {
            case GameState.Intro:
                HandleIntro(deltaTime);
                break;

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
                break;

            case GameState.Ended:
                // 아무것도 하지 않음 (정지 상태)
                break;
        }
    }

    private void HandleIntro(float deltaTime)
    {
        if (Man == null) return;

        moveTimer += deltaTime;
        float t = Mathf.Clamp01(moveTimer / moveDuration);
        Man.transform.position = Vector3.Lerp(manStartPos, manEndPos, t);

        if (t >= 1f)
            currentState = GameState.Playing;
    }

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

    private void UpdateUI()
    {
        printText.text = $"{tiltTimer:F1}/{Goal}";
    }

    private void HandleInput(float deltaTime)
    {
        float input = Input.GetAxisRaw("Horizontal");
        angularVelocity += -input * rotationSpeed * deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            ApplyTiltInput(true);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            ApplyTiltInput(false);
    }

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

    private void ApplyTiltInput(bool isLeft)
    {
        float balanceShift = LF.transform.localEulerAngles.z;
        if (balanceShift > 180f)
            balanceShift -= 360f;

        float tiltPower = (tiltAmount + tiltTimer * 5f) + Mathf.Abs(balanceShift) * sensitivity;

        angularVelocity += isLeft ? tiltPower : -tiltPower;
    }

    private void ApplyAutoTilt(float deltaTime)
    {
        tiltTimer += deltaTime;
        float autoTilt = Mathf.Sin(tiltTimer * autoTiltSpeed) * autoTiltRange;
        angularVelocity += autoTilt * deltaTime;
        angularVelocity += autoRotate;
    }

    private void ApplyGravityTilt(float deltaTime)
    {
        angularVelocity += currentAngle * gravityTiltStrength * deltaTime;
    }

    private void ApplyDamping(float deltaTime)
    {
        angularVelocity *= Mathf.Exp(-angularDamping * deltaTime);
    }

    private void UpdateAngle(float deltaTime)
    {
        currentAngle += angularVelocity * deltaTime;
        currentAngle = Mathf.Clamp(currentAngle, -179f, 179f);

        bool isOverLimit = currentAngle >= 95f || currentAngle <= -95f;
        if (!isOverLimit) return;

        if (StageManager.instance.stageLP.currentLP == 1)
        {
            stageManager.MiniGameResult(false);
            currentState = GameState.Ended;
            LF.GetComponent<Animator>().enabled = false;
            RF.GetComponent<Animator>().enabled = false;
        }
        else
        {
            currentAngle = 0f;
            angularVelocity = 0f;
            stageManager.MiniGameResult(false);
            
        }
    }

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
