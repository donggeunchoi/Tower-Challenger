using UnityEngine;
using UnityEngine.UI;

public class MailBox : MonoBehaviour
{
    public GameObject mailPaper;
    public int firstGift;
    public Button GetButton;

    public void OnClickMailPaperOpen()
    {
        mailPaper.SetActive(true);
    }
    
    public void OnClickMailBoxClose()
    {
        mailPaper.SetActive(false);
    }

    public void OnClickGetButton()
    {
        GameManager.Instance.AddGold(firstGift);
        Destroy(GetButton.gameObject);
    }
}
