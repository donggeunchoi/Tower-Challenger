using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CatController : MonoBehaviour
{
    public float jumpForce = 10f;         // 점프 힘
    public float moveSpeed = 2f;          // 좌우 이동 속도
    public float wallBounceForce = 5f;    // 벽에 닿았을 때 튕기는 힘
    public float stopTime = 2f;           // 멈춰있는 시간
    public int jumpCount = 3;             // 점프 횟수

    private Rigidbody2D _rigidbody;
    private bool _isJumping = false;
    private bool _isTouchingWall = false;
    private int _moveDirection = 1;        // 1이면 오른쪽, -1이면 왼쪽
    private bool _isWaitingForInput = true;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_isTouchingWall || _isWaitingForInput)
            {
                Jump();
            }
        }
    }

    void Move()
    {
        if (!_isTouchingWall)
        {
            _rigidbody.linearVelocity = new Vector2(moveSpeed * _moveDirection, _rigidbody.linearVelocity.y);
        }
    }

    void Jump()
    {
        _rigidbody.gravityScale = 1; // 중력 적용 시작
        _isTouchingWall = false;
        _isWaitingForInput = false;

        // 반사 방향으로 튕겨 올라감
        _rigidbody.linearVelocity = new Vector2(wallBounceForce * _moveDirection, jumpForce);

        // 고양이 방향 반전
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * _moveDirection;
        transform.localScale = scale;
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
}
  