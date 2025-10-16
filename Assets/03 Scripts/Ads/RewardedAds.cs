using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public enum RewardType
{
    None,
    Stamina,
    Diamond
}

public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
   
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms

    [SerializeField] private int tryCount;
    private RewardType currentRewardType = RewardType.None;
 
    void Awake()
    {   
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOsAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#elif UNITY_EDITOR
        _adUnitId = _androidAdUnitId;
#endif

        Debug.Log($"✅ Interstitial Ad Unit 설정 완료: {_adUnitId}");

    }
 
    // Call this public method when you want to get an ad ready to show.
    public void LoadAd()
    {
        if (tryCount <= 0)
        {
            Debug.Log("광고 횟수 제한 도달. 더 이상 로드하지 않습니다.");
            return;
        }

        Debug.Log("광고 로드 시도: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }
 
    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
    }
 
    // Implement a method to execute when the user clicks the button:
    public void ShowAd(RewardType rewardType)
    {
        currentRewardType = rewardType;
        Debug.Log(currentRewardType);
        // Then show the ad:
        Advertisement.Show(_adUnitId, this);
    }
 
    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("보상 연결 해야하고 카운트 낮추기");
            // Grant a reward.
            tryCount--;

            switch (currentRewardType)
            {
                case RewardType.Stamina:
                    StaminaReward();
                    break;
                case RewardType.Diamond:
                    DiamondReward();
                    break;
                default:
                    Debug.LogWarning("RewardType.None");
                    break;
            }
            
            LoadAd();
            
        }
    }
 
    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    private void StaminaReward()
    {
        Debug.Log("스테미나 보상 지금 요망");
    }

    private void DiamondReward()
    {
        Debug.Log("다이아 보상 지급 요망");
    }
}
