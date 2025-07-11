using UnityEngine;

public class Slime : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool isSettled = false;
   

    [Header("탑")] 
    public Transform towerRoot;
    public float percentClearBar;
    private Animator _animation;



    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animation = GetComponentInChildren<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_animation != null)
        {
            _animation.SetBool("IsDrop",false);
        }
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
                CheckOverlapWith(other.gameObject);
                Settle();
            }
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            StageManager.instance.MiniGameResult(false);
            //이후에 LPDown으로 연결
            Debug.Log("땅에 착지");
            Destroy(gameObject);
        }
        
    }

    //슬라임 정착단계의 메서드
    private void Settle()
    {
        _animation.SetBool("IsDrop",true);
        isSettled = true;
        
        _rb.linearVelocity = Vector2.zero;
        _rb.angularVelocity = 0;
        _rb.bodyType = RigidbodyType2D.Kinematic;

        gameObject.tag = "Slime";

        if (towerRoot != null)
        {
            transform.SetParent(towerRoot);
        }
        
        _animation.SetTrigger("IsDropFinal");
    }

    private void CheckOverlapWith(GameObject other)
    {
        //슬라임 나의 충돌 범위 불러오기
        Bounds myBounds = GetComponent<Collider2D>().bounds;
        //정착 되어있는 슬리임 범위 불러오기
        Bounds otherBounds = other.GetComponent<Collider2D>().bounds;

        //내 슬라임 왼쪽 x좌표와 오른쪽 x좌표를 저장
        float myLeft = myBounds.min.x;
        float myRight = myBounds.max.x;

        //정작된 왼쪽 오른쪽 좌표 저장
        float otherLeft = otherBounds.min.x;
        float otherRight = otherBounds.max.x;

        //겹친 부분의 시작점과 끝점을 찾아서
        float overlapLeft = Mathf.Max(myLeft, otherLeft);
        float overlapRight = Mathf.Min(myRight, otherRight);
        
        //겹친 가로의 길이를 계산해서 음수가 나오면 0으로 처리
        float overlapWidth = Mathf.Max(0, overlapRight - overlapLeft);
        //내 슬라임의 전체 가로 길이를 구해서
        float myWidth = myBounds.size.x;

        //절반보다 적으면
        if (percentClearBar > 1f)
            percentClearBar = 1f;

        if (overlapWidth < myWidth * percentClearBar)
        {
            StageManager.instance.MiniGameResult(false);
            //LPDown을 불러오기
            Debug.Log("잘못된 착지");
            Destroy(gameObject);
        }


    }
}
