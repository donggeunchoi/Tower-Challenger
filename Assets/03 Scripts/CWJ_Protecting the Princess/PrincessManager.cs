using UnityEngine;

public class PrincessManager : MonoBehaviour
{
    public static PrincessManager princessInstance;

    [SerializeField] private ShieldMove shieldMove;
    [SerializeField] private EnemyPos enemyPos;

    public float moveSpeed;

    private void Awake()
    {
        if (princessInstance == null)
        {
            princessInstance = this;
        }
    }
}
