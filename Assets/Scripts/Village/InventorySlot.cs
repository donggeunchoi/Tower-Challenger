using Unity.VisualScripting;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public string itemName;

    public void OnClickUse()
    {
        switch (itemName)
        {
            case "타워 입장권":
                Inventory.instance.OnClickUseButton(this.gameObject);
                break;
            case "마법의 모래시계":
                Inventory.instance.OnClickUseButton(this.gameObject);
                break;
            case "이동속도 주문서":
                Inventory.instance.OnClickUseButton(this.gameObject);
                break;
        }
    }
}
