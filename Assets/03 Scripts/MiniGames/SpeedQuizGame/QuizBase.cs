using System.Collections.Generic;
using System.IO;
using Unity.Cinemachine;
using UnityEngine;

public class QuizBase : MonoBehaviour
{
    public TextAsset QuizFile;
    public static List<SpeedQuizData> QuizList = new List<SpeedQuizData>();

    private void Awake()
    {
        LoadQuiz();
    }

    public void LoadQuiz()
    {
        QuizFile = Resources.Load<TextAsset>("SpeedQuizData");
        if (QuizFile == null)
        {
            Debug.LogError("csvFile이 없는디요?");
        }
        
        string[] lines = QuizFile.text.Split('\n');
        for (int i = 1; i < lines.Length; i++) // 헤더 제외
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            
            string[] values = lines[i].Split(',');
            
            SpeedQuizData row = new SpeedQuizData();
            
            row.index = GetValue(values,0);
            row.name = GetValue(values,1);
            row.question = GetValue(values,3);
            row.answer = GetValue(values,4);
            row.op2 = GetValue(values,5);
            row.op3 = GetValue(values,6);
            row.op4 = GetValue(values,7);
           
            
            
            QuizList.Add(row);
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
    

