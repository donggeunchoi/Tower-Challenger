using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
   public static ItemManager instance;
   //public Transform SlotParent;
   //public GameObject SlotPrefab;
   
   // public List<string> itemNames = new List<string>();
   // public List<Sprite> itemIcons = new List<Sprite>();
   
   public List<ItemData> items = new List<ItemData>();
   public List<RewardTableData> rewards = new List<RewardTableData>();
   
   public Sprite GoldSprite;
   public Sprite DiamondSprite;

   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
         DontDestroyOnLoad(this);
      }
      else
      {
         Destroy(gameObject);
      }
   }

   private void Start()
   {
      //SlotParent = GameObject.Find("Content").transform;
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SaveLoad();
        }
   }
   
   

   public void AddItem(ItemData item)
   {
        items.Add(item);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.SaveLoad();
        }
        //GameObject newSlot = Instantiate(SlotPrefab, SlotParent);

        //newSlot.SetActive(true);
        //items.Add(item);
        //// itemNames.Add(item.itemName);
        //// itemIcons.Add(item.icon);

        //newSlot.GetComponentInChildren<TMPro.TMP_Text>().text = item.itemName;
        //newSlot.GetComponent<InventorySlot>().itemName = item.itemName;
        //newSlot.GetComponent<Image>().sprite = item.icon;
    }

    public void RemoveItem(ItemData item)
    {
        items.Remove(item);              //삭제 추가!

        if (GameManager.Instance != null)
        {
            GameManager.Instance.SaveLoad();
        }
    }
    
    public void ClearItmes()
    {
        items.Clear();  //초기화 추가!
    }

    public ItemData GetItem(string name)//아이템데이터에서 아이템 이름 가져오기
    {
        return items.Find(item => item.name == name);
    }
}
