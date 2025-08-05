using System.Collections.Generic;
using UnityEngine;
using static BoxDataTable;
using static StageTable;

public static class CVSLoader
{
    public static List<MiniGameData> miniGameDataList = new List<MiniGameData>();
    public static List<RewardTableData> rewardTableDataList = new List<RewardTableData>();
    public static List<PotalStageData> potalStageDataList = new List<PotalStageData>();
    public static List<DebuffTable> debuffTableDataList = new List<DebuffTable>();
    public static List<DebuffStunTableRow> debuffSturnTableDataList = new List<DebuffStunTableRow>();

    public static List<ArrowDataTableRow> arrowDataList = new List<ArrowDataTableRow>();
    public static List<BoxDataTableRow> boxDataList = new List<BoxDataTableRow>();
    public static List<GoldBoxDataTableRow> goldBoxDataList = new List<GoldBoxDataTableRow>();
    public static List<NekoManDataTableRow> nekoManDataList = new List<NekoManDataTableRow>();
    public static List<PlusLPDataTableRow> plusLPDataList = new List<PlusLPDataTableRow>();
    #region MiniGame
    public static void LoadMiniGameCSV()
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
            row.clearTime = ParseFloat(GetValue(values, 17));
            row.gravity = ParseFloat(GetValue(values, 18));
            row.spawnDelay = ParseFloat(GetValue(values, 19));
            miniGameDataList.Add(row);
        }
    }
    //1. 직접 값을 할당 해주는 방식(지금이거) [확장성은 떨어짐] [가장 기본형]
    //2. CSV 파싱 라이브러리 사용 유니티 외부 라이브러리 사용 DLL추가 등 약간의 설정 필요
    //3. 리플렉션 활용 코드가 짧아지지만 속도가 느리고 실수 시 런타임 에러발생
    //4. 스크립터블 오브젝트로 변환 unity-excel-importer활용
    //https://github.com/mikito/unity-excel-importer
    #endregion
    #region Reward
    public static void LoadRewardCVS()
    {
        TextAsset data = Resources.Load<TextAsset>("TowerRewardTable");

        if (data == null)
        {
            Debug.LogError("미니게임 CSV 파일이 없습니다.");
            return;
        }

        string[] lines = data.text.Split('\n');
        for (int i = 1; i < lines.Length; i++) // 헤더 제외
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = lines[i].Split(',');

            RewardTableData row = new RewardTableData();
            row.index = ParseInt(GetValue(values, 0));
            row.name = GetValue(values, 1);
            row.type = GetValue(values, 2);
            row.floor = ParseInt(GetValue(values, 4));
            row.goldReward = ParseInt(GetValue(values, 5));
            row.diaReward = ParseInt(GetValue(values, 6));
            row.speedMoveReward = ParseBool(GetValue(values, 7));
            row.sandGlassReward = ParseBool(GetValue(values, 8));
            row.topTicketReward = ParseBool(GetValue(values, 9));

            rewardTableDataList.Add(row);
        }
    }
    #endregion
    #region Portal
    public static void LoadPotalCVS()
    {
        TextAsset data = Resources.Load<TextAsset>("floorTable(Sheet1)"); // 경로/파일명 파일명만(확장자없음), 실제 CSV명에 맞추기!
        if (data == null)
        {
            Debug.LogError("포탈 스테이지 CSV 파일이 없습니다.");
            return;
        }
        string[] lines = data.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] values = line.Split(',');

            if (values.Length < 7) continue;

            PotalStageData row = new PotalStageData();

            row.index = GetValue(values, 0);
            row.floor = ParseInt(GetValue(values, 1));
            row.desc = GetValue(values, 2);
            row.type = GetValue(values, 3);

            // potal: "1", "1 ~ 2", "2 ~ 3" 등 처리
            string potalRaw = GetValue(values, 4).Replace(" ", "");
            if (potalRaw.Contains("~"))
            {
                string[] arr = potalRaw.Split('~');
                row.potalMin = ParseInt(arr[0]);
                row.potalMax = ParseInt(arr[1]);
            }
            else
            {
                row.potalMin = ParseInt(potalRaw);
                row.potalMax = row.potalMin;
            }

            row.boss = ParseBool(GetValue(values, 5));
            row.LpRealse = ParseBool(GetValue(values, 6));

            potalStageDataList.Add(row);
        }
    }
    #endregion
    #region Debuff
    public static void LoadDebuffCSV()
    {
        StageTable.debuffSlowTableList.Clear();
        TextAsset data = Resources.Load<TextAsset>("DebuffTable");
        if (data == null)
        {
            Debug.LogError("디버프 테이블 CSV 파일이 없습니다.");
            return;
        }

        string[] lines = data.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = lines[i].Split(',');

            StageTable.DebuffTable row = new StageTable.DebuffTable();
            row.index = GetValue(values, 0);
            row.name = GetValue(values, 1);
            row.desc = GetValue(values, 2);
            row.type = GetValue(values, 3);
            row.floor = ParseInt(GetValue(values, 4));
            row.diflev = ParseInt(GetValue(values, 5));
            row.downSP = GetValue(values, 6);
            row.eftTime = ParseInt(GetValue(values, 7));

            StageTable.debuffSlowTableList.Add(row);
        }
    }

    public static void LoadDebuffStunCSV()
    {
        StageTable.debuffStunTableList.Clear();
        TextAsset data = Resources.Load<TextAsset>("debuffStunTable");
        if (data == null)
        {
            Debug.LogError("debuffStunTable.csv 파일이 없습니다.");
            return;
        }
        string[] lines = data.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            string[] vals = lines[i].Split(',');

            if (vals.Length < 8) continue;

            StageTable.DebuffStunTableRow row = new StageTable.DebuffStunTableRow();
            row.index = vals[0];
            row.name = vals[1];
            row.dec = vals[2];
            row.type = vals[3];
            row.floor = ParseInt(vals[4]);
            row.diflev = ParseInt(vals[5]);
            row.eftTime = ParseInt(vals[6]);
            row.imm = ParseBool(vals[7]);
            StageTable.debuffStunTableList.Add(row);
        }
    }

    public static void LoadTrapPerTableCSV()
    {
        trapPerTableList.Clear();
        TextAsset data = Resources.Load<TextAsset>("TrapPerTable"); // 확장자X
        if (data == null)
        {
            Debug.LogError("TrapPerTable.csv 파일이 없습니다.");
            return;
        }
        string[] lines = data.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            string[] vals = lines[i].Split(',');

            TrapPerTableRow row = new TrapPerTableRow();

            // NOTE: TrapPerTable.csv에서 index~stuPer까지 총 8열
            row.index = GetValue(vals, 0);
            row.name = GetValue(vals, 1);
            row.dec = GetValue(vals, 2);
            row.type = GetValue(vals, 3);
            row.floor = ParseInt(GetValue(vals, 4));
            row.hitPer = ParseInt(GetValue(vals, 5));
            row.slowPer = ParseInt(GetValue(vals, 6));
            row.stuPer = ParseInt(GetValue(vals, 7));

            if (
                string.IsNullOrWhiteSpace(row.index) &&
                string.IsNullOrWhiteSpace(row.name) &&
                string.IsNullOrWhiteSpace(row.dec) &&
                string.IsNullOrWhiteSpace(row.type) &&
                row.floor == 0 &&
                row.hitPer == 0 &&
                row.slowPer == 0 &&
                row.stuPer == 0
            )
                continue;

            trapPerTableList.Add(row);
        }
    }
    #endregion
    #region BoxData
    // ArrowDataTable.csv
    public static void LoadArrowDataCSV()
    {
        BoxDataTable.arrowDataList.Clear();

        TextAsset data = Resources.Load<TextAsset>("ArrowDataTable");
        if (data == null)
        {
            Debug.LogError("ArrowDataTable.csv 파일이 없습니다.");
            return;
        }
        string[] lines = data.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            string[] vals = lines[i].Split(',');

            ArrowDataTableRow row = new ArrowDataTableRow();
            row.index = GetValue(vals, 0);
            row.name = GetValue(vals, 1);
            row.dec = GetValue(vals, 2);
            row.floor = ParseInt(GetValue(vals, 3));
            row.waring = ParseBool(GetValue(vals, 4));
            row.arrowPer = ParseInt(GetValue(vals, 5));
            row.arrowSP = ParseFloat(GetValue(vals, 6));
            row.timing = ParseFloat(GetValue(vals, 7));
            row.damege = ParseInt(GetValue(vals, 8));
            BoxDataTable.arrowDataList.Add(row);
        }
    }
    // BoxDataTable.csv
    public static void LoadBoxDataCSV()
    {
        BoxDataTable.boxDataList.Clear();
        TextAsset data = Resources.Load<TextAsset>("BoxDataTable");
        if (data == null)
        {
            Debug.LogError("BoxDataTable.csv 파일이 없습니다.");
            return;
        }
        string[] lines = data.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            string[] vals = lines[i].Split(',');

            BoxDataTableRow row = new BoxDataTableRow();
            row.index = GetValue(vals, 0);
            row.name = GetValue(vals, 1);
            row.dec = GetValue(vals, 2);
            row.floor = ParseInt(GetValue(vals, 3));
            row.box = ParseBool(GetValue(vals, 4));
            row.boxCount = ParseInt(GetValue(vals, 5));
            row.oneBoxPer = ParseInt(GetValue(vals, 6));
            row.twoBoxPer = ParseInt(GetValue(vals, 7));
            row.thrBoxPer = ParseInt(GetValue(vals, 8));
            BoxDataTable.boxDataList.Add(row);
        }
    }
    // GoldBoxDataTable.csv
    public static void LoadGoldBoxDataCSV()
    {
        BoxDataTable.goldBoxDataList.Clear();
        TextAsset data = Resources.Load<TextAsset>("GoldBoxDataTable");
        if (data == null)
        {
            Debug.LogError("GoldBoxDataTable.csv 파일이 없습니다.");
            return;
        }
        string[] lines = data.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            string[] vals = lines[i].Split(',');

            GoldBoxDataTableRow row = new GoldBoxDataTableRow();

            row.index = GetValue(vals, 0);
            row.name = GetValue(vals, 1);
            row.dec = GetValue(vals, 2);
            row.floor = ParseInt(GetValue(vals, 3));
            row.goldPer = ParseInt(GetValue(vals, 4));
            row.goldRe = ParseInt(GetValue(vals, 5));

            BoxDataTable.goldBoxDataList.Add(row);
        }
    }
    // NekoManDataTable.csv
    public static void LoadNekoManDataCSV()
    {
        BoxDataTable.nekoManDataList.Clear();
        TextAsset data = Resources.Load<TextAsset>("NekoManDataTable");
        if (data == null)
        {
            Debug.LogError("NekoManDataTable.csv 파일이 없습니다.");
            return;
        }
        string[] lines = data.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            string[] vals = lines[i].Split(',');

            NekoManDataTableRow row = new NekoManDataTableRow();
            row.index = GetValue(vals, 0);
            row.name = GetValue(vals, 1);
            row.dec = GetValue(vals, 2);
            row.floor = ParseInt(GetValue(vals, 3));
            row.waring = ParseBool(GetValue(vals, 4));
            row.nekoPer = ParseInt(GetValue(vals, 5));
            row.neokSP = ParseFloat(GetValue(vals, 6));
            row.timing = ParseFloat(GetValue(vals, 7));
            row.damege = ParseInt(GetValue(vals, 8));
            BoxDataTable.nekoManDataList.Add(row);
        }
    }
    // PlusLPDataTable.csv
    public static void LoadPlusLPDataCSV()
    {
        BoxDataTable.plusLPDataList.Clear();
        TextAsset data = Resources.Load<TextAsset>("PlusLPDataTable");
        if (data == null)
        {
            Debug.LogError("PlusLPDataTable.csv 파일이 없습니다.");
            return;
        }
        string[] lines = data.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            string[] vals = lines[i].Split(',');

            PlusLPDataTableRow row = new PlusLPDataTableRow();
            row.index = GetValue(vals, 0);
            row.name = GetValue(vals, 1);
            row.dec = GetValue(vals, 2);
            row.floor = ParseInt(GetValue(vals, 3));
            row.plusL = ParseInt(GetValue(vals, 4));
            row.lpPer = ParseInt(GetValue(vals, 5));
            row.oneLPPer = ParseInt(GetValue(vals, 6));
            row.twoLPPer = ParseInt(GetValue(vals, 7));
            BoxDataTable.plusLPDataList.Add(row);
        }
    }
    #endregion


    private static string GetValue(string[] arr, int idx)
    {
        if (idx < arr.Length) return arr[idx];
        return "";
    }
    private static int ParseInt(string s)
    {
        int v = 0;
        int.TryParse(s, out v);
        return v;
    }
    private static float ParseFloat(string s)
    {
        float v = 0;
        float.TryParse(s, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out v);
        return v;
    }

    private static bool ParseBool(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return false;
        s = s.Trim().ToLower();
        if (s == "true" || s == "1")
            return true;
        if (s == "false" || s == "0")
            return false;
        return false;
    }
}