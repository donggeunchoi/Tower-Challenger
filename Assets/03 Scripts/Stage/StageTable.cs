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
    public static List<DebuffTable> debuffSlowTableList = new List<DebuffTable>();
    public static List<DebuffStunTableRow> debuffStunTableList = new List<DebuffStunTableRow>();
}
