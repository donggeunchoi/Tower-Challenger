using UnityEngine;

public class Slime : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool isSettled = false;

    [Header("탑")] 
    public Transform towerRoot;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    //떨어진 곳이 땅이냐 슬라임이냐
    void OnCollisionEnter2D(Collision2D other)
    {
        if (isSettled)
            return;

        //슬라임이면 정착단계로 이동하게
        if (other.gameObject.CompareTag("Slime"))
        {
            ContactPoint2D contact = other.contacts[0];

            if (contact.normal.y > 0.5f)
            {
                Settle();
            }
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            //이후에 LPDown으로 연결
            Debug.Log("땅에 착지");
            Destroy(gameObject);
        }
        
    }

    //슬라임 정착단계의 메서드
    private void Settle()
    {
        isSettled = true;
        
        _rb.linearVelocity = Vector2.zero;
        _rb.angularVelocity = 0;
        _rb.bodyType = RigidbodyType2D.Kinematic;

        gameObject.tag = "Slime";

        if (towerRoot != null)
        {
            transform.SetParent(towerRoot);
        }
    }
}
