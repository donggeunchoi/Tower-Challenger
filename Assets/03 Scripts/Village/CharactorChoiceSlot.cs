using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class CharactorChoiceSlot : MonoBehaviour
{
    // public string charactorName;
    public bool Equip = false;
    public GameObject EquipImage;
    public CharactorChoice CharactorChoice;
    
    public CharacterData data;

    public void OnClickUse()
    {
        if (CharactorChoice != null)
        {
            CharactorChoice.EquipOnly(this);
            Debug.Log($"{data.characterName}으로 변경했쥬?");
        }
        else
        {
            Debug.Log("연결안됨요");
        }
    }
}
