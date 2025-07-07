using UnityEngine;

[CreateAssetMenu(fileName = "NPCData", menuName = "Scriptable Objects/NPCData")]
public class NPCData : ScriptableObject
{
    public string npcName;
    public Sprite npcImage;
    public string npcDescription;
    public float MoveSpeed;
    public float MoveDistance;
    public float StopDuration;
}
