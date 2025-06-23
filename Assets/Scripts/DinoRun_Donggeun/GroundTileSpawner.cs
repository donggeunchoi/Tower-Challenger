using System.Collections.Generic;
using UnityEngine;

public class GroundTileSpawner : MonoBehaviour
{
    public GameObject groundTilePrefab;
    public GameObject holeTilePrefab;
    public int initialTileCount = 10;
    public float tileWidth = 5f;
    public float tileY = -3.5f;
    public Transform groundsContainer;
    private bool lastHole = false;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float spawnX = 0f;
        for (int i = 0; i < initialTileCount; i++)
        {
            bool isHole;

            if (lastHole)
            {
                Debug.Log("연속구덩이 안나오게 해야쥬?");
                isHole = false;
                lastHole = false;
            }
            else
            {
                isHole = Random.value < 0.1f;
                lastHole = isHole;
            }
            
            GameObject prefab = isHole ? holeTilePrefab : groundTilePrefab;
            GameObject tile = Instantiate(prefab, new Vector3(spawnX, tileY, 0), Quaternion.identity,groundsContainer);
            spawnX += tileWidth;
        }
    }
    
}
