using UnityEngine;
using UnityEngine.UI;

public enum PriceType
{
    dia,
    gold
}

[CreateAssetMenu(fileName = "CharactorData", menuName = "Scriptable Objects/CharactorData")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public Sprite characterImage;
    public Sprite inGameImage;
    public string description;
    public PriceType priceType;
    public int Price;
    public GameObject playerPrefab;
}
