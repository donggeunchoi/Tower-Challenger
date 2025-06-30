using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VillageManager : MonoBehaviour
{
    [Header("아이템 상점")]
    public GameObject StorePanel;
    public GameObject Item1;
    public GameObject Item2;
    public GameObject Item3;
    
    [Header("인벤토리")]
    public GameObject Invetory;

    [Header("우편함")] 
    public GameObject MailBox;

    [Header("옷가게")] 
    public GameObject ClothesShopPanel;

    [Header("길드")] 
    public GameObject Guild;

    [Header("설정")] 
    public GameObject StopPanel;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VillageMove(string villageName)
    {
        switch (villageName)
        {
            case "StorePanel":
                StorePanel.SetActive(true);
                break;
            case "Invetory":
                Invetory.SetActive(true);
                break;
            case "MailBox":
                MailBox.SetActive(true);
                break;
            case "ClothesShopPanel":
                ClothesShopPanel.SetActive(true);
                break;
            case "Guild":
                Guild.SetActive(true);
                break;
            case "StopPanel":
                StopPanel.SetActive(true);
                break;
            
            
        }
    }

    public void OnClickOutStore()
    {
        StorePanel.SetActive(false);
    }

    public void OnClickTowerMove()
    {
        SceneManager.LoadScene("GameScene");
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

    public void OnClickItemRemove(int itemnumber)
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
}
