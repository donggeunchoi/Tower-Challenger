using System.Collections.Generic;
using UnityEngine;
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
        }
    }
    
    public void AddItme(string itemName, Image itemIcon)
    {
        GameObject newSlot = Instantiate(SlotPrefab, SlotParent);
        newSlot.SetActive(true);
        inventorySlots.Add(newSlot);

        newSlot.GetComponentInChildren<TMPro.TMP_Text>().text = itemName;
        newSlot.GetComponentInChildren<Image>().sprite = itemIcon.sprite; 
        newSlot.GetComponent<InventorySlot>().itemName = itemName;
        newSlot.GetComponent<Image>().sprite = itemIcon.sprite;
    }
    
    public void OnClickInventoryClose()
    {
        Invetory.SetActive(false);
    }


    public void OnClickUseButton(GameObject slot)
    {
        Debug.Log("눌렸니?");
        inventorySlots.Remove(slot);
        Destroy(slot);
    }
}
