using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public StageManager stageManager;  //이후 주석처리 (타워매니저 상속)
    public UIManager uiManager;
    public ItemManager itemManager;    //이후 주석처리 (플레이어 매니저 상속)
    
    public GameObject stageManagerPrefab;
    public GameObject uiManagerPrefab;
    public GameObject itemManagerPrefab;

    public List<MiniGameData> miniGameDataList = new List<MiniGameData>();
    public PlayerData playerData;

    [Header("스테미나")]
    public const int MAX_STAMINA = 5;
    public int mainStamina;
    public float staminatimer = 0;
    public const float STAMINA_TIME = 1800f;

    [Header("저장")]
    public float saveTimer;
    public const float SAVETIME = 120;
    public List<CharacterData> allCharacterData = new List<CharacterData>();
    public List<CharacterData> charactors = new List<CharacterData>();
    public CharacterData equimentCharacter;
    public Coroutine saveLoadCor;

    [Header("재화")]
    public int gold;
    public int diamond;

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
    }

    private void Start()
    {
        stageManager = StageManager.instance;
        uiManager = UIManager.Instance;
        itemManager = ItemManager.instance;

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

        if (FindAnyObjectByType(typeof(ItemManager)) == null)
        {
            if (itemManagerPrefab != null)
                Instantiate(itemManagerPrefab);
        }

        LoadMiniGameCSV();

        if (playerData != null)
            playerData = SaveManager.LoadUsers();

        playerData.LoadData();
    }

    private void OnApplicationFocus(bool focus)
    {

    }
    private void OnApplicationPause(bool pause)
    {
        //if (pause)
        //{
        //    playerData.SaveData();
        //}

        //if(!pause)
        //{
        //    playerData.SaveData();
        //}
    }

    private void Update()
    {
        saveTimer += Time.deltaTime;

        if (saveTimer >= SAVETIME)
        {
            saveTimer = 0;
            SaveLoad();
        }

        if (mainStamina >= MAX_STAMINA)
        {
            staminatimer = 0;
            return;
        }
        staminatimer += Time.deltaTime;

        if (staminatimer >= STAMINA_TIME)
        {
            AddStamina();
            staminatimer -= STAMINA_TIME;
        }
    }

    public void SaveLoad()
    {
        if (saveLoadCor != null)
            StopCoroutine(saveLoadCor);
        saveLoadCor = StartCoroutine(SaveLoadCor());
    }

    private IEnumerator SaveLoadCor()
    {
        playerData.SaveData();
        yield return null;
        playerData.LoadData();
    }
        

    public void AddStamina()
    {
        mainStamina = Mathf.Min(mainStamina + 1, MAX_STAMINA);
        SaveLoad();
    }

    public void UseStamina()
    {
        if (mainStamina < 0)
            return;

        mainStamina = Mathf.Max(mainStamina - 1, 0);
        SaveLoad();
    }

    public void AddGold(int addGold)
    {
        gold += addGold;
        SaveLoad();
    }

    public void UseGold(int useGold)
    {
        gold -= useGold;
        SaveLoad();
    }

    public void AddDiamond(int addDia)
    {
        diamond += addDia;
        SaveLoad();
    }

    public void UseDiamond(int useDia)
    {
        diamond -= useDia;
        SaveLoad();
    }

    void LoadMiniGameCSV()
    {
        TextAsset data = Resources.Load<TextAsset>("Mini-GameDifficultySettings");
        if (data == null)
        {
            Debug.LogError("미니게임 CSV 파일이 없습니다.");
            return;
        }
        string[] lines = data.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            string[] values = lines[i].Split(',');
            MiniGameData row = new MiniGameData();
            row.index = GetValue(values, 0);
            row.name = GetValue(values, 1);
            row.Desc = GetValue(values, 2);
            row.Type = GetValue(values, 3);
            row.DifficultyLevel = ParseInt(GetValue(values, 4));
            row.Card = ParseInt(GetValue(values, 5));
            row.shuffleDuration = ParseFloat(GetValue(values, 6));
            row.shuffleCount = ParseInt(GetValue(values, 7));
            row.FailCount = ParseInt(GetValue(values, 8));
            row.max_Num = ParseInt(GetValue(values, 9));
            row.successTime = ParseFloat(GetValue(values, 10));
            row.delayTime = ParseFloat(GetValue(values, 11));
            row.clearGameCount = ParseInt(GetValue(values, 12));
            row.birdSpeed = ParseFloat(GetValue(values, 13));
            row.baseSpeed = ParseFloat(GetValue(values, 14));
            row.gameDuration = ParseFloat(GetValue(values, 15));
            row.spawnInterval = ParseFloat(GetValue(values, 16));
            miniGameDataList.Add(row);
        }
    }
    //1. 직접 값을 할당 해주는 방식(지금이거) [확장성은 떨어짐] [가장 기본형]
    //2. CSV 파싱 라이브러리 사용 유니티 외부 라이브러리 사용 DLL추가 등 약간의 설정 필요
    //3. 리플렉션 활용 코드가 짧아지지만 속도가 느리고 실수 시 런타임 에러발생
    //4. 스크립터블 오브젝트로 변환 unity-excel-importer활용
    //https://github.com/mikito/unity-excel-importer
    
    string GetValue(string[] arr, int idx)
    {
        if (idx < arr.Length) return arr[idx];
        return "";
    }
    int ParseInt(string s)
    {
        int v = 0;
        int.TryParse(s, out v);
        return v;
    }
    float ParseFloat(string s)
    {
        float v = 0;
        float.TryParse(s, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out v);
        return v;
    }
}
