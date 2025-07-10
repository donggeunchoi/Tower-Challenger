using Unity.VisualScripting;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public ItemData itemData; //바뀐부분! 아이템 데이터로 들고있기!

    
    public void OnClickUse()
    {
        Debug.Log("여기는 실행 되니?");
        if (itemData != null)
        {
            Items items = this.GetComponent<Items>();
            switch (itemData.type) //타입으로 실행!
            {
                case ItemType.TopTicket:  //티켓인 경우!
                    if (items.TopTicketUse())
                    {
                        Inventory.instance.OnClickUseButton(itemData);  //아이템 데이터를 넘겨주도록 변경!
                    }
                    break;
                case ItemType.SandGlass:  //모래시계인 경우!
                    if (items.SandGlass())
                    {
                        Inventory.instance.OnClickUseButton(itemData);
                    }
                    break;
                case ItemType.MoveSpeedUp: //이동속도 주문서인 경우!
                    if (items.MoveSpeedUp())
                    { 
                        Inventory.instance.OnClickUseButton(itemData);
                    }
                    break;
            }
        }
    }
}
