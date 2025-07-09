using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [Header("아이템 상점")] public GameObject StorePanel;

    public ItemData moveSpeed;
    public ItemData sandGlass;
    public ItemData topTicket;
    
    
    public GameObject Item1;
    public GameObject Item2;
    public GameObject Item3;

    
    public void OnClickCloseStore()
    {
        StorePanel.SetActive(false);
    }

    public void OnClickItem1()
    {
        Item1.SetActive(true);
    }

    public void OnClickItem2()
    {
        Item2.SetActive(true);
    }

    public void OnclickItem3()
    {
        Item3.SetActive(true);
    }

    public void OnClickItemClose(int itemnumber)
    {
        switch (itemnumber)
        {
            case 0:
                Item1.SetActive(false);
                break;
            case 1:
                Item2.SetActive(false);
                break;
            case 2:
                Item3.SetActive(false);
                break;
        }
    }
    public void BuyItem_SpeedUp()
    {
        ItemManager.instance.AddItem(moveSpeed);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.UseGold(moveSpeed.price);
        }
    }

    public void BuyItem_Sandglass()
    {
        ItemManager.instance.AddItem(sandGlass);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.UseGold(sandGlass.price);
        }
    }
    public void BuyItem_TopTicket()
    {
        ItemManager.instance.AddItem(topTicket);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.UseDiamond(topTicket.price);
        }
    }
    

    
}
