using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject Invetory;
    public Transform SlotParent;
    public GameObject SlotPrefab;
    public bool isTuto = false;

    private List<GameObject> inventorySlots = new List<GameObject>();
   

    public static Inventory instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

    private void OnEnable()  //인벤토리가 활성화 될 때
    {
        if (isTuto)
        {
            UpdateTutoInven();
            return;
        }

        UpdateInventory();
    }

    public void OnClickInventoryClose()
    {
        Invetory.SetActive(false);
    }


    public void OnClickUseButton(ItemData item) //아이템 데이터를 받아오는 것으로 변경!
    {
        //ItemData item = slot.GetComponent<ItemData>();
        // string itemName = slot.GetComponent<InventorySlot>().itemName;
        RemoveItem(item);
    }

    public void RemoveItem(ItemData item)
    {
        ItemManager.instance.items.Remove(item);  //아이템 매니저에서 아이템 제거!

        if (Invetory.activeSelf)                  //이후 인벤토리 업데이트!
        {
            UpdateInventory();
        }                        

        //// 1. 아이템 매니저에서 먼저 제거 (첫 번째 것 하나만)
        //int index = ItemManager.instance.items.IndexOf(item);

        //if (index >= 0)
        //{
        //    ItemManager.instance.items.RemoveAt(index);
        //}

        //// 2. 인벤토리 슬롯에서도 동일한 이름 중 하나 제거
        //for (int i = 0; i < inventorySlots.Count; i++)
        //{
        //    InventorySlot inv = inventorySlots[i].GetComponent<InventorySlot>();

        //    if (inv != null && inv.itemData.type == item.type)
        //    {
        //        Destroy(inventorySlots[i]);
        //        inventorySlots.RemoveAt(i);
        //        break; // 첫 번째 것만 제거하고 탈출
        //    }
        //}
    }

    public void UpdateInventory()
    {

        foreach (GameObject slot in inventorySlots)
        {
            Destroy(slot);
        }
        inventorySlots.Clear();

        // List<ItemData> names = ItemManager.instance.items;
        // List<ItemData> icons = ItemManager.instance.items;
        

        List<ItemData> items = new List<ItemData>();

        if (ItemManager.instance != null)              //아이템매니저에서 아이템 가져오기!
        {    
            items.AddRange(ItemManager.instance.items);
        }
        
        // int index = Mathf.Min(names.Count, icons.Count);

        for (int i = 0; i < items.Count; i++)
        {
            GameObject newSlot = Instantiate(SlotPrefab, SlotParent);

            newSlot.SetActive(true);
            inventorySlots.Add(newSlot);

            InventorySlot slot = newSlot.GetComponent<InventorySlot>();  //일단 인벤토리 슬롯을 가져와서!
            slot.itemData = items[i];                                    //해당 슬롯의 아이템 데이터를 넣어주고!

            newSlot.GetComponentInChildren<TMP_Text>().text = items[i].itemName; //아이템 이름이랑 이미지!
            newSlot.GetComponent<Image>().sprite = items[i].icon;                //코드는 동일!
            
            
            // newSlot.GetComponentInChildren<TMPro.TMP_Text>().text = items[i];
            // newSlot.GetComponent<InventorySlot>().itemName = items[i];
            // newSlot.GetComponent<Image>().sprite = items[i];
        }
    }

    private void UpdateTutoInven()
    {
        foreach (GameObject slot in inventorySlots)
        {
            Destroy(slot);
        }
        inventorySlots.Clear();

        List<ItemData> items = new List<ItemData>();

        if (ItemManager.instance != null)              //아이템매니저에서 아이템 가져오기!
        {
            items.AddRange(ItemManager.instance.allItemList);
        }

        for (int i = 0; i < items.Count; i++)
        {
            GameObject newSlot = Instantiate(SlotPrefab, SlotParent);

            newSlot.SetActive(true);
            inventorySlots.Add(newSlot);

            InventorySlot slot = newSlot.GetComponent<InventorySlot>();  //일단 인벤토리 슬롯을 가져와서!
            slot.itemData = items[i];                                    //해당 슬롯의 아이템 데이터를 넣어주고!

            newSlot.GetComponentInChildren<TMP_Text>().text = items[i].itemName; //아이템 이름이랑 이미지!
            newSlot.GetComponent<Image>().sprite = items[i].icon;                //코드는 동일!
        }
    }
}
