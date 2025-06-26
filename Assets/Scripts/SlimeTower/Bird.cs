using System;
using UnityEditor;
using UnityEngine;

public class Bird : MonoBehaviour
{
    //새가 좌우로 움직이는 역할을 담당할꺼야
   public GameObject bird;
   public float birdSpeed = 5f;
   public Transform target;


   void Start()
   {
       bird =  GameObject.Find("Bird");
   }
   void Update()
   {
       BirdMove();
   }

//새가 좌우로 움직이게 만들려면 일단 초기 위치를 잡아주고
//어떤 속도로 이동할껀지 생각을 해보자.
   public void BirdMove()
   {
       if ()
       {
           
       }

   }
}
