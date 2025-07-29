using System;
using System.Collections.Generic;
using UnityEngine;

public class BoxDataTable
{
    [System.Serializable]
    public class ArrowDataTableRow
    {
        public string index;
        public string name;
        public string dec;
        public int floor;
        public bool waring;
        public int arrowPer;
        public float arrowSP;
        public float timing;
        public int damege;
    }
    [Serializable]
    public class BoxDataTableRow
    {
        public string index;
        public string name;
        public string dec;
        public int floor;
        public bool box;
        public int boxCount;
        public int oneBoxPer;
        public int twoBoxPer;
        public int thrBoxPer;
    }
    [Serializable]
    public class GoldBoxDataTableRow
    {
        public string index;
        public string name;
        public string dec;
        public int floor;
        public int goldPer;
        public int goldRe;
    }

    [Serializable]
    public class NekoManDataTableRow
    {
        public string index;
        public string name;
        public string dec;
        public int floor;
        public bool waring;
        public int nekoPer;
        public float neokSP;
        public float timing;
        public int damege;
    }

    [Serializable]
    public class PlusLPDataTableRow
    {
        public string index;
        public string name;
        public string dec;
        public int floor;
        public int plusL;
        public int lpPer;
        public int oneLPPer;
        public int twoLPPer;
    }
    public static List<BoxDataTableRow> boxDataList = new List<BoxDataTableRow>();
    public static List<GoldBoxDataTableRow> goldBoxDataList = new List<GoldBoxDataTableRow>();
    public static List<NekoManDataTableRow> nekoManDataList = new List<NekoManDataTableRow>();
    public static List<PlusLPDataTableRow> plusLPDataList = new List<PlusLPDataTableRow>();
    public static List<ArrowDataTableRow> arrowDataList = new List<ArrowDataTableRow>();
}
