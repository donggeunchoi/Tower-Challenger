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
    
    public void AddItem(string CharactorName, Image CharactorIcon)
    {
        GameObject newSlot = Instantiate(SlotPrefab, SlotParent);
        newSlot.SetActive(true);
        CharactorSlots.Add(newSlot);
        
        TMPro.TMP_Text nameText = newSlot.GetComponentInChildren<TMPro.TMP_Text>();
        Image icon =  newSlot.GetComponent<Image>();

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
    
    
}
