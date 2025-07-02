using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class CharactorChoiceSlot : MonoBehaviour
{
    // public string charactorName;
    public bool Equip = false;
    public GameObject EquipImage;

    public void OnClickUse()
    {
        Equip = !Equip;
        EquipImage.SetActive(Equip);
    }
}
