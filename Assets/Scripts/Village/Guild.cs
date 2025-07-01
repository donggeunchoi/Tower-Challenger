using UnityEngine;

public class Guild : MonoBehaviour
{
    [Header("길드")]
    public GameObject GuildPanel;
    public GameObject RadManPanel;
    public GameObject GreenPanel;
    public GameObject NoonaPanel;

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
}
