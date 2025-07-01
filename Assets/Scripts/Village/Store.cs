using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [Header("아이템 상점")] public GameObject StorePanel;
    public GameObject Item1;
    public GameObject Item2;
    public GameObject Item3;
    public Image Item1Image;
    public Image Item2Image;
    public Image Item3Image;

    
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
        Inventory.instance.AddItme("이동속도 주문서", Item1Image);
    }

    public void BuyItem_Sandglass()
    {
        Inventory.instance.AddItme("마법의 모래시계", Item2Image);
    }
    public void BuyItem_TopTicket()
    {
        Inventory.instance.AddItme("타워 입장권",Item3Image);
    }
    

    
}
