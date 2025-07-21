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
    public int initialCount;
    public float spawnIntervaly;
    public float leftX;
    public float rightX;
    
    
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
        InitializeLoop();
    }
    
    // Update is called once per frame
    void Update()
    {
        WallRow front = spawnQueue.Peek();
        if (front.leftWall.transform.position.y < cameraTransform.position.y - spawnIntervaly)
        {
            spawnQueue.Dequeue();
            lastSpawnY += spawnIntervaly;
            PositionWall(front.leftWall, front.rightWall,lastSpawnY);
            spawnQueue.Enqueue(front);
        }
    }

    private void InitializeLoop()
    {
        while (spawnQueue.Count > 0)
        {
            var wall = spawnQueue.Dequeue();
            WallPool.Instance.ReturnWall(wall.leftWall);
            WallPool.Instance.ReturnWall(wall.rightWall);
        }
        lastSpawnY = -20f;
        
        
        for (int i = 0; i < initialCount; i++)
        {
            float y = i *  spawnIntervaly;
            GameObject leftWall = pool.GetWall();
            GameObject rightWall = pool.GetWall();
            PositionWall(leftWall, rightWall, y);
            spawnQueue.Enqueue(new WallRow(leftWall, rightWall));
            lastSpawnY = y;
        }
    }

    private void PositionWall(GameObject leftWall, GameObject rightWall, float y)
    {
        leftWall.transform.position  = new Vector3(leftX,  y, 0f);
        rightWall.transform.position = new Vector3(rightX, y, 0f);
        leftWall.SetActive(true);
        rightWall.SetActive(true);
    }

    public void ResetTable()
    {
        InitializeLoop();
    }
}
