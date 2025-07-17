using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BackGroundPool))]
public class BackGroundSpawner : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform;
    private BackGroundPool pool; // 싱글톤 대신해서 컴포넌트 참조를 해옵니다.

    [Header("Loop Setting")] 
    public int initialCount = 3;
    public float spawnIntervalY;
    
    
    private Queue<GameObject> tileQueue = new Queue<GameObject>();
    private float lastSpawnY;

    private void Awake()
    {
        pool = GetComponent<BackGroundPool>();
    }

    private void Start()
    {
        for (int i = 0; i < initialCount; i++)
        {
            float yPos = i * spawnIntervalY;
            GameObject tile = pool.GetBackGround();
            tile.transform.position = new Vector3(0f, yPos, 0f);
            tileQueue.Enqueue(tile);
            lastSpawnY = yPos;
        }
    }
    // Update is called once per frame
    void Update()
    {
        GameObject front = tileQueue.Peek();
        if (front.transform.position.y < cameraTransform.position.y - spawnIntervalY)
        {
            tileQueue.Dequeue();
            lastSpawnY += spawnIntervalY;
            front.transform.position = new Vector3(0f, lastSpawnY, 0f);
            tileQueue.Enqueue(front);
        }
    }
    
}
