using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    
    public Animator animator;

    private Vector2 moveInput;
   

    // Update is called once per frame
    void Update()
    {
        // 입력 처리
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // 방향 파라미터 설정
        animator.SetFloat("MoveX", moveInput.x);
        animator.SetFloat("MoveY", moveInput.y);

        // 움직임 여부
        bool isMoving = moveInput.sqrMagnitude > 0f;
        animator.SetBool("IsMove", isMoving);
        
        // 마지막 방향 저장 (정지 상태에서 Idle 방향용)
        if (isMoving)
        {
            animator.SetFloat("LastX", moveInput.x);
            animator.SetFloat("LastY", moveInput.y);
        }
    }
}

