using UnityEngine;

public class UIManager : MonoBehaviour
{
    public StageManager stageManager;
    public static UIManager Instance;
    public StageTimer timerUI;
    public StageLP stageLPUI;
    public GameObject gameOverUI;
    public GameObject allClearUI;

    public Transform notDestroyCanvus;

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
        notDestroyCanvus.gameObject.SetActive(false);
    }

    public void InstantiateUI(GameObject instanceUI)
    {
        Instantiate(instanceUI, notDestroyCanvus);
    }
}
