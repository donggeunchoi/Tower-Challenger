﻿using System;
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
    public List<string> itmeDataID = new List<string>();
    public List<string> characterNames = new List<string>(); // 보유 캐릭터 이름 리스트
    public string equippedCharacterName; // 장착 캐릭터 이름
    public bool tutorialCompleted; 
    public bool VillageTutorialCompleted;

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
            if (GameManager.Instance.account != null)
            {
                gold = GameManager.Instance.account.gold;
                diamond = GameManager.Instance.account.diamond;
            }

            if (GameManager.Instance.stamina != null)
            {
                stamina = GameManager.Instance.stamina.mainStamina;
                staminaTimer = GameManager.Instance.stamina.staminatimer;
            }
            
            if (GameManager.Instance.character != null)
            {
                characterNames.Clear();
                foreach (CharacterData characters in GameManager.Instance.character.charaters)
                    characterNames.Add(characters.characterName);

                if (GameManager.Instance.character.equippedCharacter != null)
                    equippedCharacterName = GameManager.Instance.character.equippedCharacter.characterName;
            }
        }

        if (StageManager.instance != null)
            bestFloor = StageManager.instance.bestFloor;

        if (ItemManager.instance != null)
        {
            itmeDataID.Clear();
            foreach (ItemData itemData in ItemManager.instance.items)
                itmeDataID.Add(itemData.itemID);
        }

        SaveManager.SaveUsers(this);
    }

    public void LoadData()
    {
        PlayerData loaded = SaveManager.LoadUsers();

        if (loaded == null)
        {
            this.bestFloor = 0;
            this.gold = 0;
            this.diamond = 0;
            this.stamina = 5;
            this.lastTime = DateTime.Now;
            this.staminaTimer = 0;
            this.tutorialCompleted = false;
            this.VillageTutorialCompleted = false;
        }
        else
        {
            this.bestFloor = loaded.bestFloor;
            this.gold = loaded.gold;
            this.diamond = loaded.diamond;
            this.stamina = loaded.stamina;
            this.lastTime = loaded.lastTime;
            this.itmeDataID = loaded.itmeDataID;
            this.characterNames = loaded.characterNames;
            this.equippedCharacterName = loaded.equippedCharacterName;
            this.staminaTimer = loaded.staminaTimer;
            this.itmeDataID = loaded.itmeDataID;
            this.tutorialCompleted = loaded.tutorialCompleted;
            this.VillageTutorialCompleted = loaded.VillageTutorialCompleted;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadResourceData();
        }

        if (StageManager.instance != null)
            StageManager.instance.bestFloor = bestFloor;

        if (ItemManager.instance != null)
        {
            ItemManager.instance.LoadItem();
        }
    }
}
