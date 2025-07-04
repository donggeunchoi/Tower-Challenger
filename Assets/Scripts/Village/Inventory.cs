using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject Invetory;
    public Transform SlotParent;
    public GameObject SlotPrefab;

    private List<GameObject> inventorySlots = new List<GameObject>();
   

    public static Inventory instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateInventory();
    }
    
    public void OnClickInventoryClose()
    {
        Invetory.SetActive(false);
    }


    public void OnClickUseButton(GameObject slot)
    {
        
        if (Items.Instance.use == false)
        {
            Debug.Log("사용안해서 못넘겨요");
        }
        else
        {
            string itemName = slot.GetComponent<InventorySlot>().itemName;
            RemoveItem(itemName);
        }
    }

    public void RemoveItem(string itemName)
    {
        // 1. 아이템 매니저에서 먼저 제거 (첫 번째 것 하나만)
        int index = ItemManager.instance.itemNames.IndexOf(itemName);
        if (index >= 0)
        {
            ItemManager.instance.itemNames.RemoveAt(index);
            ItemManager.instance.itemIcons.RemoveAt(index);
        }

        // 2. 인벤토리 슬롯에서도 동일한 이름 중 하나 제거
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            InventorySlot inv = inventorySlots[i].GetComponent<InventorySlot>();
            if (inv != null && inv.itemName == itemName)
            {
                Destroy(inventorySlots[i]);
                inventorySlots.RemoveAt(i);
                break; // 첫 번째 것만 제거하고 탈출
            }
        }
    }

    public void UpdateInventory()
    {
        foreach (GameObject slot in inventorySlots)
        {
            Destroy(slot);
        }
        inventorySlots.Clear();
        
        List<string> names = ItemManager.instance.itemNames;
        List<Sprite> icons = ItemManager.instance.itemIcons;

        // int index = Mathf.Min(names.Count, icons.Count);

        for (int i = 0; i < names.Count; i++)
        {
            GameObject newSlot = Instantiate(SlotPrefab, SlotParent);
            newSlot.SetActive(true);
            inventorySlots.Add(newSlot);
                
            newSlot.GetComponentInChildren<TMPro.TMP_Text>().text = names[i];
            newSlot.GetComponent<InventorySlot>().itemName = names[i];
            newSlot.GetComponent<Image>().sprite = icons[i];
            
        }
    }
    
}
