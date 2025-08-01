using UnityEngine;
using UnityEngine.InputSystem;

public class ShieldMove : MonoBehaviour
{
    public int graveityScale;
    public void OnMove(InputAction.CallbackContext context)
    {
        if (Camera.main != null)
        {
            Vector2 screenPos = context.ReadValue<Vector2>();

            Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

            transform.position = worldPos;
        }
        else
        {
            Debug.Log(Camera.main);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundManager.instance.PlaySound2D("PGameextinction");
        Destroy(collision.gameObject);
    }
}