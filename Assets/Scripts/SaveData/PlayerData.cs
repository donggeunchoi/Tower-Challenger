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
    public List<ItemData> itmeDatas = new List<ItemData>();
    public List<CharacterData> characterDatas = new List<CharacterData>();

    //public List<string> chracterId = new List<string>();
    //public string equimentCharcter = "";

    public DateTime lastTime;

    public void SaveData()
    {
        lastTime = DateTime.Now;

        //TimeSpan time 밀리초로 환산을해서 밀리초로 가지고있다가 분으로 환산해서
        if (GameManager.Instance != null)
        {
            gold = GameManager.Instance.gold;
            diamond = GameManager.Instance.diamond;
            stamina = GameManager.Instance.mainStamina;
            staminaTimer = GameManager.Instance.staminatimer;
        }

        if (StageManager.instance != null)
            bestFloor = StageManager.instance.bestFloor;
        
        if (ItemManager.instance != null)
        {
            itmeDatas.Clear();
            itmeDatas.AddRange(ItemManager.instance.items);
        }
            
        if (CharactorChoice.instance != null)
        {
            for (int i = 0; i < CharactorChoice.instance.CharactorSlots.Count; i++)
            {
                if (CharactorChoice.instance != null)
                    characterDatas.AddRange(CharactorChoice.instance.charactors);
            }
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
        this.itmeDatas = loaded.itmeDatas;
        this.characterDatas = loaded.characterDatas;
        this.staminaTimer = loaded.staminaTimer;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.staminatimer = staminaTimer;

            TimeSpan diff = DateTime.Now - lastTime;
            float secondsPassed = (float)diff.TotalSeconds;

            float totalTimer = staminaTimer + secondsPassed;

            int recoverInterval = (int)GameManager.STAMINA_TIME;
            int maxStamina = GameManager.MAX_STAMINA;
            int recovered = (int)(totalTimer / recoverInterval);
            int newStamina = Math.Min(stamina + recovered, maxStamina);

            float remainTimer = totalTimer % recoverInterval;

            GameManager.Instance.mainStamina = newStamina;
            GameManager.Instance.gold = gold;
            GameManager.Instance.diamond = diamond;
            GameManager.Instance.mainStamina = stamina;
        }

        if (StageManager.instance != null)
            StageManager.instance.bestFloor = bestFloor;

        if (ItemManager.instance != null)
        {
            ItemManager.instance.items.Clear();
            ItemManager.instance.items.AddRange(itmeDatas);
        }

        if (CharactorChoice.instance != null)
        {
            if (CharactorChoice.instance != null)
                CharactorChoice.instance.charactors.AddRange(characterDatas);
        }
    }
}
