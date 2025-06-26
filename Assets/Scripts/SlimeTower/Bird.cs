using System;
using UnityEditor;
using UnityEngine;

public class Bird : MonoBehaviour
{
    //새가 좌우로 움직이는 역할을 담당할꺼야
   public float birdSpeed = 2f;
   private bool isMove = true;
   private Vector3 direction;
   public SpriteRenderer BirdSprite;


   void Start()
   {
       direction = Vector3.right;
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
           transform.Translate(direction * birdSpeed * Time.deltaTime);
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
