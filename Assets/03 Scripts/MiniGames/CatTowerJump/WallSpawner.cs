using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(WallPool))]
public class WallSpawner : MonoBehaviour
{
    [Header("References")] 
    private WallPool pool;
    public Transform cameraTransform;

    [Header("Looper Settings")] 
    public int initialCount = 12;
    public float spawnIntervaly;
    public float leftX = -4f;
    public float rightX = 4f;

    private struct WallRow
    {
        public GameObject leftWall;
        public GameObject rightWall;

        public WallRow(GameObject left, GameObject right)
        {
            leftWall = left; 
            rightWall = right;
        }
    }

    private Queue<WallRow> spawnQueue = new Queue<WallRow>();
    private float lastSpawnY = 0f;

    private void Awake()
    {
        pool = GetComponent<WallPool>();
    }

    private void Start()
    {
        for (int i = 0; i < initialCount; i++)
        {
            float y = i *  spawnIntervaly;
            GameObject leftWall = pool.GetWall();
            GameObject rightWall = pool.GetWall();
            
            PositionWall(leftWall, rightWall, y);
            spawnQueue.Enqueue(new WallRow(leftWall, rightWall));
            lastSpawnY = y;
            
        }
        // for (int i = 0; i < 6; i++)
        // {
        //     float yPos = i * spawnIntervaly;
        //     SpawnWalls(yPos);
        //     lastSpawnY = yPos;
        // }
    }
    // Update is called once per frame
    void Update()
    {
        WallRow front = spawnQueue.Peek();
        if (front.leftWall.transform.position.y < cameraTransform.position.y - spawnIntervaly)
        {
            // 큐에서 제거
            spawnQueue.Dequeue();

            // 새로운 Y 계산
            lastSpawnY += spawnIntervaly;

            // 같은 쌍을 가장 위로 재배치
            PositionWall(front.leftWall, front.rightWall, lastSpawnY);

            // 다시 큐에 넣어 순환 유지
            spawnQueue.Enqueue(front);
        }
    }

    private void PositionWall(GameObject leftWall, GameObject rightWall, float y)
    {
        leftWall.transform.position  = new Vector3(leftX,  y, 0f);
        rightWall.transform.position = new Vector3(rightX, y, 0f);
        leftWall.SetActive(true);
        rightWall.SetActive(true);
    }
}
