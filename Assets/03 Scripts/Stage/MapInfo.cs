using System.Collections.Generic;
using System;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    public static class StageTempMemory
    {
        public static int currentFloor;
        public static DestroyedItemInfo destroyedInfo = new DestroyedItemInfo();
    }

    [Serializable]
    public class DestroyedItemInfo
    {
        public int floor;
        public List<string> destroyedTrapIds = new List<string>();
        public List<string> destroyedChestIds = new List<string>();
    }

}
