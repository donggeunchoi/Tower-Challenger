using UnityEngine;

public class Dino_Ground_Move : MonoBehaviour
{
    public float groundSpeed;

    public Dino_Ground_Move[] groundPrefabs;

    public SpriteRenderer groundSprite;
    public SpriteRenderer holeSprite;

    [HideInInspector] public Collider2D groiundCollider;

    private void Update()
    {
        GroundMove();
    }

    public void GroundMove()
    {
        transform.position += Vector3.left * groundSpeed * Time.deltaTime;

        // 화면 왼쪽 바깥으로 나가면 오른쪽 끝으로 이동
        if (transform.position.x < -12)
        {
            float rightGround = GetRightPosition();
            transform.position = new Vector2(rightGround + Dino_Ground_Manager.instance.spawner.GraoundWidth(), transform.position.y);
        }
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