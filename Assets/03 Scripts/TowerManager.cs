using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance;

    public StageManager stageManager;
    public MiniGameManager miniGameManager;

    [SerializeField] private string[] mapScenes;
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

    public IEnumerator LatePlayerData()
    {
        AsyncOperation ansynLoad = SceneManager.LoadSceneAsync(StageManager.instance.currentSceneName); //어씬크
        while (!ansynLoad.isDone)
        {
            yield return null;
        }

        if (StageManager.instance.stageClearPortal.Count == 0 && StageManager.instance.isGameActive)
        {
            Map map = FindAnyObjectByType<Map>();
            map.AllClearFloor();
        }

        PlayerSetting();  //플레이어 놓기

        if (StageManager.instance.isGameActive)
        {
            if (StageManager.instance.stageClearPortal.Count == 0)
                StageManager.instance.ResetClearPortal();
        }
    }
    public void PlayerSetting()
    {
        GameObject player = null;
        while (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");  //플레이어를 찾아서 
        }

        if (player != null)
        {
            player.layer = StageManager.instance.layerNumber;
            string LayerName;
            switch (StageManager.instance.layerNumber)
            {
                case 20:
                    LayerName = "Layer 1";
                    break;
                case 21:
                    LayerName = "Layer 2";
                    break;
                default:
                    LayerName = "Layer 3";
                    break;
            }
            player.GetComponent<SpriteRenderer>().sortingLayerName = LayerName;
            player.transform.position = StageManager.instance.playerPosition;   //플레이어 위치를 수정
        }
    }
}

