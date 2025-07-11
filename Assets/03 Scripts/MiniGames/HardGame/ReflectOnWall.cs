using UnityEngine;

public class ReflectOnWall : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Wall"))
        {
            Vector3 rot = transform.eulerAngles;
            rot.y += 180f;
            transform.rotation = Quaternion.Euler(rot);
        }
    }
}
