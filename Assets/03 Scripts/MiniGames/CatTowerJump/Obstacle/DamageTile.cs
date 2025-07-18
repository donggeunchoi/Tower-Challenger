using UnityEngine;

public class DamageTile : ObstacleBase
{
    public int damageAmount = 1;
    public bool useTrigger = true;

    protected override void OnUpdateObstacle()
    {
        // 이 장애물은 따로 매 프레임 로직이 없을 수도 있으므로 비워두거나 시각 이펙트 처리
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!useTrigger) return;
        TryDamage(other.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (useTrigger) return;
        TryDamage(collision.gameObject);
    }

    private void TryDamage(GameObject target)
    {
        if (target.CompareTag("Player"))
        {
            // var health = target.GetComponent<PlayerHealth>();
            // if (health != null) health.TakeDamage(damageAmount);
            // else GameManager.Instance.GameOver();
            Debug.Log("충돌됨. 여기서 LP를 깎아야지");
        }
    }
}
