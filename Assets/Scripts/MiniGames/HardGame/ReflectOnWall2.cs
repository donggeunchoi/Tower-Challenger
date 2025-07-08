using UnityEngine;

public class ReflectOnWall2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Wall"))
        {
            Vector3 rot = transform.eulerAngles;
            rot.x += 180f;
            transform.rotation = Quaternion.Euler(rot);
        }
    }
}
