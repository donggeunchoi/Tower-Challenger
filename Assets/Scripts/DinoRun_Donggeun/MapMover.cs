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
        Debug.Log($"충돌한 태그: {collision.tag}, 이름: {collision.name}");
        
        if(collision.CompareTag("RespawnZone"))
        {
            Debug.Log("메롱시티");
            Destroy(gameObject);
        }
    }
}
