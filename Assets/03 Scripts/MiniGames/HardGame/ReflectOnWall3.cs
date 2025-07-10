using UnityEngine;

public class ReflectOnWall3 : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {

            float currentX = transform.eulerAngles.x;

            // x가 85~95도 사이에 있으면 90도로 간주 (애니메이션 트리거 조건)
            if (currentX >= 85f && currentX <= 95f)
            {
                animator?.SetTrigger("Ture"); // 정확한 트리거 이름 확인
            }
            else
            {
                animator?.SetTrigger("False");
            }
            Vector3 rot = transform.eulerAngles;
            rot.x += 180f;
            transform.rotation = Quaternion.Euler(rot);
            // 회전 이전의 값으로 체크


            // 회전 적용
        }
    }
}
