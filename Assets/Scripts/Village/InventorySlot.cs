using Unity.VisualScripting;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public string itemName;

    
    public void OnClickUse()
    {
        Debug.Log("여기는 실행 되니?");
        switch (itemName)
        {
            case "타워 입장권 1매":
                if (Items.Instance.TopTicketUse())
                {
                    Inventory.instance.OnClickUseButton(this.gameObject);
                }
                break;
            case "마법의 모래시계 +10":
                if (Items.Instance.SandGlass())
                {
                    Inventory.instance.OnClickUseButton(this.gameObject);
                }
                break;
            case "이동속도 주문서":
                if (Items.Instance.MoveSpeedUp())
                {
                    Inventory.instance.OnClickUseButton(this.gameObject);
                }
                break;
        }
    }
}
