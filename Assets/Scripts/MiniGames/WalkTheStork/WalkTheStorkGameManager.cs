using TMPro;
using UnityEngine;
using TMPro;

public class WalkTheStorkGameManager : MonoBehaviour
{
    [Header("회전 관련")]
    public float rotationSpeed = 400f;      // 입력 시 회전 가속도
    public float tiltAmount = 200f;         // 좌우로 꺾을 때 회전 가속도 증가량

    [Header("자동 흔들림")]
    public float autoTiltSpeed;             // 자동 흔들림 속도
    public float autoTiltRange;             // 자동 흔들림 범위
    private float tiltTimer;                // 흔들림 타이머

    [Header("기울기 중력 효과")]
    public float gravityTiltStrength = 10f; // 기울어진 쪽으로 더 기울어지는 힘

    [Header("감속 관련")]
    public float angularDamping = 1.5f;     // 회전 감속 계수 (마찰 역할)

    [Header("부위")]
    public GameObject Body;                 // 회전을 적용할 목 오브젝트
    public GameObject Head;
    public GameObject Leg;
    public GameObject Hand;


    [Header("출력 UI")]

    public GameObject PrintOut;

    // 내부 상태 값
    private float currentAngle = 0f;        // 현재 회전 각도
    private float angularVelocity = 0f;     // 회전 속도

    // 바람/난이도 조절 관련
    private float autoRotate = 0f;          // 바람처럼 지속적으로 적용되는 회전력
    private float resetTimer = 0f;          // 난이도 타이머
    private float resetClock = 0f;

    private float Goal;// 다음 난이도 조정까지의 시간


    int Lv = 1;

    private void Start()
    {

        if (Lv == 1)
        {
            Goal = 30;
        }
        else if (Lv == 2)
        {
            Goal = 50;
        }
        else if (Lv == 3)
        {
            Goal = 100;
        }
        Debug.Log("");


    }
    void Update()
    {
        PrintOut.GetComponent<TextMeshProUGUI>().text = tiltTimer.ToString("F1") +"/"+ Goal;

        float deltaTime = Time.deltaTime;

        // 자동 흔들림 
        tiltTimer += deltaTime;
        float autoTilt = Mathf.Sin(tiltTimer * autoTiltSpeed) * autoTiltRange;
        angularVelocity += autoTilt * deltaTime;

        //바람처럼 지속적으로 작용하는 회전력
        angularVelocity += autoRotate;

        // 사용자 입력에 따른 회전 가속
        float input = Input.GetAxisRaw("Horizontal");
        angularVelocity += -input * rotationSpeed * deltaTime;

        // 좌우 꺾기
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            angularVelocity += (tiltAmount + tiltTimer*5);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            angularVelocity -= (tiltAmount + tiltTimer*5);

        // 기울어진 각도에 따라 더 기울어지게 만드는 중력 효과
        angularVelocity += currentAngle * gravityTiltStrength * deltaTime;

        // 회전 감속 처리 (자연스러운 정지 효과)
        angularVelocity *= Mathf.Exp(-angularDamping * deltaTime);

        //  회전 각도 갱신
        currentAngle += angularVelocity * deltaTime;

        
        currentAngle = Mathf.Clamp(currentAngle, -179, 179);

        if (Mathf.Abs(currentAngle) >= 95f)
        {
            currentAngle = 0;
            angularVelocity = 0f;
        }
    

        // 실제 오브젝트 회전에 적용 (Neck 오브젝트에만 회전 적용)
        if (Body != null)
        {
            Body.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
        }
        if (Head != null)
        {
            Head.transform.rotation = Quaternion.Euler(0f, 0f, -currentAngle * 0.00001f);
        }
        if (Leg != null)
        {
            Leg.transform.rotation = Quaternion.Euler(0f, 0f, -currentAngle * 0.00001f);
        }
        if (Hand != null)
        {
            Hand.transform.rotation = Quaternion.Euler(0f, 0f, -currentAngle * 0.00001f);
        }

        //  바람 세기 및 자동 흔들림 난이도 변화
        resetTimer += deltaTime;
        if (resetTimer > resetClock)
        {
            resetTimer = 0f;
            autoTiltSpeed = Random.Range(5f, 15f);
            autoTiltRange = Random.Range(25f, 75f);
            autoRotate = Random.Range(-0.1f, 0.1f);
            resetClock = Random.Range(1.5f, 5.5f); // 다음 변경까지 시간
        }
    }

    
}
