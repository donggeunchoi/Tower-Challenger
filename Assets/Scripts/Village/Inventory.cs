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
            inventorySlots.Remove(slot);
            Destroy(slot);
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
