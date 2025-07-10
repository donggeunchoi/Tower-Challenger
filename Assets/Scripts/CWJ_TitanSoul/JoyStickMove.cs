using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStickMove : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private Image joyBgImage;
    [SerializeField] private Image joyHandleImage;

    private double radiusJoy;
    private float deadZone;

    Vector2 touchPos;

    private void Awake()
    {
        joyBgImage = GetComponent<Image>();
        joyHandleImage = transform.Find("JoyStickHandle").GetComponent<Image>();
    }

    private void Start()
    {
        radiusJoy = joyHandleImage.rectTransform.sizeDelta.x * 0.5;
        deadZone = 0.1f;
        touchPos = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
        (
            joyBgImage.rectTransform,   // 플레이어가 터치할 UI의 좌표
            eventData.position,         // 플레이어가 터치한 좌표
            eventData.pressEventCamera, // 플레이어가 터치 했을 때 UI룰 보고 있는 카메라
            out touchPos                // 화면을 기준으로 터치가 된 좌표값을 UI를 기준의 좌표값으로 바꿔 저장
        ))
        {
            float distance = Mathf.Min(touchPos.magnitude, (float)radiusJoy);
            Vector2 direction = touchPos.normalized;

            if (distance < radiusJoy * deadZone)
            {
                joyHandleImage.rectTransform.anchoredPosition = Vector2.zero;
            }
            else
            {
                joyHandleImage.rectTransform.anchoredPosition = direction * distance;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joyHandleImage.rectTransform.anchoredPosition = Vector2.zero;
        touchPos = Vector2.zero;
    }
}
