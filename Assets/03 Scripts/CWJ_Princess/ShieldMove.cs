using UnityEngine;
using UnityEngine.InputSystem;

public class ShieldMove : MonoBehaviour
{
    public int graveityScale;
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 screenPos = context.ReadValue<Vector2>();

        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        transform.position = worldPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }

    }
}