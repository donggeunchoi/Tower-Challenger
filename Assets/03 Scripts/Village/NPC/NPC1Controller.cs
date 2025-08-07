using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class NPC1Controller : NPCBase
{
   public Transform stopPoint1;
   public Transform stopPoint2;
   public float stopRange;
   
   private bool _isPaused = false;
   private bool _turnToRight = false;

   protected override void Update()
   {
      if (_isPaused) return;
      
      if (Vector3.Distance(transform.localPosition, stopPoint1.localPosition) < stopRange || Vector3.Distance(transform.localPosition, stopPoint2.localPosition) < stopRange)
      {
         StartCoroutine(StopTemporarily());
         return;
      }
      
      base.Update();
   }
   protected override IEnumerator WaitBeforeTurn(bool turnToRight)
   {
      _isWaiting = true;
        
      _animator.SetBool("IsMove", false);

      if (!turnToRight)
      {
         talkImage.SetActive(true);
         RandomDescription();
      }
      else
      {
         talkImage.SetActive(false);
         GetComponent<Image>().enabled = false;
      }
        
      yield return new WaitForSeconds(npcData.StopDuration);
        
      GetComponent<Image>().enabled = true;
      _movingLeft = turnToRight;
      Flip(!turnToRight);
      _isWaiting = false;
      talkImage.SetActive(false);
   }
   
   private IEnumerator StopTemporarily()
   {
      _isPaused = true;
      _isWaiting = true;
      
      bool isNearPoint1 = Vector3.Distance(transform.localPosition, stopPoint1.localPosition) < stopRange;
      bool isNearPoint2 = Vector3.Distance(transform.localPosition, stopPoint2.localPosition) < stopRange;
      
      Vector3 offset = _movingLeft ? Vector3.left : Vector3.right;
      transform.localPosition += offset * 0.5f;

      _animator.SetBool("IsMove", false);

      if (_movingLeft)
      {
         if (isNearPoint1)
         {
            ShowDialogue(1);
         }
         else if (isNearPoint2)
         {
            ShowDialogue(2);
         }
      }
      else
      {
         if (isNearPoint1)
         {
            stopPoint1.gameObject.SetActive(false);
         }
         else if (isNearPoint2)
         {
            stopPoint2.gameObject.SetActive(false);
         }
      }
      
      // if (Vector3.Distance(transform.localPosition, stopPoint1.localPosition) < stopRange)
      // {
      //    ShowDialogue(1);
      // }
      // else if (Vector3.Distance(transform.localPosition, stopPoint2.localPosition) < stopRange)
      // {
      //    ShowDialogue(2);
      // }

      // 오브젝트 숨기기
      stopPoint1.gameObject.SetActive(false);

      yield return new WaitForSeconds(npcData.StopDuration); // 멈추는 시간

      _animator.SetBool("IsMove", true);
      
      talkImage.SetActive(false);
      
      // 다시 보이게
      stopPoint1.gameObject.SetActive(true);
      _isPaused = false;
      _isWaiting = false;
   }

   protected override void RandomDescription()
   {
      if (talkImage != null)
      {
         talkImage.gameObject.SetActive(true);
         talkText.text = npcData.npcDescription[0];
      }
   }
   
   private void ShowDialogue(int a)
   {
      if (talkImage != null) talkImage.SetActive(true);
      if (talkText != null) talkText.text = npcData.npcDescription[a];
   }
}
