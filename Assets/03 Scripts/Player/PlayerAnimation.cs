using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    private Vector2 moveInput;
    private string lastDirection = "";

    public PlayerInput playerInput;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

#if UNITY_2023_1_OR_NEWER
        if (playerInput == null)
            playerInput = FindFirstObjectByType<PlayerInput>();
#else
        if (playerInput == null)
            playerInput = FindObjectOfType<PlayerInput>();
#endif
    }

    void Update()
    {
        if (playerInput != null && playerInput.isDashing)
        {
            TriggerAnimation("Dash");
        }
        else
        {
            TriggerAnimation("Walk");
        }

        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (moveInput == Vector2.zero)
        {
            if (lastDirection != "Stand")
            {
                ResetAllTriggers();
                animator.SetTrigger("Stand");
                lastDirection = "Stand";
            }
            return;
        }

        if (Mathf.Abs(moveInput.y) > Mathf.Abs(moveInput.x))
        {
            if (moveInput.y > 0)
                TriggerAnimation("UpWalk");
            else
                TriggerAnimation("DownWalk");
        }
        else
        {
            if (moveInput.x > 0)
                TriggerAnimation("RightWalk");
            else
                TriggerAnimation("LeftWalk");
        }
    }

    void TriggerAnimation(string direction)
    {
        if (direction == lastDirection) return;

        ResetAllTriggers();
        animator.SetTrigger(direction);
        lastDirection = direction;
    }

    void ResetAllTriggers()
    {
        animator.ResetTrigger("RightWalk");
        animator.ResetTrigger("LeftWalk");
        animator.ResetTrigger("UpWalk");
        animator.ResetTrigger("DownWalk");
        animator.ResetTrigger("Stand");
        animator.ResetTrigger("Dash");
        animator.ResetTrigger("Walk");
    }
}