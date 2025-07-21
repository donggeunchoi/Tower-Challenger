using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Transform cameraTransform;
    public float spawnIntervalY;
    public int initialCount;
    
    public float minX = -4f;
    public float maxX = 4f;
    public float positionY;
    
    private float lastSpawny = 0f;
    private List<GameObject> activeObstacles = new List<GameObject>();

    [Header("벽 장애물 설정")] 
    public float wallLeft;
    public float wallRight;

    [Range(0f, 1f)] 
    public float wallObstacleChance = 0.1f;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeLoop();
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraTransform.position.y > lastSpawny + spawnIntervalY)
        {
            float y = lastSpawny + spawnIntervalY;
            SpawnRandom(y);
            lastSpawny = y;
        }
    }

    private void InitializeLoop()
    {
        foreach (GameObject obstacles in activeObstacles)
        {
            ObstaclePoolManager.Instance.ReturnObstacle(obstacles);
        }

        activeObstacles.Clear();

        lastSpawny = 0f;

        for (int i = 0; i < initialCount; i++)
        {
            float y = i * spawnIntervalY;
            SpawnRandom(y);
            lastSpawny = y;
        }
    }

    public void SpawnRandom(float yPositon)
    {
        bool chooseSpinner = Random.value < 0.5f;
        GameObject obstacle;
        float ySetting = yPositon + positionY;

        if (Random.value < wallObstacleChance)
        {
            obstacle = ObstaclePoolManager.Instance.GetWall();
            float randomX = (Random.value < 0.5f) ? wallLeft : wallRight;
            obstacle.transform.position = new Vector3(randomX, ySetting, 0);
        }
        else
        {
            if (chooseSpinner)
            {
                obstacle = ObstaclePoolManager.Instance.GetSine();
            }
            else
            {
                obstacle = ObstaclePoolManager.Instance.GetMove();
            }
            
           
            float randomX = Random.Range(minX, maxX);
            obstacle.transform.position = new Vector3(randomX, ySetting, 0);
            
        }
        
        obstacle.SetActive(true);
        activeObstacles.Add(obstacle);
    }

    public void ResetTable()
    {
        InitializeLoop();
    }
}
