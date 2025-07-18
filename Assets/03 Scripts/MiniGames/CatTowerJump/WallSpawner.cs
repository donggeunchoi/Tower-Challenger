using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

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

    [Header("벽 장애물 설정")] 
    [Range(0f, 1f)]
    public float obstacleChance;
    public bool obstacleLeft = true;
    
    
    private struct WallRow
    {
        public GameObject leftWall;
        public GameObject rightWall;
        public GameObject obstacleWall;

        public WallRow(GameObject left, GameObject right,  GameObject obstacle)
        {
            leftWall = left; 
            rightWall = right;
            obstacleWall= obstacle;
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


            GameObject wallObstacle = null;
            if (Random.value < obstacleChance)
            {
                wallObstacle = ObstaclePoolManager.Instance.GetWall();
            }
            
            PositionWall(leftWall, rightWall, wallObstacle, y);
            spawnQueue.Enqueue(new WallRow(leftWall, rightWall, wallObstacle));
            lastSpawnY = y;
            
        }
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
            PositionWall(front.leftWall, front.rightWall, front.obstacleWall,lastSpawnY);

            // 다시 큐에 넣어 순환 유지
            spawnQueue.Enqueue(front);
        }
    }

    private void PositionWall(GameObject leftWall, GameObject rightWall, GameObject wallObstacle, float y)
    {
        leftWall.transform.position  = new Vector3(leftX,  y, 0f);
        rightWall.transform.position = new Vector3(rightX, y, 0f);
        leftWall.SetActive(true);
        rightWall.SetActive(true);

        if (wallObstacle != null)
        {
            float x = obstacleLeft ?  leftX : rightX;
            wallObstacle.transform.position = new Vector3(leftX,  y, 0f);
            wallObstacle.SetActive(true);
        }
    }
}
