using UnityEngine;

public class Ads : MonoBehaviour
{
    public GameObject AdsPanelDia;
    public GameObject AdsPanelCount;

    public void OnClickCloseAds()
    {
        AdsPanelDia.SetActive(false);
    }

    public void OnClickCloseAdsCount()
    {
        AdsPanelCount.SetActive(false);
    }
    
}
