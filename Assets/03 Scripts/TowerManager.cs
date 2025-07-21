using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance;

    public StageManager stageManager;
    public MiniGameManager miniGameManager;

    [SerializeField] private string[] mapScenes;
    public string currentSceneName;   //현재 씬 이름
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
        DestroyAnyManager();
    }

    private void Start()
    {
        stageManager = GetComponent<StageManager>();
        miniGameManager = GetComponent<MiniGameManager>();
    }

    private void DestroyAnyManager()
    {
        StageManager[] stageManagers = FindObjectsByType<StageManager>(FindObjectsSortMode.None);
        foreach (StageManager sm in stageManagers)
        {
            if (sm.gameObject != this.gameObject)
            {
                Debug.LogWarning("스테이지 매니저 중복 파괴");
                Destroy(sm.gameObject);
            }
        }

        MiniGameManager[] miniGameManagers = FindObjectsByType<MiniGameManager>(FindObjectsSortMode.None);
        foreach (MiniGameManager mgm in miniGameManagers)
        {
            if (mgm.gameObject != this.gameObject)
            {
                Debug.LogWarning("미니게임 매니저 중복 파괴");
                Destroy(mgm.gameObject);
            }
        }
    }
    public string LoadRandomMap()
    {
        int randomSceneNum = Random.Range(0, mapScenes.Length);
        if (mapScenes == null)
        {
            return mapScenes[0];
        }
        string mapName = mapScenes[randomSceneNum];
        return mapName;
    }

    public IEnumerator LoadBossStage(bool isBoss)
    {
        AsyncOperation ansynLoad;

        if (isBoss)
            ansynLoad = SceneManager.LoadSceneAsync("BossRoom");
        else
            ansynLoad = SceneManager.LoadSceneAsync(LoadRandomMap()); //어씬크

        while (!ansynLoad.isDone)
        {
            yield return null;
        }

        Map map = FindAnyObjectByType<Map>();
        map.SetRandomPortal();
    }

    public IEnumerator AfterLoadData()
    {
        AsyncOperation ansynLoad = SceneManager.LoadSceneAsync(currentSceneName); //어씬크
        while (!ansynLoad.isDone)
        {
            yield return null;
        }

        if (StageManager.instance.stageClearPortal.Count == 0 && StageManager.instance.isGameActive)
        {
            Map map = FindAnyObjectByType<Map>();
            map.AllClearFloor();
        }

        PlayerManager.Instance.PlayerSetting();  //플레이어 놓기

        if (StageManager.instance.isGameActive)
        {
            if (StageManager.instance.stageClearPortal.Count == 0)
                StageManager.instance.ResetClearPortal();
        }
    }
}

