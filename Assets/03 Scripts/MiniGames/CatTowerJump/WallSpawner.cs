using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    public float spawnInterval = 1f;
    public Transform cameraTransform;
    public float leftX = -2.5f;
    public float rightX = 2.5f;

    private float lastSpawnY = 0f;
    

    // Update is called once per frame
    void Update()
    {
        if (cameraTransform.position.y > lastSpawnY + spawnInterval)
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
