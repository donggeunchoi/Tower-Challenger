using UnityEngine;

[CreateAssetMenu(fileName = "MiniGameData", menuName = "New MiniGame")]

public class MiniGameData : ScriptableObject
{
    [Header("미니게임 프리팹")] public GameObject miniGamePrefab;
    [Header("최고 난이도")][Min(1)] public int max_difficult = 1;

    [Header("모든 스테이지에서 등장")] public bool allStage = true;
    [Header("스테이지 등장 최소")][Min(1)] public int minStage = 1;
    [Header("스테이지 등장 최대")][Min(1)] public int maxStage= 1;
}
