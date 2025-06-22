using UnityEngine;

public class Dino_Ground_Move : MonoBehaviour
{
    public float groundSpeed;
    private float groundWidth;

    public Dino_Ground_Move[] groundPrefabs;

    private SpriteRenderer groundSprite;
    private Collider2D groiundCollider;

    private void Awake()
    {
        groundSprite = GetComponent<SpriteRenderer>();
        groiundCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        groundWidth = GroundWidth();
    }

    private void Update()
    {
        GroundMove();
    }

    public void GroundMove()
    {
        transform.position += Vector3.left * groundSpeed * Time.deltaTime;

        // 화면 왼쪽 바깥으로 나가면 오른쪽 끝으로 이동
        if (transform.position.x < -20)
        {
            float rightGround = GetRightPosition();
            transform.position = new Vector2(rightGround + groundWidth, transform.position.y);
            Debug.Log("타일 재배치됨!");
        }
    }

    private float GroundWidth()
    {
        if (groundSprite != null)
        {
            return groundSprite.bounds.size.x;
        }

        Debug.LogWarning("SpriteRenderer가 없습니다. 기본값 6 사용");
        return 6f;
    }
    private float GetRightPosition()
    {
        groundPrefabs = FindObjectsByType<Dino_Ground_Move>(FindObjectsSortMode.None);
        float maxX = -999f;

        foreach (Dino_Ground_Move tile in groundPrefabs)
        {
            if (tile != this && tile.transform.position.x > maxX)
            {
                maxX = tile.transform.position.x;
            }
        }

        return maxX;
    }
}
