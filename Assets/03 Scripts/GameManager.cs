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

    public void LoadData()
    {
        Save.LoadData();
    }

    public void SaveData()
    {
        Save.SaveData();
    }

    public void AddStamina()
    {
        if (stamina != null)
            stamina.AddStamina();
    }

    public void UseStamina()
    {
        if (stamina != null)
            stamina.UseStamina();
    }

    public void AddGold(int addGold)
    {
        if (account != null)
            account.AddGold(addGold);
    }

    public void UseGold(int useGold)
    {
        if (account != null)
            account.UseGold(useGold);
    }

    public void AddDiamond(int addDia)
    {
        if (account != null)
            account.AddDiamond(addDia);
    }

    public void UseDiamond(int useDia)
    {
        if (account != null)
            account.UseDiamond(useDia);
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
