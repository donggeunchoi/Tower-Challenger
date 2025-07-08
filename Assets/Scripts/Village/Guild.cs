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
    public CharacterData lukeData;
    public CharacterData rinData;
    public CharacterData mirData;
    public CharacterData kainData;
    public CharacterData merylData;
    
    
    public GameObject[] ClearImage;
    public Button[] targetButton;

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
    
    public void Buy_Luke()
    {
        CharactorChoice.instance.AddItem(lukeData);
        ClearImage[0].SetActive(true);
        Destroy(targetButton[0]);
    }

    public void Buy_Rin()
    {
        CharactorChoice.instance.AddItem(rinData);
        ClearImage[1].SetActive(true);
        Destroy(targetButton[1]);
    }
    public void Buy_Mir()
    {
        CharactorChoice.instance.AddItem(mirData);
        ClearImage[2].SetActive(true);
        Destroy(targetButton[2]);
    }

    public void Buy_Kain()
    {
        CharactorChoice.instance.AddItem(kainData);
        ClearImage[3].SetActive(true);
        Destroy(targetButton[3]);
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UseDiamond(kainData.Price);
        }
        
    }

    public void Buy_Meryl()
    {
        CharactorChoice.instance.AddItem(merylData);
        ClearImage[4].SetActive(true);
        Destroy(targetButton[4]);
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UseDiamond(merylData.Price);
        }
        
    }
}
