using UnityEngine;

public enum ItemType //아이템 타입 선언
{
    TopTicket,
    SandGlass,
    MoveSpeedUp
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemID; //이름이 한글이라 저장이 안되는지 실험
    public string itemName;
    public Sprite icon;
    public int price;
    public ItemType type;  //아이템 타입도 들고있기!
}
