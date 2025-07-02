using UnityEngine;

[CreateAssetMenu(fileName = "MiniGameData", menuName = "New MiniGame")]
public class MapData : ScriptableObject
{
    [Header("씬 이름")] public string sceneName;

    [Header("보스")] 



    [Header("최고 난이도")][Min(1)] public int max_difficult = 1;

    [Header("모든 스테이지에서 등장")] public bool allStage = true;

    [Header("만약에 모든 스테이지에서 등장하지 않는다면 최대 최소 스테이지")]
    [Min(1)] public int minStage = 1;
    [Min(1)] public int maxStage = 1;


}
