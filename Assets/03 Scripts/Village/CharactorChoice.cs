using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (GameManager.Instance != null)
            GameManager.Instance.playerData.LoadData();

        UpdateCharacterData();
    }

    public void UpdateCharacterData()
    {
        foreach (CharactorChoiceSlot slot in CharactorSlots)
            Destroy(slot.gameObject);
        CharactorSlots.Clear();

        foreach (CharacterData data in GameManager.Instance.charactors)
        {
            GameObject slotObj = Instantiate(SlotPrefab, SlotParent);
            var slot = slotObj.GetComponent<CharactorChoiceSlot>();
            slot.data = data;
            slot.CharactorChoice = this;
            slot.Init(); 
            CharactorSlots.Add(slot);

            slot.EquipImage.SetActive(GameManager.Instance.equimentCharacter == data);
        }
    }

    public void OnCharacterEquipped()
    {
        foreach (var slot in CharactorSlots)
        {
            bool isEquipped = (GameManager.Instance.equimentCharacter == slot.data);
            slot.EquipImage.SetActive(isEquipped);
        }
    }

    public void OnClickCharactorClose()
    {
        CharactorChoicePanel.SetActive(false);
    }
}
