using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CharactorData", menuName = "Scriptable Objects/CharactorData")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public Sprite characterImage;
    public Sprite inGameImage;
    public int Price;
}
