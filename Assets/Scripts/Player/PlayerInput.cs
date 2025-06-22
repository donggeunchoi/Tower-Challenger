using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
{
    public Transform player;                // 플레이어 위치
    public RectTransform stick;             // 조이스틱
    private float radius;                   // 조이스틱 이동 반지름
    private Vector2 inputDir = Vector2.zero;// 입력 방향 벡터
    private float deadZone = 0.15f;
    public float speed = 5;

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
}