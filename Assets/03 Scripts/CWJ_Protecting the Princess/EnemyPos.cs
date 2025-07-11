using System.Collections.Generic;
using UnityEngine;

public class EnemyPos : MonoBehaviour
{
    // 프리펩을 넣는 변수
    [SerializeField] private GameObject[] enemyPrefabs;

    // 프리펩이 들어있는 
    private List<GameObject>[] pools;

    private int randomEnemy;
    

    private void Start()
    {
        PrefabInit();
    }

    private void Update()
    {
       

        if (Input.GetButtonDown("Jump"))
        {
            randomEnemy = Random.Range(0, enemyPrefabs.Length);
            GetEnemy(randomEnemy);
        }
    }

    private void PrefabInit()
    {
        pools = new List<GameObject>[enemyPrefabs.Length + 1];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    private GameObject GetEnemy(int index)
    {
        GameObject select = null;
        foreach (GameObject enemy in pools[index])
        {
            if (!enemy.activeSelf)
            {
                select = enemy;
                select.SetActive(true);

                break;
            }
        }

        if (select == null)
        {
            // 원본 오브젝트를 복사해서 생성하는 함수
            select = Instantiate(enemyPrefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
