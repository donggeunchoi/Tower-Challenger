using UnityEngine;
using UnityEngine.UI;

public class CharactorChoiceSlot : MonoBehaviour
{
    // public string charactorName;
    public bool Equip = false;
    public Image characterImage;
    public GameObject EquipImage;
    public CharactorChoice CharactorChoice;
    
    public CharacterData data;

    private void Start()
    {
        EquipImage.SetActive(false);
    }

    public void Init()
    {
        if (data != null)
        {
            characterImage.sprite = data.characterImage;
        }
    }

    public void OnClickUse()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.equimentCharacter = data;
            CharactorChoice.OnCharacterEquipped();
            EquipImage.SetActive(true);
        }
    }
}
