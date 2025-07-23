using UnityEngine;

public class BoxClose : MonoBehaviour
{
    
   public GameObject box;

   public void OnTriggerEnter2D(Collider2D other)
   {
      Debug.Log("여기 들어왔니?");
      if (other.CompareTag("Player"))
      {
         box.SetActive(false);
         
      }
   }
}
