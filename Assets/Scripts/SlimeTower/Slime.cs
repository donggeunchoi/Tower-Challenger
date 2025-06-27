using UnityEngine;

public class Slime : MonoBehaviour
{
    private Rigidbody2D _rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("땅에 착지");
    }
}
