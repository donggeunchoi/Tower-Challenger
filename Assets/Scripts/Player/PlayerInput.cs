using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
{
    public Transform player;                // 플레이어 위치
    public RectTransform stick;             // 조이스틱 스틱(움직이는 부분)
    private float radius;                   // 조이스틱 이동 반지름(최대 이동 거리)
    private Vector2 inputDir = Vector2.zero;// 입력 방향 벡터

    void Start()
    {
        // 스틱의 크기를 반지름으로 사용 (정사각형 기준)
        radius = stick.sizeDelta.x * 0.5f;
        stick.anchoredPosition = Vector2.zero; // 항상 중앙에 위치
        inputDir = Vector2.zero;
    }

    void Update()
    {
        if (player != null)
        {
            player.Translate(new Vector3(inputDir.x, inputDir.y, 0) * Time.deltaTime * 5f);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
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
            Vector2 dir = localPoint.normalized;
            float dist = Mathf.Min(localPoint.magnitude, radius);
            stick.anchoredPosition = dir * dist;
            inputDir = dir;
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
}