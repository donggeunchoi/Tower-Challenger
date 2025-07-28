using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeTower : MonoBehaviour
{
    [Header("슬라임 생성")]
    public GameObject slimePrefab;  //슬라임 프리팹
    public Transform spawnPosition; //스폰 포지션
    public Transform towerRoot;     //타워 포지션
    public int clearGameCount;      //몇개 쌓으면 클리어인지

    [Header("쿨타임")]
    [SerializeField] private int maxStack = 3;  //슬라임을 던질 수 있는 최대 스택갯수
    [SerializeField] public float coolDown = 2f; //스택이 차는 쿨타임
    [SerializeField] public int currentStack; //현재 던질 수 있는 스택
    [SerializeField] public float stackTimer; //쿨타임을 측정해주는 타이머

    [Header("미니게임 클리어 UI")]
    public GameObject miniGameClearUI;
    public Canvas mainCanvas;
    private bool _clear = false;
    
    private bool canClick = false;
    private float clickCooldown = 0.2f;
    private float clickCooldownTimer = 0f;

    private GameObject currentSlime; //현재슬라임
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (StageManager.instance != null && GameManager.Instance)
        {
            int difficulty = StageManager.instance.difficulty;

            MiniGameData data = GameManager.Instance.miniGameDataList.Find(x => x.name == "SlimeTower" && x.DifficultyLevel == difficulty);

            if (data != null)
            {
                clearGameCount = data.clearGameCount;
            }
        }

        currentStack = maxStack;
        stackTimer = 0;
    }

    // Update is called once per frame
    void Update()// 온클릭으로 바꿔야함
    {
        if (!canClick)
        {
            clickCooldownTimer += Time.deltaTime;
            if (clickCooldownTimer >= clickCooldown)
            {
                canClick = true;
                clickCooldownTimer = 0f;
            }
            return;
        }

        if (canClick && Input.GetMouseButtonDown(0)) 
        {
            MoveSlime();
            SpawnSlime();
        }

        SlimeCountClear();

        if (currentStack < maxStack)
        {
            stackTimer += Time.deltaTime;
            if (stackTimer >= coolDown)
            {
                currentStack++;
                stackTimer = 0;
            }
        }
    }
    //슬라임을 생성하는 메서드
    public void SpawnSlime()  
    {
        if (currentStack <= 0)
            return;

        currentSlime = Instantiate(slimePrefab, spawnPosition.position,Quaternion.identity);

        Slime slime = currentSlime.GetComponent<Slime>();
        slime.towerRoot = towerRoot;

        currentStack--;
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
            GameObject miniGameClear = Instantiate(miniGameClearUI,mainCanvas.transform);
            miniGameClear.transform.SetAsLastSibling();

            if (!_clear)
            {
                _clear = true;
                StartCoroutine(WaitinTime());
            }
            
        }
    }
    
    IEnumerator WaitinTime()
    {
        if(_clear == false) yield break;
        
        yield return new WaitForSeconds(1f);
        
        if (StageManager.instance != null)
            StageManager.instance.MiniGameResult(true);
    }
}
