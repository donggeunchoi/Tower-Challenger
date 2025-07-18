﻿using System;
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

    [Header("스테미나")]
    public const int MAX_STAMINA = 5;
    public int mainStamina { get; private set; }
    public float staminatimer { get; private set; }
    public const float STAMINA_TIME = 1800f;

    [Header("저장")]
    private bool isLoad = false;
    public float saveTimer;
    public const float SAVETIME = 120;
    public bool isSaving = false;
    public float saveCoolDown;
    public const float SAVECOOLDOWN = 0.5f;
    public List<CharacterData> allCharacterData { get; private set; } = new List<CharacterData>();
    public List<CharacterData> charactors = new List<CharacterData>();
    public CharacterData equimentCharacter;

    [Header("재화")]
    public int gold { get; private set; }
    public int diamond { get; private set; }

    public int Gold;
    public int Dia;

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
        LoadMiniGameCSV();

        playerData = SaveManager.LoadUsers();
        playerData.LoadData();
        isLoad = true;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (isLoad && !focus)
        {
            SaveData();
        }
    }
    private void OnApplicationPause(bool pause)
    {
        if (isLoad && pause)
            SaveData();
    }

    private void Update()
    {
        Gold = gold;
        Dia = diamond;

        AutoSave();
        UpdateStaminaTimer();

        if (isSaving)
        {
            saveCoolDown += Time.deltaTime;
            if (saveCoolDown >= SAVECOOLDOWN)
            {
                isSaving = false;
            }
        }
    }

    private void AutoSave()
    {
        saveTimer += Time.deltaTime;

        if (saveTimer >= SAVETIME)
        {
            saveTimer = 0;
            SaveData();
        }
    }

    private void UpdateStaminaTimer()
    {
        if (mainStamina >= MAX_STAMINA)
        {
            staminatimer = 0;
            return;
        }
        staminatimer += Time.deltaTime;

        if (staminatimer >= STAMINA_TIME)
        {
            Debug.Log("시간이 돌았습니다");
            AddStamina();
            staminatimer -= STAMINA_TIME;
        }
    }

    public void LoadData()
    {
        playerData.LoadData();
    }

    public void SaveData()
    {
        if (isSaving)
            return;

        playerData.SaveData();
        isSaving = true;
        saveCoolDown = 0f;
    }

    public void AddStamina()
    {
        mainStamina = Mathf.Min(mainStamina + 1, MAX_STAMINA);
        SaveData();
    }

    public void UseStamina()
    {
        if (mainStamina < 0)
            return;

        mainStamina = Mathf.Max(mainStamina - 1, 0);
        SaveData();
    }

    public void AddGold(int addGold)
    {
        gold += addGold;
        Debug.Log("획득한 골드 :" + addGold);
        SaveData();
    }

    public void UseGold(int useGold)
    {
        gold -= useGold;
        SaveData();
    }

    public void AddDiamond(int addDia)
    {
        diamond += addDia;
        SaveData();
    }

    public void UseDiamond(int useDia)
    {
        diamond -= useDia;
        SaveData();
    }

    void LoadMiniGameCSV()
    {
        CVSLoader.LoadMiniGameCSV();
        miniGameDataList = CVSLoader.miniGameDataList;
    }

    public void LoadResource()
    {
        mainStamina = playerData.stamina;
        gold = playerData.gold;
        diamond = playerData.diamond;
    }

    public void LoadStaminaTimer()
    {
        if (!string.IsNullOrEmpty(playerData.lastTimeString))
        {
            playerData.lastTime = DateTime.Parse(playerData.lastTimeString, null,
                System.Globalization.DateTimeStyles.RoundtripKind);
        }
        else
        {
            playerData.lastTime = DateTime.Now;
        }

        float secondsPassed = (float)(DateTime.Now - playerData.lastTime).TotalSeconds;
        staminatimer = playerData.staminaTimer + secondsPassed;

        if (mainStamina <= 0)  //테스트용 코드
            mainStamina = 5;
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
