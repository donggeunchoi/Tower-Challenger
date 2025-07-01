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
    public float speed = 5;
    public float DashSpeed = 10f;           // 대쉬스피드
    public float DashTime = 5f;             //대쉬지속시간
    public float CooldownTime = 3f;         //쿨타임
    public bool canDash = true;             //대쉬할수 있는 상태
    public bool isDashing = false;          //대쉬중은 아니니까 
    public SpriteRenderer spriteRenderer;   //방향전환 이미지

    void Start()
    {
        radius = stick.sizeDelta.x * 0.5f;     //스틱의 크기의 절반을 반지름으로
        stick.anchoredPosition = Vector2.zero; //중앙 위치
        inputDir = Vector2.zero;               //입력 방향을 초기화
        spriteRenderer = player.GetComponent<SpriteRenderer>(); //플레이어 스프라이트
    }

    void Update()
    {   //입력방향이 있을 때 speed만큼 플레이어를 이동시킴
        if (player != null)
        {
            player.Translate(new Vector3(inputDir.x, inputDir.y, 0) * Time.deltaTime * speed);
            FlipChange();
        }
    }

    public void OnPointerDown(PointerEventData eventData)  //터치가 시작될 때 드래그 처리
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            stick, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            float dist = Mathf.Min(localPoint.magnitude, radius);  //스틱거리제한
            Vector2 dir = localPoint.normalized;  //방향 계산
            if (dist < radius * deadZone)
            {
                stick.anchoredPosition = Vector2.zero;
                inputDir = Vector2.zero;
            }
            else
            {
                stick.anchoredPosition = dir * dist;
                inputDir = dir;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ResetStick();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ResetStick();
    }

    private void ResetStick()
    {
        stick.anchoredPosition = Vector2.zero;
        inputDir = Vector2.zero;
    }

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

        Debug.Log("달리는 시간이쥬?");
        float originalSpeed = speed;
        speed = DashSpeed;

        yield return new WaitForSeconds(DashTime);

        speed = originalSpeed;
        isDashing = false;
        Debug.Log("달리기 멈추고 쿨타임시간");

        yield return new WaitForSeconds(CooldownTime);
        canDash = true;
    }

    public void FlipChange()
    {
        Vector3 move = new Vector3(inputDir.x, inputDir.y, 0);
        player.Translate(move * Time.deltaTime * speed);

        if (inputDir.x > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (inputDir.x < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }
}