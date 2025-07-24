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
        charaters.Clear(); //리스트 비우고
        if (Save.playerData.characterNames != null) //저장된 데이터 검사
        {
            foreach (string name in Save.playerData.characterNames)  //캐릭터 이름목록으로 캐릭터 불러와서 추가하기
            {
                CharacterData data = allCharacterList.Find(c => c.characterName == name);
                if (data != null)
                    charaters.Add(data);
            }
            charaters = charaters.GroupBy(c => c.characterName).Select(g => g.First()).ToList();  //대표값 뽑아서 중복 x처리
            //GroupBy 그룹으로 하나로 묶기 예를들어 First는 그룹에서 대표로 하나만 뽑는 것
            //Select는 각 그룹에서 원하는 값만 뽑아내는 것
            //결과적으로 Rin Rin Luke BlueSlime 이렇게 데이터 값이 들어있으면
            //Rin (하나뽑았으니까 다음) Luke, BlueSlime 이런식으로 뽑아온다
            //결론적으로 Rin Luke BlueSlime 이렇게 세개만 있는 리스트로 변환
        }

        if (defaltCharater != null && !charaters.Any(c => c.characterName == defaltCharater.characterName)) //캐릭터 목록에 기본캐릭터가 없을 경우 추가
            charaters.Add (defaltCharater);

        if (Save.playerData.equippedCharacterName != null && Save.playerData.equippedCharacterName != "")  //장착된 캐릭터가 있다면
        {
            CharacterData equip = charaters.Find(c => c.characterName == Save.playerData.equippedCharacterName);  //해당 캐릭터 탐색
            if (equip != null)
                equippedCharacter = equip;  //있다면 장착   
        }
        else
        {
            if (defaltCharater != null)
                equippedCharacter = defaltCharater;  //없으면 기본캐릭터 장착
        }

        if (PlayerManager.Instance != null)  //플레이어 매니저에도 추가
        {
            if (equippedCharacter != null && equippedCharacter.playerPrefab != null)
                PlayerManager.Instance.LoadPlayer(equippedCharacter.playerPrefab);
            else if (defaltCharater != null && defaltCharater.playerPrefab != null)
                PlayerManager.Instance.LoadPlayer(defaltCharater.playerPrefab);
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
            Save.SaveData();

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
            Save.SaveData();

        return true;
    }

    public bool EquipCharacter(string characterName)
    {
        CharacterData data = charaters.Find(c => c.characterName == characterName);
        if (data == null)
            return false;

        equippedCharacter = data;
        Save.playerData.equippedCharacterName = characterName;

        if (PlayerManager.Instance != null)
            PlayerManager.Instance.LoadPlayer(equippedCharacter.playerPrefab);

        if (GameManager.Instance != null)
            Save.SaveData();

        return true;
    }

    public CharacterData GetCharacter(string characterName)
    {
        return allCharacterList.Find(c => c.characterName == characterName);
    }
}