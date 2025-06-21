using UnityEngine;
using UnityEngine.EventSystems;

// 조이스틱 입력을 처리하는 스크립트
public class PlayerInput : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
{
    public Transform player;        // 플레이어 위치
    public RectTransform stick;     // 조이스틱 스틱(움직이는 부분)
    private Vector2 startPos;       // 조이스틱 스틱의 시작 위치
    private float radius;           // 조이스틱 이동 반지름(최대 이동 거리)
    private Vector2 inputDir = Vector2.zero; // 입력 방향 벡터

    void Start()
    {
        startPos = stick.anchoredPosition;                         // 스틱의 초기 위치 저장
        radius = ((RectTransform)stick.parent).sizeDelta.x * 0.5f; // 스틱의 반지름 계산 (배경의 절반 크기)
    }

    void Update()
    {
        // inputDir을 이용해서 플레이어 이동
        if (player != null)
        {
            player.Translate(new Vector3(inputDir.x, inputDir.y, 0) * Time.deltaTime * 5f);
        }
    }

    // 터치가 시작될 때 호출
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData); // 터치 시작 시에도 입력 처리
    }

    // 드래그 중일 때 호출
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        // 터치 위치를 조이스틱 배경의 로컬 좌표로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)stick.parent, eventData.position, eventData.pressEventCamera, out pos);
        Vector2 dir = pos.normalized;                 // 방향 벡터 계산
        float dist = Mathf.Min(pos.magnitude, radius);// 반지름을 넘지 않도록 제한
        stick.anchoredPosition = dir * dist;          // 스틱의 방향 * 거리만큼 이동
        inputDir = dir;                               // 입력 방향 저장
    }

    // 드래그가 끝났을 때 호출
    public void OnEndDrag(PointerEventData eventData)
    {
        ResetStick(); // 스틱 위치 및 입력 초기화
    }

    // 터치가 끝났을 때 호출
    public void OnPointerUp(PointerEventData eventData)
    {
        ResetStick(); // 스틱 위치 및 입력 초기화
    }

    // 스틱 위치와 입력값을 초기화하는 함수
    private void ResetStick()
    {
        stick.anchoredPosition = startPos; // 스틱을 원래 위치로 되돌림
        inputDir = Vector2.zero;           // 입력 방향 초기화
    }
}