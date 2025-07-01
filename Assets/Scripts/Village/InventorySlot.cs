using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public string itemName;

    public void OnClickUse()
    {
        switch (itemName)
        {
            case "Topticket":
                Inventory.instance.OnClickUseButton(this.gameObject);
                break;
            case "Sandglass":
                Inventory.instance.OnClickUseButton(this.gameObject);
                break;
            case "SpeedUp":
                Inventory.instance.OnClickUseButton(this.gameObject);
                break;
        }
    }
}
