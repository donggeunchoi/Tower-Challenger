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
            Debug.Log("시작");
        }
        if (context.performed)
        {
            Debug.Log("하는중");

        }
        if (context.canceled)
        {
            Debug.Log("끝");

        }
    }
}
