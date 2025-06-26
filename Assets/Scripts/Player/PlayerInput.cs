using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
{
    public Transform player;                // 플레이어 위치
    public RectTransform stick;             // 조이스틱
    private float radius;                   // 조이스틱 이동 반지름
    private Vector2 inputDir = Vector2.zero;// 입력 방향 벡터
    private float deadZone = 0.15f;
    
    [Header("움직임과 대쉬")]
    public float speed = 5;
    public float DashSpeed = 10f;           // 대쉬스피드
    public float DashTime = 5f;           //대쉬지속시간
    public float CooldownTime = 3f;         //쿨타임
    public bool canDash = true;             //대쉬할수 있는 상태
    public bool isDashing = false;          //대쉬중은 아니니까 

    
    void Start()
    {
        radius = stick.sizeDelta.x * 0.5f;     //스틱의 크기의 절반을 반지름으로
        stick.anchoredPosition = Vector2.zero; //중앙 위치
        inputDir = Vector2.zero;               //입력 방향을 초기화
    }

    void Update()
    {   //입력방향이 있을 때 speed만큼 플레이어를 이동시킴
        if (player != null)
        {
            player.Translate(new Vector3(inputDir.x, inputDir.y, 0) * Time.deltaTime * speed);
        }
    }

    public void OnPointerDown(PointerEventData eventData)  //터치가 시작될 때 드래그 처리
    {
        OnDrag(eventData);
    }

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

    public void OnEndDrag(PointerEventData eventData)  //드래그가끝나면 리셋
    {
        ResetStick();
    }

    public void OnPointerUp(PointerEventData eventData)  //터치가끝나면 리셋(한번터치하고 냅두면 계속이동해버림)
    {
        ResetStick();
    }

    private void ResetStick()  //스틱을 중앙으로 초기화 및 입력 초기화
    {
        stick.anchoredPosition = Vector2.zero;
        inputDir = Vector2.zero;
    }

    
    //버튼을 누르면 플레이어 속도가 대쉬 속도로 증가한다.
    //대쉬 지속시간 후에는 원래 속도로 돌아오게 만든다
    //쿨타임 3초동안은 다시 대쉬를 할 수 없어야 한다.
    public void OnClickDash()
    {
        //대쉬할수 있고 대쉬중이 아니라면
        if (canDash && !isDashing)
        {
            //해당 대쉬를 진행
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
    
}