using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    public float spawnInterval;
    public Transform cameraTransform;
    public float leftX = -4f;
    public float rightX = 4f;

    private float lastSpawnY = -5f;



    private void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            float yPos = i * spawnInterval;
            SpawnWalls(yPos);
            lastSpawnY = yPos;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (cameraTransform.position.y > lastSpawnY)
        {
            SpawnWalls(lastSpawnY + spawnInterval);
            lastSpawnY += spawnInterval;
        }
    }

    private void SpawnWalls(float yPos)
    {
        GameObject leftWall = WallPool.Instance.GetWall();
        leftWall.transform.position = new Vector3(leftX, yPos, 0);
        
        GameObject rightWall = WallPool.Instance.GetWall();
        rightWall.transform.position = new Vector3(rightX, yPos, 0);
    }
}
