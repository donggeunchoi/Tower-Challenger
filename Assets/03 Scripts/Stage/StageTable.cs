using UnityEngine;

public class StageTable : MonoBehaviour
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
}
