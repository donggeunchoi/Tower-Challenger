using UnityEngine;

public class MapMover : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;

    void Update()
    {
        float speed = DinoMiniGame.instance.CurrentSpeed;
        transform.Translate(Vector3.left * speed * Time.deltaTime);

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
