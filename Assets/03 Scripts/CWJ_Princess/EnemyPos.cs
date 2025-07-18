using System.Collections.Generic;
using UnityEngine;

public class EnemyPos : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    private SpriteRenderer spriteRenderer;

    private int random;
    private int side;
    private int spawnRandom;
    private float cameraHeight;
    private float cameraWidth;

    public float time;
    private Camera cam;
    private Vector2 camPos;

    private void Start()
    {
        cam = Camera.main;
        camPos = cam.transform.position;

        CameraPos();
    }

    private void Update()
    {
        time += Time.deltaTime;

        SpawnTag();
       // Spawn();
    }

    private void PrefabInit(GameObject _prefab, int _side) // 몬스터 생성 함수
    {
        Vector3 enemyPos = SpawnPos(_side);

        // 프리펩 몬스터 생성(Insantiate(랜덤한프리펩, 랜덤한 위치))
        GameObject enemySprite = Instantiate(_prefab, enemyPos, Quaternion.identity);

        spriteRenderer = enemySprite.GetComponentInChildren<SpriteRenderer>();

        // 왼쪽이거나 x축 절반을 기준으로 0보다 작은 위치에서 생성된다면
        if (side == 0 || (side == 2 && enemyPos.x < camPos.x))
        {
            spriteRenderer.flipX = true;
        }
    }
    private void SpawnTag() // 태그에 알맞는 몬스터 찾기
    {
        if (time >= PrincessManager.princessInstance.enemyTime)
        {
            if (enemyPrefabs.Length == 0)
                return;

            random = Random.Range(0, enemyPrefabs.Length);

            GameObject selectde = enemyPrefabs[random];
            string tag = selectde.tag;

            if (tag == "Fire")
            {
                spawnRandom = Random.Range(1, 4);

                for (int i = 0; i < spawnRandom; i++)
                {
                    side = Random.Range(0, 3);
                    PrefabInit(selectde, side);
                }
            }
            else
            {
                side = Random.Range(0, 2); // 2방향만 왼 , 오 , 위

                PrefabInit(selectde, side);
            }

            time = 0f;
        }

    }

    private void CameraPos()
    {
        cameraHeight = cam.orthographicSize;
        cameraWidth = (cameraHeight * 2) * cam.aspect;
    }

    private Vector3 SpawnPos(int _side)
    {
        float xMin = camPos.x - cameraWidth / 2;
        float xMax = camPos.x + cameraWidth / 2;
        float yMin = camPos.y;
        float yMax = camPos.y + cameraHeight;

        float randomX = Random.Range(xMin, xMax);
        float randomY = Random.Range(yMin, yMax);

        switch(_side)
        {
            case 0: // 0이면 왼쪽에서 랜덤 위치에 생성
                return new Vector3(xMin - 1, randomY, 0f);

            case 1: // 1이면 오른쪽에서 랜덤 위치에 생성
                return new Vector3(xMax + 1, randomY, 0f);
            
            case 2:
                return new Vector3(randomX, yMax + 1, 0);
        }

        return Vector3.zero;
    }

    //public void Spawn()
    //{
    //    foreach (var enemy in enemyPrefabs)
    //    {
    //        string tag = enemy.tag;

    //        if (tag == "Fire")
    //        {
    //            for (int i = 0; i < enemyPrefabs.Length; i++)
    //            {
    //                SpawnTag();
    //            }
    //        }
    //        else
    //        {
    //            SpawnTag();
    //        }
    //    }
    //}

    //private void PrefabInit()
    //{
    //    Vector3 enemyPos = SpawnPos();
    //    GameObject enemySprite = Instantiate(enemyPrefabs[randomEnemy], enemyPos, Quaternion.identity);
    //    spriteRenderer = enemySprite.GetComponentInChildren<SpriteRenderer>();

    //    // 왼쪽이거나 x축 절반을 기준으로 0보다 작은 위치에서 생성된다면
    //    if (side == 0 || (side == 2 && enemyPos.x < camPos.x))
    //    {
    //        spriteRenderer.flipX = true;
    //    }
    //}

    //private void SpawnTag()
    //{
    //    time += Time.deltaTime;

    //    if (time >= PrincessManager.princessInstance.enemyTime)
    //    {
    //        if (enemyPrefabs.Length == 0)
    //        {
    //            return;
    //        }
    //        randomEnemy = Random.Range(0, enemyPrefabs.Length);
    //        PrefabInit();

    //        time = 0f;
    //    }
    //}
}
