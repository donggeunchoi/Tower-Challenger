using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPC4Controller : NPCBase
{
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
         // 여기서는 이미지 비활성화 안함
      }

      yield return new WaitForSeconds(npcData.StopDuration);

      _movingLeft = turnToRight;
      Flip(!turnToRight);
      _isWaiting = false;
      talkImage.SetActive(false);
   }
}

