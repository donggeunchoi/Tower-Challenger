using UnityEngine;

public class Resources : MonoBehaviour
{
    public int gold { get; private set; }
    public int diamond { get; private set; }

    public void LoadResource()
    {
        gold = Save.playerData.gold;
        diamond = Save.playerData.diamond;
    }

    public void AddGold(int addGold)
    {
        gold += addGold;
        Debug.Log("획득한 골드 :" + addGold);
        Save.SaveData();
    }

    public void UseGold(int useGold)
    {
        gold -= useGold;
        Save.SaveData();
    }

    public void AddDiamond(int addDia)
    {
        diamond += addDia;
        Save.SaveData();
    }

    public void UseDiamond(int useDia)
    {
        diamond -= useDia;
        Save.SaveData();
    }
}
