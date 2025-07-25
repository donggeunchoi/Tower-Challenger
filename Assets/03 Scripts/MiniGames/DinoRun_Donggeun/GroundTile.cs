﻿using UnityEngine;

public class GroundTile : MonoBehaviour
{

    public float tileWidth = 10f;
    
    [Header("타일 전환용")]
    public SpriteRenderer spriteRenderer;
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
        float speed = DinoMiniGame.instance.CurrentSpeed;
        transform.Translate(Vector3.left * speed * Time.deltaTime);
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
            GroundTile[] allTiles = FindObjectsByType<GroundTile>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            float maxX = float.MinValue;
            foreach (var tile in allTiles)
            {
                if (tile == exclude) continue;
                if (tile.transform.position.x > maxX)
                    maxX = tile.transform.position.x;
            }
            FindAnyObjectByType(typeof(GroundTile));
            return maxX;
        }
    }
}
