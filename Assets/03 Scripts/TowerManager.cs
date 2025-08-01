﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance;

    public StageManager stageManager;
    public MiniGameManager miniGameManager;
    private int mapIndex = -1;

    [SerializeField] private MapObstacle[] mapObstacle;
    public MapObstacle currentObstacle;


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
                Destroy(sm.gameObject);
            }
        }

        MiniGameManager[] miniGameManagers = FindObjectsByType<MiniGameManager>(FindObjectsSortMode.None);
        foreach (MiniGameManager mgm in miniGameManagers)
        {
            if (mgm.gameObject != this.gameObject)
            {
                Destroy(mgm.gameObject);
            }
        }
    }
    public string LoadRandomMap()
    {
        if (mapScenes == null || mapScenes.Length == 0)
        {
            return "TopScene-1";
        }

        int randomSceneNum;

        if (mapScenes.Length == 1)
        {
            mapIndex = 0;
            return mapScenes[0];
        }

        do
        {
            randomSceneNum = Random.Range(0, mapScenes.Length);
        }
        while (randomSceneNum == mapIndex);

        mapIndex = randomSceneNum;
        string mapName = mapScenes[randomSceneNum];

        if (mapObstacle[randomSceneNum] != null)
            currentObstacle = mapObstacle[randomSceneNum];

        return mapName;
    }

    public IEnumerator LoadBossStage(bool isBoss)
    {
        AsyncOperation ansynLoad;

        if (isBoss)
        {
            ansynLoad = SceneManager.LoadSceneAsync("BossRoom");
            currentObstacle = null;
        }
        else
            ansynLoad = SceneManager.LoadSceneAsync(LoadRandomMap()); //어씬크

        while (!ansynLoad.isDone)
        {
            yield return null;
        }

        MapInfo.StageTempMemory.destroyedInfo = new MapInfo.DestroyedItemInfo();

        if (currentObstacle != null)
        {
            MapObstacle thisObstacle = Instantiate(currentObstacle);
            thisObstacle.Init();
        }

        Map map = FindAnyObjectByType<Map>();
        map.SetRandomPortal();
        map.Init();
    }

    public IEnumerator AfterLoadData()
    {
        AsyncOperation ansynLoad = SceneManager.LoadSceneAsync(currentSceneName); //어씬크
        while (!ansynLoad.isDone)
        {
            yield return null;
        }
        //if (StageManager.instance.floor == 30 && !StoryManager.storyInstance.storyTalk.isClear)
        //{ StoryManager.storyInstance.storyTalk.StoryInit(); Debug.Log("30층"); }

        if (StageManager.instance.stageClearPortal.Count == 0 && StageManager.instance.isGameActive)
        {
            Map map = FindAnyObjectByType<Map>();
            map.AllClearFloor();
        }

        if (currentObstacle != null)
        {
            MapObstacle thisObstacle = Instantiate(currentObstacle);
            thisObstacle.Init();

            RestoreDestroyedObjects();
        }
        PlayerManager.Instance.PlayerSetting();  //플레이어 놓기

        if (StageManager.instance.isGameActive)
        {
            if (StageManager.instance.stageClearPortal.Count == 0)
                StageManager.instance.ResetClearPortal();
        }
    }

    void RestoreDestroyedObjects()
    {
        foreach (Trap trap in FindObjectsByType<Trap>(FindObjectsSortMode.None))
        {
            if (MapInfo.StageTempMemory.destroyedInfo.destroyedTrapIds.Contains(trap.trapId))
                trap.gameObject.SetActive(false);
        }

        foreach (InteractionBox box in FindObjectsByType<InteractionBox>(FindObjectsSortMode.None))
        {
            if (MapInfo.StageTempMemory.destroyedInfo.destroyedChestIds.Contains(box.boxId))
                box.gameObject.SetActive(false);
        }
    }
}

