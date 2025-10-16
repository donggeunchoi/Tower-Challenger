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
   [Header("UI Buttons")]
   [SerializeField] private Button _staminaButton;
   [SerializeField] private Button _diamondButton;
   
   [Header("RewardCount")]
   [SerializeField] private int staminaTryCount;
   [SerializeField] private int diamondTryCount;
    
    
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms
    
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
        Debug.Log("광고 로드 시도: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }
 
    // If the ad successfully loads, add a listener to the button and enable it:
    public void ShowAd(RewardType rewardType)
    {
        currentRewardType = rewardType;

        if (rewardType == RewardType.Stamina && staminaTryCount <= 0)
        {
            UpdateButtonState();
            return;
        }

        if (rewardType == RewardType.Diamond && diamondTryCount <= 0)
        {
            UpdateButtonState();
            return;
        }
        
        Debug.Log(currentRewardType);
        Advertisement.Show(_adUnitId, this);
    }
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
        UpdateButtonState();
    }
    
    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("보상 연결 해야하고 카운트 낮추기");
            // Grant a reward.

            switch (currentRewardType)
            {
                case RewardType.Stamina:
                    staminaTryCount--;
                    StaminaReward();
                    break;
                case RewardType.Diamond:
                    diamondTryCount--;
                    DiamondReward();
                    break;
                default:
                    Debug.LogWarning("RewardType.None");
                    break;
            }
            
            LoadAd();
            UpdateButtonState();
            
        }
    }
 
 
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        LoadAd();
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    private void StaminaReward()
    {
        Debug.Log("스테미나 보상 지금 요망");
        GameManager.Instance.stamina.AddStamina();
    }

    private void DiamondReward()
    {
        Debug.Log("다이아 보상 지급 요망");
    }

    private void UpdateButtonState()
    {
        if (_staminaButton != null)
            _staminaButton.interactable = staminaTryCount > 0;

        if (_diamondButton != null)
            _diamondButton.interactable = diamondTryCount > 0;
    }
}
