using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickMove : MonoBehaviour
{
    public RectTransform joystickBG;        // 조이스틱 배경 핸들 위치 제어
    public RectTransform joystickHandle;    // 캐릭터를 움직이게 하는 조이스틱

    public float radius;    // 조이스틱 배경 안에서 핸들 이동 반경 제어 값

    private Vector2 inputVector;

    public void OnTouchDown(BaseEventData data)
    {
        PointerEventData eventData = data as PointerEventData;
        if (RectTransformUtility.RectangleContainsScreenPoint
            (joystickBG, eventData.position, eventData.enterEventCamera))
        {
            OnDrag(eventData);
        }
    }

    public void OnDrag(BaseEventData data)
    {
        PointerEventData eventData = data as PointerEventData;
        Vector2 localPos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (joystickBG, eventData.position, eventData.enterEventCamera, out localPos);

        localPos = Vector2.ClampMagnitude(localPos, radius);

        joystickHandle.anchoredPosition = localPos;

        inputVector = localPos / radius;
    }

    public void OnTouchUp(BaseEventData eventData)
    {
        inputVector = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
    }
    //public RectTransform joystickHandle;
    //public float handleRange = 100f;
    //private Vector2 inputVector;

    //public Vector2 Direction => inputVector;

    //public void OnDrag(BaseEventData data)
    //{
    //    PointerEventData eventData = data as PointerEventData;
    //    Vector2 pos;
    //    RectTransformUtility.ScreenPointToLocalPointInRectangle(
    //        transform as RectTransform,
    //        eventData.position,
    //        eventData.pressEventCamera,
    //        out pos
    //    );

    //    pos = Vector2.ClampMagnitude(pos, handleRange);
    //    joystickHandle.anchoredPosition = pos;
    //    inputVector = pos / handleRange;
    //}

    //public void OnPointerDown(BaseEventData eventData)
    //{
    //    OnDrag(eventData);
    //}

    //public void OnPointerUp(BaseEventData eventData)
    //{
    //    inputVector = Vector2.zero;
    //    joystickHandle.anchoredPosition = Vector2.zero;
    //}
}
