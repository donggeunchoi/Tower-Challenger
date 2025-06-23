using UnityEngine;

public class GroundTileSpawner : MonoBehaviour
{
    public GameObject groundTilePrefab;
    public GameObject holeTilePrefab;
    public int initialTileCount = 10;
    public float tileWidth = 5f;
    public float tileY = -3.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float spawnX = 0f;

        for (int i = 0; i < initialTileCount; i++)
        {
            bool isHole = Random.value < 0.1f;
            GameObject prefab = isHole ? holeTilePrefab : groundTilePrefab;
            GameObject tile = Instantiate(prefab, new Vector3(spawnX, tileY, 0), Quaternion.identity);
            spawnX += tileWidth;
        }
    }
}