using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public Joystick joyStick;

    private Rigidbody2D _rigid;
    [SerializeField] private SpriteRenderer playerRenderer;

    public Vector2 vecMove;
    public float moveSpeed;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }  

    private void FixedUpdate()
    {
        Vector2 nextMove = vecMove.normalized * moveSpeed * Time.fixedDeltaTime;
        _rigid.MovePosition(_rigid.position + vecMove * nextMove);
    }

    public void OnMove(InputValue value)
    {
        vecMove = value.Get<Vector2>();
    }
}
