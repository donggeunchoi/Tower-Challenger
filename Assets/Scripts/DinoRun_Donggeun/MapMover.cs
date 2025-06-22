using UnityEngine;

public class MapMover : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        if (transform.position.x < -20f)
        {
            Debug.Log("way");
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("RespawnZone"))
        {
            Destroy(gameObject);
        }
    }
}
