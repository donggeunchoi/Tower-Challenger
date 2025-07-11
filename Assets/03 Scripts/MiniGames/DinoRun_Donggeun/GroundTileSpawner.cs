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
    public int safeTileCount = 4; // 예: 2초 동안 이동할 거리만큼의 타일 개수


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float spawnX = 0f;
        for (int i = 0; i < initialTileCount; i++)
        {
            bool isHole;
            if (i < safeTileCount)
            {
                isHole = false; // 초반 N개는 무조건 땅
                lastHole = false;
            }
            else if (lastHole)
            {
                isHole = false;
                lastHole = false;
            }
            else
            {
                isHole = Random.value < 0.1f;
                lastHole = isHole;
            }

            GameObject prefab = isHole ? holeTilePrefab : groundTilePrefab;
            GameObject tile = Instantiate(prefab, new Vector3(spawnX, tileY, 0), Quaternion.identity, groundsContainer);
            spawnX += tileWidth;
        }
    }
}