using UnityEngine;

public class Dino_Player_Move : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    public LayerMask groundLayer;

    public float jump;
    public float rayLength;

    private bool isGround;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        if (rigidbody2D == null)
        {
            Debug.LogError("Rigidbody2D가 없습니다!");
        }
    }
    public void PlayerJump()
    {
        if (GroundLayer())
        {
            rigidbody2D.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        }

    }

    private bool GroundLayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);

        isGround = hit.collider != null;

        return isGround;
    }
}
