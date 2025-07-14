using System;
using System.Collections.Generic;
using UnityEngine;

public static class CVSLoader
{
    public static List<MiniGameData> miniGameDataList = new List<MiniGameData>();
    public static List<RewardTableData>  rewardTableDataList = new List<RewardTableData>();

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
            miniGameDataList.Add(row);
        }
    }
    //1. 직접 값을 할당 해주는 방식(지금이거) [확장성은 떨어짐] [가장 기본형]
    //2. CSV 파싱 라이브러리 사용 유니티 외부 라이브러리 사용 DLL추가 등 약간의 설정 필요
    //3. 리플렉션 활용 코드가 짧아지지만 속도가 느리고 실수 시 런타임 에러발생
    //4. 스크립터블 오브젝트로 변환 unity-excel-importer활용
    //https://github.com/mikito/unity-excel-importer

    public static void LoadPotalCVS()
    {
        TextAsset data = Resources.Load<TextAsset>("//여기에 경로를 입력!");
        if (data == null)
        {
            Debug.LogError("미니게임 CSV 파일이 없습니다.");
            return;
        }
        string[] lines = data.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            //여기에 항목 추가!
        }
    }

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
            row.index = ParseInt(GetValue(values,0));
            row.name = GetValue(values,1);
            row.type = GetValue(values,2);
            row.floor = ParseInt(GetValue(values,4));
            row.goldReward =  ParseInt(GetValue(values,5));
            row.diaReward =  ParseInt(GetValue(values,6));
            row.speedMoveReward = ParseBool(GetValue(values,7));
            row.sandGlassReward =  ParseBool(GetValue(values,8));
            row.topTicketReward =  ParseBool(GetValue(values,9));
            
            
            rewardTableDataList.Add(row);
        }
        
    }

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
        if(string.IsNullOrWhiteSpace(s)) return false;
        s = s.Trim().ToLower();
        if(s == "true" || s == "1")
            return true;
        if (s == "false" || s == "0")
            return false;
        return false;
    }
}
