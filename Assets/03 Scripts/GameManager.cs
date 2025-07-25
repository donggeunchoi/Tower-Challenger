using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public StageManager towerManager;
    public UIManager uiManager;
    public ItemManager itemManager;

    public GameObject towerManagerPrefab;
    public GameObject uiManagerPrefab;
    public GameObject itemManagerPrefab;
    public GameObject playerManagerPrefab;
    public GameObject soundManagerPrefab;
    public GameObject storyManagerPrefab;

    public List<MiniGameData> miniGameDataList = new List<MiniGameData>();
    public PlayerData playerData { get; private set; }

    [Header("데이터 관리")]
    public Stamina stamina;
    public Account account;
    public Character character;

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

        towerManager = StageManager.instance;
        uiManager = UIManager.Instance;
        itemManager = ItemManager.instance;

        if (FindAnyObjectByType(typeof(UIManager)) == null)
        {
            if (uiManagerPrefab != null)
                Instantiate(uiManagerPrefab);
        }

        if (FindAnyObjectByType(typeof(TowerManager)) == null)
        {
            if (towerManagerPrefab != null)
                Instantiate(towerManagerPrefab);
        }

        if (FindAnyObjectByType(typeof(ItemManager)) == null)
        {
            if (itemManagerPrefab != null)
                Instantiate(itemManagerPrefab);
        }

        if (FindAnyObjectByType(typeof(PlayerManager)) == null)
        {
            if (playerManagerPrefab != null)
                Instantiate(playerManagerPrefab);
        }

        if (FindAnyObjectByType(typeof(SoundManager)) == null)
        {
            if (soundManagerPrefab != null)
                Instantiate(soundManagerPrefab);
        }
        if (FindAnyObjectByType(typeof(StoryManager)) == null)
        {
            if (storyManagerPrefab != null)
                Instantiate(storyManagerPrefab);
        }
    }

    private void Start()
    {
        TryGetComponent(out stamina);
        TryGetComponent(out account);
        TryGetComponent(out character);

        playerData = new PlayerData();
        Save.SetPlayerData(playerData);

        LoadMiniGameCSV();
        Save.LoadData();
    }

    private void Update()
    {
        Save.SaveUpdate();
    }

    private static void OnApplicationFocus(bool focus)
    {
        if (Save.isLoad && !focus)
        {
            Save.SaveData();
        }
    }
    private static void OnApplicationPause(bool pause)
    {
        if (Save.isLoad && pause)
            Save.SaveData();
    }

    void LoadMiniGameCSV()
    {
        CVSLoader.LoadMiniGameCSV();
        miniGameDataList = CVSLoader.miniGameDataList;
    }
    
    public void LoadResourceData()
    {
        if (stamina != null)
            stamina.LoadStamina();

        if (account != null)
            account.LoadResource();

        if (character != null)
            character.LoadCharacters();
    }
}
