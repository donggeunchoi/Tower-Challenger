using UnityEngine;

public class SlimeTower : MonoBehaviour
{
    [Header("슬라임 생성")]
    public GameObject slimePrefab;  //슬라임 프리팹
    public Transform spawnPosition; //스폰 포지션
    public Transform towerRoot;     //타워 포지션
    public int clearGameCount;      //몇개 쌓으면 클리어인지

    private GameObject currentSlime; //현재슬라임
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //SpawnSlime();
    }

    // Update is called once per frame
    void Update()// 온클릭으로 바꿔야함
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveSlime();
            Invoke("SpawnSlime",0.5f);
        }
        
        
        SlimeCountClear();
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

    private void SlimeCountClear()
    {
        int slimeCount = towerRoot.childCount;
        if (slimeCount == clearGameCount)
        {
            StageManager.instance.MiniGameResult(true);
        }
    }
}
