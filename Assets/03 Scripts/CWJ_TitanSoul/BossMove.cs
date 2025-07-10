using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D target;

    private Rigidbody2D rd;
    private SpriteRenderer sr;

    private bool isdead;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        this.transform.position = new Vector2(-1, 8);
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        BossFlip();
    }

    private void Move()
    {
        Vector2 vecDir = target.position - rd.position;
        Vector2 vexNext = vecDir.normalized * moveSpeed * Time.deltaTime;

        rd.MovePosition(rd.position + vexNext);
    }

    private void BossFlip()
    {
        if (this.transform.position.x <= 0)
        {
            sr.flipX = true;
        }
    }
}
