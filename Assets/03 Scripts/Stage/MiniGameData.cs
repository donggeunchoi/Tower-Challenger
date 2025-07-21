using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Globalization;

[System.Serializable]
public class MiniGameData
{
    public string index;
    public string name;
    public string Type;
    public string Desc;
    public int DifficultyLevel;
    public int Card;
    public float shuffleDuration;
    public int shuffleCount;
    public int FailCount;
    public int max_Num;
    public float successTime;
    public float delayTime;
    public int clearGameCount;
    public float birdSpeed;
    public float baseSpeed;
    public float gameDuration;
    public float spawnInterval;
    public float clearTime;
    public float gravity;
    public float spawnDelay;
}

public class CSVLoader : MonoBehaviour
{
    public List<MiniGameData> dataList = new List<MiniGameData>();

    void Start()
    {
        LoadCSV();
    }

    void LoadCSV()
    {
        TextAsset data = Resources.Load<TextAsset>("Images/Resources/Mini-GameDifficultySettings");
        if (data == null)
        {
            Debug.LogError("CSV 파일을 찾을 수 없습니다.");
            return;
        }

        string[] lines = Regex.Split(data.text, "\r\n|\n|\r");
        if (lines.Length <= 1) return;

        // 첫 줄은 헤더
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

            dataList.Add(row);
        }
    }

    // 배열 인덱스 초과 방지 및 빈 값 처리
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
        float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out v);
        return v;
    }
}