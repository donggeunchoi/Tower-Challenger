using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int bestFloor;
    public int gold;
    public int diamond;
    public int stamina;
    public float staminaTimer;
    public List<string> itmeDataName = new List<string>();
    public List<string> characterNames = new List<string>(); // 보유 캐릭터 이름 리스트
    public string equippedCharacterName; // 장착 캐릭터 이름

    public string lastTimeString;

    [NonSerialized]
    public DateTime lastTime;
    public void SaveData()
    {
        lastTime = DateTime.Now;
        lastTimeString = lastTime.ToString("yyyy-MM-ddTHH:mm:ss.fffK");

        //TimeSpan time 밀리초로 환산을해서 밀리초로 가지고있다가 분으로 환산해서
        if (GameManager.Instance != null)
        {
            gold = GameManager.Instance.gold;
            diamond = GameManager.Instance.diamond;
            stamina = GameManager.Instance.mainStamina;
            staminaTimer = GameManager.Instance.staminatimer;

            characterNames.Clear();
            foreach (CharacterData characters in GameManager.Instance.charactors)
                characterNames.Add(characters.characterName);

            if (GameManager.Instance.equimentCharacter != null)
                equippedCharacterName = GameManager.Instance.equimentCharacter.characterName;
        }

        if (StageManager.instance != null)
            bestFloor = StageManager.instance.bestFloor;

        if (ItemManager.instance != null)
        {
            itmeDataName.Clear();
            foreach (ItemData itemData in ItemManager.instance.items)
                itmeDataName.Add(itemData.itemName);
        }

        SaveManager.SaveUsers(this);
    }

    public void LoadData()
    {
        PlayerData loaded = SaveManager.LoadUsers();

        this.bestFloor = loaded.bestFloor;
        this.gold = loaded.gold;
        this.diamond = loaded.diamond;
        this.stamina = loaded.stamina;
        this.lastTime = loaded.lastTime;
        this.itmeDataName = loaded.itmeDataName;
        this.characterNames = loaded.characterNames;
        this.equippedCharacterName = loaded.equippedCharacterName;
        this.staminaTimer = loaded.staminaTimer;
        this.itmeDataName = loaded.itmeDataName;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.staminatimer = staminaTimer;

            if (!string.IsNullOrEmpty(lastTimeString))
                lastTime = DateTime.Parse(lastTimeString, null, System.Globalization.DateTimeStyles.RoundtripKind);
            else
                lastTime = DateTime.Now;

            float secondsPassed = (float)(DateTime.Now - lastTime).TotalSeconds;
            GameManager.Instance.staminatimer = staminaTimer + secondsPassed;

            if (this.stamina <= 0)  //테스트용 코드
                this.stamina = 5;

            GameManager.Instance.mainStamina = this.stamina;
            GameManager.Instance.gold = gold;
            GameManager.Instance.diamond = diamond;

            GameManager.Instance.charactors.Clear();
            foreach (string name in characterNames)
            {
                CharacterData data = GameManager.Instance.allCharacterData.Find(character => character.characterName == name);
                if (data != null)
                    GameManager.Instance.charactors.Add(data);
            }
            if (characterNames != null)
            {
                if (equippedCharacterName != null)
                {
                    CharacterData data = GameManager.Instance.allCharacterData.Find(character => character.characterName == equippedCharacterName);
                    if (data != null)
                        GameManager.Instance.equimentCharacter = data;
                }
            }
        }

        if (StageManager.instance != null)
            StageManager.instance.bestFloor = bestFloor;

        if (ItemManager.instance != null)
        {
            ItemManager.instance.items.Clear();
            foreach (string name in itmeDataName)
            {
                ItemData item = ItemManager.instance.allItemList.Find(item => item.name == name);
                if (item != null)
                    ItemManager.instance.items.Add(item);
            }
        }
    }
}
