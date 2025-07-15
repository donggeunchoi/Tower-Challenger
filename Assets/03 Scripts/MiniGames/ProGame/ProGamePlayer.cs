using System.Collections;
using UnityEngine;

public class ProGamePlayer : MonoBehaviour
{
    private StageManager stageManager;
    private Animator animator;

    private bool isInvincible = false;

    private void Start()
    {
        stageManager = StageManager.instance;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && !isInvincible)
        {
            stageManager.MiniGameResult(false);
            animator.SetTrigger("Hit");
            StartCoroutine(InvincibleRoutine());
        }
    }

    private IEnumerator InvincibleRoutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(1f); // 애니메이션 시간
        isInvincible = false;
    }
}
