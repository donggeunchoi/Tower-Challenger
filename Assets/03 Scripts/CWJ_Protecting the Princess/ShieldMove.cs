using UnityEngine;
using UnityEngine.InputSystem;

public class ShieldMove : MonoBehaviour
{
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 screenPos = context.ReadValue<Vector2>();

        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        transform.position = worldPos;
    }    
}
