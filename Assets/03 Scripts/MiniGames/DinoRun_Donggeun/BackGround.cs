using UnityEngine;

public class BackGround : MonoBehaviour
{
   private float scrollSpeed => DinoMiniGame.instance.CurrentSpeed;

   void Update()
   {
      transform.Translate(Vector3.left * scrollSpeed *Time.deltaTime);
   }
}
