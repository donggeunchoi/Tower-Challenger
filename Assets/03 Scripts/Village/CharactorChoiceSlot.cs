using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharactorChoiceSlot : MonoBehaviour
{
    // public string charactorName;
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
            if (GameManager.Instance.equimentCharacter == data)
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
            GameManager.Instance.equimentCharacter = data;
            CharactorChoice.OnCharacterEquipped();
            EquipImage.SetActive(true);
            GameManager.Instance.SaveData();
        }
    }
}
