using System.Collections.Generic;
using UnityEngine;

public class EnemyPos : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    private SpriteRenderer spriteRenderer;

    private int randomEnemy; // 프리펩을 랜덤으로
    private float cameraHeight;
    private float cameraWidth;
    private int side;

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
        if (Input.GetButtonDown("Jump"))
        {
            if (enemyPrefabs.Length == 0)
            {
                return;
            }
            randomEnemy = Random.Range(0, enemyPrefabs.Length);
            PrefabInit();
        }
    }

    private void PrefabInit()
    {
        Vector3 enemyPos = SpawnPos();
        GameObject enemySprite = Instantiate(enemyPrefabs[randomEnemy], enemyPos, Quaternion.identity);
        spriteRenderer = enemySprite.GetComponentInChildren<SpriteRenderer>();

        // 왼쪽이거나 x축 절반을 기준으로 0보다 작은 위치에서 생성된다면
        if (side == 0 || (side == 2 && enemyPos.x < camPos.x))
        {
            spriteRenderer.flipX = true;
        }
    }

    private void CameraPos()
    {
        cameraHeight = cam.orthographicSize;
        cameraWidth = (cameraHeight * 2) * cam.aspect;
    }

    private Vector3 SpawnPos()
    {
        float xMin = camPos.x - cameraWidth / 2;
        float xMax = camPos.x + cameraWidth / 2;
        float yMin = camPos.y;
        float yMax = camPos.y + cameraHeight;

        float randomX = Random.Range(xMin, xMax);
        float randomY = Random.Range(yMin, yMax);

        side = Random.Range(0, 3); // 3방향만 왼 , 오 , 위

        switch(side)
        {
            case 0: // 0이면 왼쪽에서 랜덤 위치에 생성
                return new Vector3(xMin - 1, randomY, 0f);

            case 1: // 1이면 오른쪽에서 랜덤 위치에 생성
                return new Vector3(xMax + 1, randomY, 0f);

            case 2: // 2라면 위에서 랜덤 위치에 생성
                return new Vector3(randomX, yMax + 1, 0f);
        }

        return Vector3.zero;
    }
}
