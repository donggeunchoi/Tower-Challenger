using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    private Vector2 moveInput;
    private string lastDirection = "";

    void Update()
    {
        // 방향키 입력
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (moveInput == Vector2.zero)
        {
            // 입력이 없을 경우 Stand 트리거 작동
            if (lastDirection != "Stand")
            {
                ResetAllTriggers();
                animator.SetTrigger("Stand");
                lastDirection = "Stand";
            }
            return;
        }

        // 수직 방향 우선
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
    }
}