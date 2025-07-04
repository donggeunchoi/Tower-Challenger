using JetBrains.Annotations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public StageManager stageManager;
    public UIManager uiManager;

    public GameObject stageManagerPrefab;
    public GameObject uiManagerPrefab;

    [Header("스테미나")]
    public const int MAX_STAMINA = 5;
    public int mainStamina;
    public float staminatimer = 0;
    public const float STAMINA_TIME = 1800f;

    [Header("재화")]
    public int gold;
    public int diamond;

    //1800초 30분 마다 1참
    //게임이 꺼져도 차게할 방법.... 시작시간과 끝시간을 계산해서 채워준다..?
    //강종하면 어떻게하지.. 특정행동이나 시간마다 자동 저장을 해준다
    //추후 저장기능 추가 후 종료시간 ~ 다시킨 시간을 불러와서 staminatimer에 더해준다

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        stageManager = StageManager.instance;
        uiManager = UIManager.Instance;

        if (FindAnyObjectByType(typeof(UIManager)) == null)
        {
            if (uiManagerPrefab != null)
                Instantiate(uiManagerPrefab);
        }

        if (FindAnyObjectByType(typeof(StageManager)) == null)
        {
            if (stageManagerPrefab != null)
                Instantiate(stageManagerPrefab);
        }
    }

    private void Update()
    {
        if (mainStamina >= MAX_STAMINA)
            return;

        staminatimer += Time.deltaTime;

        if (staminatimer >= STAMINA_TIME)
        {
            AddStamina();
            staminatimer -= STAMINA_TIME;
        }
    }

    public void AddStamina()
    {
        mainStamina = Mathf.Min(mainStamina + 1, MAX_STAMINA);
    }

    public void UseStamina()
    {
        if (mainStamina < 0)
            return;

        mainStamina = Mathf.Max(mainStamina - 1, 0);
    }
}
