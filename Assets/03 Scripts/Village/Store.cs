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
        if (GameManager.Instance != null)
        {
            if (moveSpeed.price > Save.playerData.gold)
            {
                Debug.Log("돈 부족이요");
            }
            else
            {
                ItemManager.instance.AddItem(moveSpeed);
                GameManager.Instance.UseGold(moveSpeed.price);
            }
        }
    }

    public void BuyItem_Sandglass()
    {
        if (GameManager.Instance != null)
        {
            if (sandGlass.price > Save.playerData.gold)
            {
                Debug.Log("골드 부족이요");
            }
            else
            {
                ItemManager.instance.AddItem(sandGlass);
                GameManager.Instance.UseGold(sandGlass.price);
            }
        }
    }
    public void BuyItem_TopTicket()
    {
        if (GameManager.Instance != null)
        {
            if (topTicket.price > Save.playerData.diamond)
            {
                Debug.Log("다이아 부족이요");
            }
            else
            {
                ItemManager.instance.AddItem(topTicket);
                GameManager.Instance.UseDiamond(topTicket.price);
            }
        }
    }
    

    
}
