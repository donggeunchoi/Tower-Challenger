using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class CharactorChoiceSlot : MonoBehaviour
{
    // public string charactorName;
    public bool Equip = false;
    public GameObject EquipImage;
    public CharactorChoice CharactorChoice;

    public void OnClickUse()
    {
        if (CharactorChoice != null)
        {
            CharactorChoice.EquipOnly(this);
        }
        else
        {
            Debug.Log("연결안됨요");
        }
    }
}
