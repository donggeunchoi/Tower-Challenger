using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactorChoice : MonoBehaviour
{
    public GameObject CharactorChoicePanel;
    public Transform SlotParent;
    public GameObject SlotPrefab;

    public Image equippedCharactorImage;

    private List<GameObject> CharactorSlots = new List<GameObject>();

    public static CharactorChoice instance;

    private void Awake()
    {
        instance = this;
    }
    
    public void AddItem(string CharactorName, Image CharactorIcon)
    {
        GameObject newSlot = Instantiate(SlotPrefab, SlotParent);
        newSlot.SetActive(true);
        CharactorSlots.Add(newSlot);
        
        TMPro.TMP_Text nameText = newSlot.GetComponentInChildren<TMPro.TMP_Text>();
        Image icon =  newSlot.GetComponent<Image>();
        CharactorChoiceSlot slotScript = newSlot.GetComponent<CharactorChoiceSlot>();

        if (slotScript != null)
        {
            slotScript.CharactorChoice = this;
            slotScript.IconImage =  CharactorIcon;
        }

        if (nameText != null)
        {
            nameText.text = CharactorName;
        }
        else
        {
            Debug.LogError("텍스트 컴포넌트 확인해봐라");
        }

        if (icon != null && CharactorIcon != null)
        {
            icon.sprite = CharactorIcon.sprite;
        }
        else
        {
            Debug.LogError("icon, CharactorIcon 확인해봐라");
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

                if (isSelected)
                {
                    equippedCharactorImage = slot.IconImage;
                }
            }
        }
    }

    public Image SandCharactorImage()
    {
        return equippedCharactorImage;
    }


}
