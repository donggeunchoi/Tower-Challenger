using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    public PlayerInput playerInput;           // PlayerInput 스크립트 참조 (자동 할당)
    public SpriteRenderer spriteRenderer;     // SpriteRenderer 참조 (자동 할당)

    void Start()
    {
        // PlayerInput 자동 할당
        if (playerInput == null)
            playerInput = FindObjectOfType<PlayerInput>();

        // SpriteRenderer 자동 할당
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        // 그래도 못 찾으면, playerInput 안에서 찾아보기
        if (spriteRenderer == null && playerInput != null)
            spriteRenderer = playerInput.GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (playerInput != null && spriteRenderer != null)
            Flip();
    }

    public void Flip()
    {
        Vector2 input = playerInput.FinalInput;

        if (input.x > 0.1f)
            spriteRenderer.flipX = false;
        else if (input.x < -0.1f)
            spriteRenderer.flipX = true;
    }
}