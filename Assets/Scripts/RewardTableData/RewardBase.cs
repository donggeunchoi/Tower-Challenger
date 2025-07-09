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
                count = int.Parse(values[3])
            };

            rewards.Add(reward);
        }
    }
    
    
}
