using UnityEngine;

public class MailBox : MonoBehaviour
{
    public GameObject mailBox;
    
    
    public void OnClickMailBoxClose()
    {
        mailBox.SetActive(false);
    }
}
