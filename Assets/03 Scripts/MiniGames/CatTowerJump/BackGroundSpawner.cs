using UnityEngine;

public class BackGroundSpawner : MonoBehaviour
{
    public float spawnInterval;
    public Transform cameraTransform;
    
    private float lastSpawnY = -5f;
    
    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            float yPos = i * spawnInterval;
            SpawnBackTile(yPos);
            lastSpawnY = yPos;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (cameraTransform.position.y > lastSpawnY)
        {
            SpawnBackTile(lastSpawnY + spawnInterval);
            lastSpawnY += spawnInterval;
        }
    }

    private void SpawnBackTile(float yPos)
    {
        GameObject BackGroundTile = BackGroundPool.Instance.GetBackGround();
        BackGroundTile.transform.position = new Vector3(0, yPos, 0);
        
    }
    
    
}
