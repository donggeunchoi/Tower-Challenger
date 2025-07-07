using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
//    public Joystick joyStick;

    private Rigidbody2D _rigid;

    public Vector2 vecMove;
    public float moveSpeed;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }  

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        vecMove.x = Input.GetAxis("Horizontal");
        vecMove.y = Input.GetAxis("Vertical");

        // _rigid.AddForce(vecMove);
        // _rigid.linearVelocity = vecMove;
        _rigid.MovePosition(_rigid.position + vecMove * moveSpeed * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
       
    }
}
