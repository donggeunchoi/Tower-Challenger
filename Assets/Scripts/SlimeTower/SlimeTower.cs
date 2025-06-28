using UnityEngine;

public class SlimeTower : MonoBehaviour
{
    [Header("슬라임 생성")]
    public GameObject slimePrefab;
    public Transform spawnPosition;
    public Transform towerRoot;

    private GameObject currentSlime;
    private float tiltLimit = 20f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnSlime();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveSlime();
            Invoke("SpawnSlime",0.5f);
        }
        
        // CheckTower();
    }
    //슬라임을 생성하는 메서드
    public void SpawnSlime()
    {
        currentSlime = Instantiate(slimePrefab, spawnPosition.position,Quaternion.identity);

        Slime slime = currentSlime.GetComponent<Slime>();
        slime.towerRoot = towerRoot;
    }
    //떨어지는 메서드(움직이는 물체를 보여줘야하니까)
    public void MoveSlime()
    {
        if (currentSlime != null)
        {
            Rigidbody2D rb = currentSlime.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            currentSlime = null;
        }
    }
    
    //타워를 체크해서 기울기를 확인하는 메서드
    // private void CheckTower()
    // {
    //     float zAngle = towerRoot.eulerAngles.z;
    //     if (zAngle > 180f)
    //     {
    //         zAngle -= 360f;
    //     }
    //
    //     if (Mathf.Abs(zAngle) < tiltLimit)
    //     {
    //         Debug.Log($"이게 뭔지 다시 한번체크해봐야겠다.{zAngle}");
    //         
    //     }
    // }
    
}
