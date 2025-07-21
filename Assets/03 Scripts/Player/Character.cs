using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Character : MonoBehaviour
{
    public List<CharacterData> allCharacterList = new List<CharacterData>();
    [SerializeField] private CharacterData defaltCharater;
    public List<CharacterData> charaters { get; private set; } = new List<CharacterData>();
    public CharacterData equippedCharacter { get; private set; }

    public void LoadCharacters()
    {
        charaters.Clear();
        if (Save.playerData.characterNames != null)
        {
            foreach (string name in Save.playerData.characterNames)
            {
                CharacterData data = allCharacterList.Find(c => c.characterName == name);
                if (data != null)
                    charaters.Add(data);
            }
            charaters = charaters.GroupBy(c => c.characterName).Select(g => g.First()).ToList();
            //GroupBy 그룹으로 하나로 묶기 예를들어 First는 그룹에서 대표로 하나만 뽑는 것
            //Select는 각 그룹에서 원하는 값만 뽑아내는 것
            //결과적으로 Rin Rin Luke BlueSlime 이렇게 데이터 값이 들어있으면
            //Rin (하나뽑았으니까 다음) Luke, BlueSlime 이런식으로 뽑아온다
            //결론적으로 Rin Luke BlueSlime 이렇게 세개만 있는 리스트로 변환
        }
        if (Save.playerData.equippedCharacterName != null && Save.playerData.equippedCharacterName != "")
        {
            CharacterData equip = allCharacterList.Find(c => c.characterName == Save.playerData.equippedCharacterName);
            if (equip != null)
                equippedCharacter = equip;
        }

        if (defaltCharater != null)
        {
            if (equippedCharacter == null)
                equippedCharacter = defaltCharater;
        }
    }

    public bool AddCharacter(string characterName)
    {
        foreach (CharacterData character in charaters)
        {
            if (character.characterName == characterName)
            { 
                return false;
            }
        }

        CharacterData data = allCharacterList.Find(c => c.characterName == characterName);
        if (data == null)
            return false;

        charaters.Add(data);

        if (GameManager.Instance != null)
            GameManager.Instance.SaveData();

        return true;
    }

    public bool RemoveCharacter(string characterName)
    {
        CharacterData data = charaters.Find(c => c.characterName == characterName);
        if (data == null)
            return false;

        if (equippedCharacter != null && equippedCharacter.characterName == characterName)
            equippedCharacter = null;

        charaters.Remove(data);
        Save.playerData.characterNames.Remove(characterName);

        if (GameManager.Instance != null)
            GameManager.Instance.SaveData();

        return true;
    }

    public bool EquipCharacter(string characterName)
    {
        CharacterData data = charaters.Find(c => c.characterName == characterName);
        if (data == null)
            return false;

        equippedCharacter = data;
        Save.playerData.equippedCharacterName = characterName;

        if (GameManager.Instance != null)
            GameManager.Instance.SaveData();

        return true;
    }

    public CharacterData GetCharacter(string characterName)
    {
        return allCharacterList.Find(c => c.characterName == characterName);
    }
}