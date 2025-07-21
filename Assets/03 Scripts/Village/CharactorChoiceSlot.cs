using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharactorChoiceSlot : MonoBehaviour
{
    public bool Equip = false;
    public Image characterImage;
    public GameObject EquipImage;
    [SerializeField] private TextMeshProUGUI characterName;
    public CharactorChoice CharactorChoice;
    
    public CharacterData data;

    public void Init()
    {
        if (data != null)
        {
            characterImage.sprite = data.characterImage;
            characterName.text = data.characterName;
        }

        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.character.equippedCharacter != null &&
                GameManager.Instance.character.equippedCharacter.characterName == data.characterName)
                EquipImage.SetActive(true);
        }
        else
        {
            EquipImage.SetActive(false);
        }
    }

    public void OnClickUse()
    {
        if (GameManager.Instance != null)
        {   
            GameManager.Instance.character.EquipCharacter(data.characterName);
            CharactorChoice.OnCharacterEquipped();
            EquipImage.SetActive(true);
            GameManager.Instance.SaveData();
        }
    }
}
