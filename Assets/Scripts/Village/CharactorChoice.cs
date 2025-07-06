using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactorChoice : MonoBehaviour
{
    public GameObject CharactorChoicePanel;
    public Transform SlotParent;
    public GameObject SlotPrefab;

    private List<GameObject> CharactorSlots = new List<GameObject>();

    public static CharactorChoice instance;

    private void Awake()
    {
        instance = this;
    }
    
    public void AddItem(CharacterData data)
    {
        GameObject newSlot = Instantiate(SlotPrefab, SlotParent);
        newSlot.SetActive(true);
        CharactorSlots.Add(newSlot);

        CharactorChoiceSlot slotScript = newSlot.GetComponent<CharactorChoiceSlot>();
        slotScript.CharactorChoice = this;
        slotScript.data = data;
            
        TMPro.TMP_Text nameText = newSlot.GetComponentInChildren<TMPro.TMP_Text>();
        Image icon =  newSlot.GetComponent<Image>();

        if (nameText != null)
        {
            nameText.text = data.characterName;
        }

        if (icon != null)
        {
            icon.sprite = data.characterImage;
        }
        
        
    }
    
    public void OnClickCharactorClose()
    {
        CharactorChoicePanel.SetActive(false);
    }

    public void EquipOnly(CharactorChoiceSlot Choiceslot)
    {
        foreach (GameObject slotObject in CharactorSlots)
        {
            CharactorChoiceSlot slot = slotObject.GetComponent<CharactorChoiceSlot>();
            if (slot != null)
            {
                bool isSelected = (slot == Choiceslot);
                slot.Equip = isSelected;
                slot.EquipImage.SetActive(isSelected);
            }
        }
    }


}
