using UnityEngine;
using UnityEngine.UI;

public class Guild : MonoBehaviour
{
    [Header("길드")]
    public GameObject GuildPanel;
    public GameObject RadManPanel;
    public GameObject GreenPanel;
    public GameObject NoonaPanel;
    public GameObject KainPanel;
    public GameObject MerylPanel;

    [Header("캐릭터 데이터")]
    public CharacterData[] characterDatas;

    //public CharacterData lukeData;
    //public CharacterData rinData;
    //public CharacterData mirData;
    //public CharacterData kainData;
    //public CharacterData merylData;
    
    
    public GameObject[] ClearImage;
    public Button[] targetButton;

    private void Start()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.SaveLoad();

        for (int i = 0; i < targetButton.Length; i++)
        {
            targetButton[i].onClick.RemoveAllListeners();
        }

        for (int i = 0; i < targetButton.Length; i++)
        {
            int index = i;
            if (targetButton[i] != null && characterDatas[i] != null)
            targetButton[i].onClick.AddListener(() => OnClickCharactarBuy(index));
        }
    }

    public void OnClickRedMan()
    {
        RadManPanel.SetActive(true);
    }

    public void OnClickGreenMan()
    {
        GreenPanel.SetActive(true);
    }

    public void OnClickNoona()
    {
        NoonaPanel.SetActive(true);
    }

    public void OnClickKain()
    {
        KainPanel.SetActive(true);
    }

    public void OnClickMeryl()
    {
        MerylPanel.SetActive(true);
    }

    public void GuildClose()
    {
        GuildPanel.SetActive(false);
    }
    
    public void OnClickRedManClose()
    {
        RadManPanel.SetActive(false);
    }

    public void OnClickGreenManClose()
    {
        GreenPanel.SetActive(false);
    }

    public void OnClickNoonaClose()
    {
        NoonaPanel.SetActive(false);
    }

    public void OnClickKainClose()
    {
        KainPanel.SetActive(false);
    }

    public void OnClickMerylClose()
    {
        MerylPanel.SetActive(false);
    }

    public void OnClickCharactarBuy(int characterNum)
    {
        GameManager.Instance.charactors.Add(characterDatas[characterNum]);
        ClearImage[characterNum].SetActive(true);
        Destroy(targetButton[characterNum]);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.SaveLoad();
        }
    }
}
