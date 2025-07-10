using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RewardBase : MonoBehaviour
{
    public TextAsset csvFile;
    public List<RewardTableData> rewards = new List<RewardTableData>();
    
    private void Awake()
    {
        LoadCSV();
    }

    private void LoadCSV()
    {
        csvFile = Resources.Load<TextAsset>("TowerRewardTable");
        if (csvFile == null)
        {
            Debug.LogError("csvFile이 없는디요?");
        }
        
        string[] lines = csvFile.text.Split('\n');
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
            
            
            rewards.Add(row);
        }
    }

    string GetValue(string[] arr, int index)
    {
        if (index < arr.Length) return arr[index];
        return "";
    }

    int ParseInt(string s)
    {
        int v = 0;
        int.TryParse(s, out v);
        return v;
    }

    bool ParseBool(string s)
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
