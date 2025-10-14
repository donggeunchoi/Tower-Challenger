using UnityEngine;

public class Ads : MonoBehaviour
{
    public GameObject AdsPanel;

    public void OnClickCloseAds()
    {
        AdsPanel.SetActive(false);
    }
    
}
