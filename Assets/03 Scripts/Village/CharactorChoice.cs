using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactorChoice : MonoBehaviour
{
    public GameObject CharactorChoicePanel;
    public Transform SlotParent;
    public GameObject SlotPrefab;
    public List<CharactorChoiceSlot> CharactorSlots = new List<CharactorChoiceSlot>();

    public CharactorChoice instance;
    public Image CharactorIcon;

    private void Awake() 
    {
        instance = this;
    }
    
    private void Start()  
    {
        UpdateCharacterData();
    }

    public void UpdateCharacterData()
    {
        foreach (CharactorChoiceSlot slot in CharactorSlots)
            Destroy(slot.gameObject);
        CharactorSlots.Clear();

        foreach (CharacterData data in GameManager.Instance.character.charaters)
        {
            GameObject slotObj = Instantiate(SlotPrefab, SlotParent);
            var slot = slotObj.GetComponent<CharactorChoiceSlot>();
            slot.data = data;
            slot.CharactorChoice = this;
            slot.Init(); 
            CharactorSlots.Add(slot);

            bool isEquipped = (GameManager.Instance.character.equippedCharacter != null &&
                               GameManager.Instance.character.equippedCharacter.characterName == data.characterName);
            slot.EquipImage.SetActive(isEquipped);
        }
    }

    public void OnCharacterEquipped()
    {
        foreach (var slot in CharactorSlots)
        {
            bool isEquipped = (GameManager.Instance.character.equippedCharacter != null &&
                               GameManager.Instance.character.equippedCharacter.characterName == slot.data.characterName);
            slot.EquipImage.SetActive(isEquipped);
        }
    }

    public void OnClickCharactorClose()
    {
        CharactorChoicePanel.SetActive(false);
    }
}
