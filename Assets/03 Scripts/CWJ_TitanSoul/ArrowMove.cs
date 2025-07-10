using UnityEngine;
using UnityEngine.InputSystem;

public class ArrowMove : MonoBehaviour
{
    private Rigidbody2D rd;

    public GameObject prefabArrow;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
       
    }

    private void Update()
    {

    }
    
    public void ActionAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
        if (context.performed)
        {

        }
        if (context.canceled)
        {

        }
    }
}
