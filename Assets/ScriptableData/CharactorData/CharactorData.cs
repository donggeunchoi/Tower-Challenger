using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CharactorData", menuName = "Scriptable Objects/CharactorData")]
public class CharactorData : ScriptableObject
{
    public string characterName;
    public Sprite characterImage;
    public Sprite inGameImage;
}
