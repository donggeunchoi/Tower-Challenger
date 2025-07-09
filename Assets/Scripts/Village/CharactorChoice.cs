using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactorChoice : MonoBehaviour
{
    public GameObject CharactorChoicePanel;
    public Transform SlotParent;
    public GameObject SlotPrefab;

    public List<GameObject> CharactorSlots = new List<GameObject>();
    public List<CharacterData> charactors = new List<CharacterData>();
    public CharacterData equimentCharacter;

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

        if (charactors.Count < 0)
        {
            for (int i = 0; i < charactors.Count; i++)
            {
                AddItem(charactors[i]);
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
                equimentCharacter = slot.data;
            }
        }
        
        if (CharactorIcon != null && Choiceslot.data != null)
        {
            CharactorIcon.sprite = Choiceslot.data.characterImage;
        }

        UpdateCharacterData();
    }
}
