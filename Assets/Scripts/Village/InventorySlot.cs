using Unity.VisualScripting;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public string itemName;

    public void OnClickUse()
    {
        switch (itemName)
        {
            case "Topticket":
                Items.Instance.TopTicket();
                Inventory.instance.OnClickUseButton(this.gameObject);
                break;
            case "Sandglass":
                Items.Instance.Sandglass();
                Inventory.instance.OnClickUseButton(this.gameObject);
                break;
            case "SpeedUp":
                Items.Instance.MoveSpeedUp();
                Inventory.instance.OnClickUseButton(this.gameObject);
                break;
        }
    }
}
