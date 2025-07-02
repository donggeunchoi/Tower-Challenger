using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
{
    public Transform player;                // 플레이어 위치
    public RectTransform stick;             // 조이스틱
    private float radius;                   // 조이스틱 이동 반지름
    private Vector2 inputDir = Vector2.zero;// 입력 방향 벡터
    private float deadZone = 0.15f;         // 민감도 조정

    [Header("움직임과 대쉬")]
    public float speed = 5;                 // 기본 이동 속도
    public float DashSpeed = 10f;           // 대쉬 스피드
    public float DashTime = 0.2f;           // 대쉬 지속 시간
    public float CooldownTime = 1f;         // 대쉬 쿨타임
    public bool canDash = true;             // 대쉬 가능 여부
    public bool isDashing = false;          // 대쉬 중 여부
    public SpriteRenderer spriteRenderer;   // 방향 전환 이미지


    [SerializeField] private Animator uiRootAnim;
    private Rigidbody2D rb;                 // Rigidbody2D 컴포넌트

    void Start()
    {
        radius = stick.sizeDelta.x * 0.5f;     //스틱의 크기의 절반을 반지름으로
        stick.anchoredPosition = Vector2.zero; //중앙 위치
        inputDir = Vector2.zero;               //입력 방향을 초기화

        if (player != null)
        {
            spriteRenderer = player.GetComponent<SpriteRenderer>(); //플레이어 스프라이트

            player.TryGetComponent<Rigidbody2D>(out rb);
        }
    }

    void FixedUpdate()
    {
        if (player != null && rb != null) //플레이어와 리지드바디가 있을 때만 이동 처리
        {
            float currentSpeed = isDashing ? DashSpeed : speed; // 대쉬 중이면 대쉬 속도
            rb.linearVelocity = inputDir.normalized * currentSpeed;    // 방향 * 속도

            FlipChange(); // 방향 전환 처리
        }
    }

    //터치가 시작될 때 드래그 처리
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    //드래그 처리
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        // 스틱의 RectTransform 기준으로 터치 위치를 변환
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            stick, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            float dist = Mathf.Min(localPoint.magnitude, radius);  //스틱거리제한
            Vector2 dir = localPoint.normalized;  //방향 계산
            if (dist < radius * deadZone) //미세조정값이 들어오면 컨트롤러가 떨리는 것을 방지
            {
                stick.anchoredPosition = Vector2.zero;
                inputDir = Vector2.zero;
            }
            else
            {
                stick.anchoredPosition = dir * dist;  //드래그한 길이계산
                inputDir = dir;  //입력방향 저장
            }
        }
    }

    //드래그가 끝나면 리셋
    public void OnEndDrag(PointerEventData eventData)
    {
        ResetStick();
    }

    //터치가 끝나면 리셋(한번 터치하고 냅두면 계속이동해버림)
    public void OnPointerUp(PointerEventData eventData)
    {
        ResetStick();
    }

    //스틱을 중앙으로 초기화 및 입력 초기화
    private void ResetStick()
    {
        stick.anchoredPosition = Vector2.zero;
        inputDir = Vector2.zero;
    }

    //버튼을 누르면 플레이어 속도가 대쉬 속도로 증가한다.
    //대쉬 지속시간 후에는 원래 속도로 돌아오게 만든다
    //쿨타임 3초동안은 다시 대쉬를 할 수 없어야 한다.
    public void OnClickDash()
    {
        if (canDash && !isDashing)
        {
            StartCoroutine(DashRoutine());
        }
    }

    private IEnumerator DashRoutine()
    {
        isDashing = true;
        canDash = false;

        // 대쉬 시작
        yield return new WaitForSeconds(DashTime);

        // 대쉬 종료
        isDashing = false;

        // 쿨타임 대기
        yield return new WaitForSeconds(CooldownTime);
        canDash = true;
    }

    //입력 방향에 따라 스프라이트 좌우 반전 처리
    public void FlipChange()
    {
        if (spriteRenderer == null) return;

        if (inputDir.x > 0.1f)
            spriteRenderer.flipX = false; // 오른쪽
        else if (inputDir.x < -0.1f)
            spriteRenderer.flipX = true;  // 왼쪽
    }
}