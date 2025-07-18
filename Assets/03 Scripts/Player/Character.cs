using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public List<CharacterData> allCharacterData { get; private set; } = new List<CharacterData>();
    public List<CharacterData> charactors = new List<CharacterData>();
    public CharacterData equimentCharacter;

    public void LoadCharacter()
    {
        charactors.Clear();
        foreach (string name in Save.playerData.characterNames)
        {
            CharacterData data = allCharacterData.Find(character => character.characterName == name);
            if (data != null)
                charactors.Add(data);
        }
        if (Save.playerData.characterNames != null)
        {
            if (Save.playerData.equippedCharacterName != null)
            {
                CharacterData data = allCharacterData.Find(character => character.characterName == Save.playerData.equippedCharacterName);
                if (data != null)
                    equimentCharacter = data;
            }
        }
    }

    public void AddCharacter()
    {

    }
}
