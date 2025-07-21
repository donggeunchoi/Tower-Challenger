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
    public Resources resource;

    [Header("캐릭터")]
    public List<CharacterData> allCharacterData { get; private set; } = new List<CharacterData>();
    public List<CharacterData> charactors = new List<CharacterData>();
    public CharacterData equimentCharacter;


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
        TryGetComponent(out resource);

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
        if (resource != null)
            resource.AddGold(addGold);
    }

    public void UseGold(int useGold)
    {
        if (resource != null)
            resource.UseGold(useGold);
    }

    public void AddDiamond(int addDia)
    {
        if (resource != null)
            resource.AddDiamond(addDia);
    }

    public void UseDiamond(int useDia)
    {
        if (resource != null)
            resource.UseDiamond(useDia);
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

        if (resource != null)
            resource.LoadResource();
    }

    public void LoadCharacter()
    {
        charactors.Clear();
        foreach (string name in playerData.characterNames)
        {
            CharacterData data = allCharacterData.Find(character => character.characterName == name);
            if (data != null)
                charactors.Add(data);
        }
        if (playerData.characterNames != null)
        {
            if (playerData.equippedCharacterName != null)
            {
                CharacterData data = allCharacterData.Find(character => character.characterName == playerData.equippedCharacterName);
                if (data != null)
                    equimentCharacter = data;
            }
        }
    }
}
