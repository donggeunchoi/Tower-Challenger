using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CatController : MonoBehaviour
{
    [Header("점프 세팅")]
    public float jumpForce = 10f;         // 점프 힘
    public float wallBounceForce = 5f;    // 벽에 닿았을 때 튕기는 힘
    public float stopTime = 2f;           // 멈춰있는 시간
    
    [Header("멀티 점프")]
    public int maxJumpCount = 3;             // 점프 횟수
    private int jumpLeft;                 // 남은 점프 홧수
    
    private Rigidbody2D _rigidbody;
    private bool _isTouchingWall = false;
    private int _moveDirection = 1;        // 1이면 오른쪽, -1이면 왼쪽
    // private bool _isWaitingForInput = true;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0;
        jumpLeft = maxJumpCount;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Jump();
        }
    }

    void Jump()
    {
        if (_isTouchingWall || jumpLeft > 0)
        {
            JumpCount();
            
            Debug.Log(jumpLeft);
            _rigidbody.linearVelocity = new Vector2(wallBounceForce * _moveDirection, jumpForce);

            // 고양이 방향 반전
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * _moveDirection;
            transform.localScale = scale;
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _isTouchingWall = true;
            _rigidbody.linearVelocity = Vector2.zero;         // 완전히 멈춤
            _rigidbody.gravityScale = 0;                // 중력 제거
            _moveDirection *= -1;                // 반대 방향으로 준비
        }

        StartCoroutine(Falling(stopTime));
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _isTouchingWall = false;
        }
    }

    public IEnumerator Falling(float time)
    {
        yield return new WaitForSeconds(time);
        _rigidbody.gravityScale = 1;
    }

    public void JumpCount()
    {
        if (_rigidbody.gravityScale == 0)
        {
            _rigidbody.gravityScale = 1f;
        }

        if (_isTouchingWall)
        {
            _isTouchingWall = false;
            jumpLeft = maxJumpCount;
        }
        else
        {
            jumpLeft--;
        }
    }
}
  