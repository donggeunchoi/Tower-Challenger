using JetBrains.Annotations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public StageManager stageManager;
    public UIManager uiManager;

    public GameObject stageManagerPrefab;
    public GameObject uiManagerPrefab;

    public const int MAX_STAMINA = 5;
    public int mainStamina;
    public const float STAMINA_TIME = 0.5f;

    //1800초 30분 마다 1참
    //게임이 꺼져도 차게할 방법.... 시작시간과 끝시간을 계산해서 채워준다..?
    //강종하면 어떻게하지.. 특정행동이나 시간마다 자동 저장을 해준다

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
        if (Time.time >= 0.5f)
        {
            AddStamina();
        }
    }

    public void AddStamina()
    {
        mainStamina = Mathf.Max(mainStamina + 1, MAX_STAMINA);
    }

    public void UseStamina()
    {
        if (mainStamina < 0)
            return;

        mainStamina = Mathf.Max(mainStamina - 1, 0);
    }
}
