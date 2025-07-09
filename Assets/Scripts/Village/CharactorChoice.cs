using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactorChoice : MonoBehaviour
{
    public GameObject CharactorChoicePanel;
    public Transform SlotParent;
    public GameObject SlotPrefab;

    public List<GameObject> CharactorSlots = new List<GameObject>();
    

    public static CharactorChoice instance;
    public Image CharactorIcon;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        UpdateCharacterData();
    }

    private void Start()
    {
        if(GameManager.Instance != null)
            GameManager.Instance.playerData.LoadData();

        UpdateCharacterData();
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

    public void UpdateCharacterData()
    {
        foreach (GameObject slot in CharactorSlots)
        {
            Destroy(slot);
        }
        CharactorSlots.Clear();

        if (GameManager.Instance.charactors.Count > 0)
        {
            for (int i = 0; i < GameManager.Instance.charactors.Count; i++)
            {
                AddItem(GameManager.Instance.charactors[i]);
            }
        }

        if (GameManager.Instance.equimentCharacter != null)
        {
            foreach (GameObject slotObject in CharactorSlots)
            {
                CharactorChoiceSlot slot = slotObject.GetComponent<CharactorChoiceSlot>();
                if (slot != null && slot.data == GameManager.Instance.equimentCharacter)
                {
                    slot.Equip = true;
                    slot.EquipImage.SetActive(true);
                }
                else if (slot != null)
                {
                    slot.Equip = false;
                    slot.EquipImage.SetActive(false);
                }
            }
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
                GameManager.Instance.equimentCharacter = slot.data;
            }
        }
        
        if (CharactorIcon != null && Choiceslot.data != null)
        {
            CharactorIcon.sprite = Choiceslot.data.characterImage;
        }

        UpdateCharacterData();
    }
}
