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
    
    [Header("이미지")]
    public Image Charactor1Image;
    public Image Charactor2Image;
    public Image Charactor3Image;
    public Image Charactor4Image;
    public Image Charactor5Image;
    
    
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
        Debug.Log(Charactor1Image.sprite.name);
        CharactorChoice.instance.AddItem("Luke", Charactor1Image);
        ClearImage[0].SetActive(true);
        Destroy(targetButton[0]);
    }

    public void Buy_Rin()
    {
        CharactorChoice.instance.AddItem("Rin", Charactor2Image);
        ClearImage[1].SetActive(true);
        Destroy(targetButton[1]);
    }
    public void Buy_Mir()
    {
        CharactorChoice.instance.AddItem("Mir",Charactor3Image);
        ClearImage[2].SetActive(true);
        Destroy(targetButton[2]);
    }

    public void Buy_Kain()
    {
        CharactorChoice.instance.AddItem("Kain", Charactor4Image);
        ClearImage[3].SetActive(true);
        Destroy(targetButton[3]);
    }

    public void Buy_Meryl()
    {
        CharactorChoice.instance.AddItem("Meryl", Charactor5Image);
        ClearImage[4].SetActive(true);
        Destroy(targetButton[4]);
    }
}
