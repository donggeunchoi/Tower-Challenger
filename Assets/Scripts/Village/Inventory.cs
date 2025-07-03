using System.Collections.Generic;
using TMPro;
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
        Debug.Log("눌렸니?");
        inventorySlots.Remove(slot);
        Destroy(slot);
    }

    public void UpdateInventory()
    {
        List<string> names = ItemManager.instance.itemNames;
        List<Sprite> icons = ItemManager.instance.itemIcons;

        if (names.Count == 0 || icons.Count == 0)
        {
            return;
        }

        int index = names.Count - 1;
        
        GameObject newSlot = Instantiate(SlotPrefab, SlotParent);
        newSlot.SetActive(true);
            
        newSlot.GetComponentInChildren<TMPro.TMP_Text>().text = names[index];
        newSlot.GetComponent<Image>().sprite = icons[index];
    }
}
