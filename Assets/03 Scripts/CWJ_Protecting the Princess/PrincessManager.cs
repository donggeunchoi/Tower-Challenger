using UnityEngine;

public class PrincessManager : MonoBehaviour
{
    public static PrincessManager princessInstance;

    [SerializeField] private ShieldMove shieldMove;
    [SerializeField] private EnemyPos enemyPos;
    public SpriteRenderer spriteRenderer;

    public float moveSpeed;
    public float enemyTime;

    private void Awake()
    {
        if (princessInstance == null)
        {
            princessInstance = this;
        }
    }

    private void Start()
    {
        spriteRenderer.gameObject.SetActive(false);
    }
}
