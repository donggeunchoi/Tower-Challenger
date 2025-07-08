using UnityEngine;

public class MailBox : MonoBehaviour
{
    public GameObject mailPaper;

    public void OnClickMailPaperOpen()
    {
        mailPaper.SetActive(true);
    }
    
    public void OnClickMailBoxClose()
    {
        mailPaper.SetActive(false);
    }
}
