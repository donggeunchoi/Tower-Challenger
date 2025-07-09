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
        csvFile = Resources.Load<TextAsset>("TowerRewardData");
        if (csvFile == null)
        {
            Debug.LogError("csvFile이 없는디요?");
        }
        
        string[] lines = csvFile.text.Split('\n');
        for (int i = 1; i < lines.Length; i++) // 헤더 제외
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = lines[i].Split(',');

            RewardTableData reward = new RewardTableData
            {
                id = int.Parse(values[0]),
                name = values[1],
                type = values[2],
                goldCount = int.Parse(values[6]),
                diaCount = int.Parse(values[7]),
                speedMoveGive  = Parsebool(values[8]),
                sandGlassGive  = Parsebool(values[9]),
                topTicketGive  = Parsebool(values[10])
            };
            
            
            
            rewards.Add(reward);
        }
    }
    
    private bool Parsebool(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;
        
        value = value.Trim().ToLower();
        if (value == "true" || value == "1")
            return true;
        if (value == "false" || value == "0")
            return false;
        // 필요하다면 예외처리 또는 기본값 반환
        return false;
    }
}
