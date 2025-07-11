using System.Collections.Generic;
using UnityEngine;

public class EnemyPos : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;

    private List<GameObject>[] pools;

    private void Start()
    {
        pools = new List<GameObject>[enemyPrefabs.Length];

        float x = Random.Range(-11f, 10);
        float y = Random.Range(-1f, 5.5f);

        transform.position = new Vector2(x, y);
    }
}
