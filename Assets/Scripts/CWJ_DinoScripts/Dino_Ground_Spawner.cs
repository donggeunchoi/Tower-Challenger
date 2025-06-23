using UnityEngine;

public class Dino_Ground_Spawner : MonoBehaviour
{
    public GameObject groundPrefabs;
    public GameObject groundHolePrefab;

    public int groundCount = 6;
    public float groundWidth = 6;

    private void Start()
    {
        GroundSpawner();
    }

    private void GroundSpawner()
    {
        for (int i = 0; i < groundCount; i++)
        {
            Vector2 pos = new Vector2(i * GraoundWidth(), 0);

            Instantiate(groundPrefabs, pos, Quaternion.identity);
        }
    }

    public float GraoundWidth()
    {
        if (Dino_Ground_Manager.instance.groundSprite != null)
        {
            return Dino_Ground_Manager.instance.groundSprite.bounds.size.x;
        }

        return groundWidth;
    }
}
