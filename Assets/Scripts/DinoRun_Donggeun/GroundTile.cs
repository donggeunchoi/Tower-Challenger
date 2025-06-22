using UnityEngine;

public class GroundTile : MonoBehaviour
{

    public float tileWidth = 10f;
    public float moveSpeed = 5f;
    
    [Header("타일 전환용")]
    public SpriteRenderer spriteRenderer;
    public Collider2D groundCollider;
    public bool isHole = false;
    
    void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        tileWidth = spriteRenderer.bounds.size.x;
    }
    void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RespawnZone"))
        {
            Reposition();
        }
    }

    void Reposition()
    {
        if (spriteRenderer == null)
        {
            Debug.LogWarning("❗ spriteRenderer가 이미 파괴되어 있음 — 리스폰 중단");
            return;
        }
        
        float rightMost = GetRightMostX(this.gameObject);
        transform.position = new Vector3(rightMost + tileWidth, transform.position.y, transform.position.z);
        
        float GetRightMostX(GameObject exclude)
        {
            GroundTile[] allTiles = FindObjectsOfType<GroundTile>();
            float maxX = float.MinValue;
            foreach (var tile in allTiles)
            {
                if (tile == exclude) continue;
                if (tile.transform.position.x > maxX)
                    maxX = tile.transform.position.x;
            }

            return maxX;
        }
    }
}
