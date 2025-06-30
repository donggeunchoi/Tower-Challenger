using UnityEngine;

public class UIManager : MonoBehaviour
{
    public StageManager stageManager;
    public static UIManager Instance;
    public StageTimer timerUI;
    public StageLP stageLPUI;
    public GameObject gameOverUI;

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
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        stageManager = StageManager.instance;
        timerUI = stageManager.stageTimer;
        stageLPUI = stageManager.stageLP;
    }
}
