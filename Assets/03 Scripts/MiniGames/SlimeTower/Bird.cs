﻿using UnityEditor;
using UnityEngine;

public class Bird : MonoBehaviour
{
    //새가 좌우로 움직이는 역할을 담당할꺼야
    public float birdSpeed = 2f;
    private bool isMove = true;
    private Vector3 direction;
    public SpriteRenderer BirdSprite;
    
    public BoxCollider2D[] boxColliders;
    public Vector2 birdTransform;
    private const float MARGIN = 2f;


    void Start()
    {
        if (StageManager.instance != null && GameManager.Instance)
        {
            int difficulty = StageManager.instance.difficulty;

            MiniGameData data = GameManager.Instance.miniGameDataList.Find(x => x.name == "SlimeTower" && x.DifficultyLevel == difficulty);

            if (data != null)
            {
                birdSpeed = data.birdSpeed;
            }
        }

        float minX = float.MaxValue;
        float maxX = float.MinValue;

        foreach (BoxCollider2D collider in boxColliders)
        {
            if(collider.bounds.min.x < minX)
                minX = collider.bounds.min.x + MARGIN;
            if(collider.bounds.max.x > maxX)
                maxX = collider.bounds.max.x - MARGIN;
        }
        
        Vector3 pos = transform.position;
        pos.x = Random.Range(minX, maxX);
        transform.position = pos;

        direction = Random.value < 0.5f ? Vector3.right : Vector3.left;
        BirdSprite.flipX = direction.x < 0;
    }
    void Update()
    {
        BirdMove();
    }

    //새가 좌우로 움직이게 만들려면 일단 초기 방향을 잡아주고
    //어떤 속도로 이동할껀지 생각을 해보자.
    public void BirdMove()
    {
        if (isMove)
        {
            transform.Translate(-direction * birdSpeed * Time.deltaTime);
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TurnZone"))
        {
            direction = -direction;
            BirdSprite.flipX = direction.x < 0;
        }
    }
}
