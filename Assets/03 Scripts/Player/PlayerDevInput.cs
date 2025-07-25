using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDevInput : MonoBehaviour
{
    private Vector2 keyboardInput = Vector2.zero;

    public PlayerInput playerInput;
    public PlayerInteraction interaction;

    private void FixedUpdate()
    {
        if (playerInput == null) return;

        if (keyboardInput.sqrMagnitude > 0.01f)
        {
            playerInput.SetKeyboardInput(keyboardInput);
        }
        else
        {
            playerInput.SetKeyboardInput(Vector2.zero);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed || context.phase == InputActionPhase.Started)
        {
            keyboardInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            keyboardInput = Vector2.zero;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && playerInput != null)
        {
            playerInput.OnClickDash();
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && interaction != null)
        {
            interaction.Interact();
        }
    }
}