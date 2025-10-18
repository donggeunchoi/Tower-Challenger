using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using Random = UnityEngine.Random;

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
    
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    // [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms
    
    private RewardType currentRewardType = RewardType.None;
    
    public int RandomDia;
    private PlayerData data;
    
 
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

    void Start()
    {
        data = GameManager.Instance.playerData;
        CheckResetAdData(data);
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
        CheckResetAdData(data);
        
        currentRewardType = rewardType;

        if (rewardType == RewardType.Stamina && data.staminaAdRemaining <= 0)
        {
            UpdateButtonState();
            return;
        }

        if (rewardType == RewardType.Diamond && data.diamondAdRemaining <= 0)
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
                    data.staminaAdRemaining--;
                    Debug.Log(data.staminaAdRemaining);
                    StaminaReward();
                    break;
                case RewardType.Diamond:
                    data.diamondAdRemaining--;
                    Debug.Log(data.diamondAdRemaining);
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
        RandomDia = GetRandomNum();
        GameManager.Instance.account.AddDiamond(RandomDia);
    }

    private void UpdateButtonState()
    {
        if (_staminaButton != null)
            _staminaButton.interactable = data.staminaAdRemaining > 0;

        if (_diamondButton != null)
            _diamondButton.interactable = data.diamondAdRemaining > 0;
    }

    private int GetRandomNum()
    {
        int totalNum = 0;
        int[] nums = new int[30];

        for (int i = 0; i < 6; i++)
        {
            nums[i] = 3;
        }

        for (int i = 6; i < 30; i++)
        {
            nums[i] = 1;
        }

        foreach (int w in nums)
        {
            totalNum += w;
        }
        
        int randomNum = Random.Range(1, totalNum + 1);

        int cumulative = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            cumulative += nums[i];
            if (randomNum <= cumulative)
            {
                return i + 1;
            }
        }

        return 30;
    }

    public static void CheckResetAdData(PlayerData data)
    {
        string today = DateTime.Now.ToString("yyyyMMdd");
        if (data.adLastResetDate != today)
        {
            data.staminaAdRemaining = 3;
            data.diamondAdRemaining = 3;
            data.adLastResetDate = today;
            
            data.SaveData();
            Debug.Log("광고 횟수 자정 리셋 완료");
        }
    }
}
