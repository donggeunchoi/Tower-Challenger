using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject Invetory;
    public GameObject InventoryItem;
    
    public void OnClickInventoryClose()
    {
        Invetory.SetActive(false);
    }

    public void OnClickItemOpen()
    {
        InventoryItem.SetActive(true);
    }

    public void OnClickItemClose()
    {
        InventoryItem.SetActive(false);
    }
   
}
