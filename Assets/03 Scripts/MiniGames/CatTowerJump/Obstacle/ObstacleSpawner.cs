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
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // for (int i = 0; i < initialCount; i++)
        // {
        //     float y = i * spawnIntervalY;
        //     SpawnRandom(y);
        //     lastSpawny = y;
        // }

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
        
        
        if (chooseSpinner)
        {
            obstacle = ObstaclePoolManager.Instance.GetSine();
        }
        else
        {
            obstacle = ObstaclePoolManager.Instance.GetMove();
        }
        
        float ySetting = yPositon + positionY;
        float randomX = Random.Range(minX, maxX);
        obstacle.transform.position = new Vector3(randomX, ySetting, 0);
    }

    public void ResetTable()
    {
        InitializeLoop();
    }
}
