using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickMove : MonoBehaviour
{
    public RectTransform joystickBG;        // 조이스틱 배경 핸들 위치 제어
    public RectTransform joystickHandle;    // 캐릭터를 움직이게 하는 조이스틱

    public float radius;    // 조이스틱 배경 안에서 핸들 이동 반경 제어 값

    private Vector2 inputVector;

    private void OnDrag(PointerEventData eventData)
    {
        //Vector2 pos = 
    }
}
