using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    public Rigidbody2D playerRb;
    public float jumpForce = 10f;
    // public float slideDuration = 0.5f;
    private bool isGrounded = true;
    private bool isSliding = false;
    private bool isInvincible = false;
    public Vector3 respawnPosition = new Vector3(-3f, -1.5f, 0f);
    private Vector3 originalScale;
    private Vector3 slideScale;
    
    private int normalLayer;
    private int invincibleLayer;

    void Start()
    {
        normalLayer = LayerMask.NameToLayer("Default");
        invincibleLayer = LayerMask.NameToLayer("InvinciblePlayer");
        respawnPosition = transform.position;
        
        originalScale = transform.localScale;
        slideScale = new Vector3(originalScale.x, originalScale.y * 0.5f, originalScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        // Touch for jump
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, jumpForce);
        }

        if (Input.GetKey(KeyCode.S) && isGrounded)
        {
            transform.localScale = slideScale;
        }
        else
        {
            transform.localScale = originalScale;
        }
    }
    void LateUpdate()
    {
        // Y값이 -5보다 아래로 떨어졌을 경우
        if (transform.position.y < -5f)
        {
            // 떨어지지 않도록 리스폰 위치로 복구
            transform.position = respawnPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle") && !isInvincible)
        {
            DinoMiniGame.Instance.HandleHit();
            StartCoroutine(InvencibilityRoutine());
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone") && !isInvincible)
        {
            DinoMiniGame.Instance.HandleHit();
            transform.position = respawnPosition;
            StartCoroutine(InvencibilityRoutine());
        }
    }

    IEnumerator InvencibilityRoutine()
    {
        isInvincible = true;
        gameObject.layer = invincibleLayer;
        
        float invincibleTime = 3f;
        float elapsed = 0f;
        
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();

        while (elapsed < invincibleTime)
        {
            // 깜빡임 효과 (선택)
            if (sr != null)
                sr.color = new Color(1f, 1f, 1f, Mathf.PingPong(Time.time * 5, 1f));

            elapsed += Time.deltaTime;
            yield return null;
        }

        gameObject.layer = normalLayer;
        // 무적 해제 + 투명도 복구
        if (sr != null)
            sr.color = Color.white;
        
        isInvincible = false;
    }
   
}
