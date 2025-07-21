using System.Collections.Generic;
using UnityEngine;

public class WallPool : MonoBehaviour
{
    public static WallPool Instance;
    
    public GameObject wallPrefab;
    public Transform wallContainer;
    public int poolSize;
    
    private Queue<GameObject> wallPool = new Queue<GameObject>();
    private void Awake()
    {
        Instance = this;
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject wall = Instantiate(wallPrefab, wallContainer);
            wall.SetActive(false);
            wallPool.Enqueue(wall);
            
        }
    }

    public GameObject GetWall()
    {
        if (wallPool.Count > 0)
        {
            GameObject wall = wallPool.Dequeue();
            wall.SetActive(true);
            return wall;
        }
        else
        {
            GameObject wall = Instantiate(wallPrefab, wallContainer);
            return wall;
        }
    }

    public void ReturnWall(GameObject wall)
    {
        wall.SetActive(false);
        wallPool.Enqueue(wall);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
