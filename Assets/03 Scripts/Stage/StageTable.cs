using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class StageTable
{
    [System.Serializable]
    public class PotalStageData
    {
        public string index;        
        public int floor;           
        public string desc;         
        public string type;         
        public int potalMin;        
        public int potalMax;        
        public bool boss;           
        public bool LpRealse;       
    }
    [System.Serializable]
    public class DebuffTable //슬로우 테이블입니다
    {
        public string index;
        public string name;
        public string desc;
        public string type;
        public int floor;
        public int diflev;
        public string downSP;
        public int eftTime;
    }
    [System.Serializable]
    public class DebuffStunTableRow
    {
        public string index;
        public string name;
        public string dec;
        public string type;
        public int floor;
        public int diflev;
        public int eftTime;
        public bool imm;
    }
    [System.Serializable]
    public class TrapPerTableRow
    {
        public string index;   // ex) Trap1001 (있는 row만)
        public string name;    // ex) Trap
        public string dec;     // ex) 트랩 확률
        public string type;    // ex) trap
        public int floor;      // ex) 1~30
        public int hitPer;     // ex) 60, 50 등등
        public int slowPer;
        public int stuPer;
    }

    [System.Serializable]
    public class TrapCountTableRow
    {
        public string index;        // ex) di1001
        public string name;         // ex) disBuff
        public string dec;          // 설명
        public int floor;           // 층수
        public bool disBuff;        // 예시: TRUE/FALSE
        public string disBuffCount; // 예시: 1 ~ 2 또는 2 또는 2~3
        public int oneDisPer;
        public int twoDisPer;
        public int thrDisPer;
        public int foDisPer;
        public int fiveDisPer;
    }

    public static List<TrapCountTableRow> trapCountTableList = new List<TrapCountTableRow>();
    public static List<DebuffTable> debuffSlowTableList = new List<DebuffTable>();
    public static List<DebuffStunTableRow> debuffStunTableList = new List<DebuffStunTableRow>();
    public static List<TrapPerTableRow> trapPerTableList = new List<TrapPerTableRow>();
}
