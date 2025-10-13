using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string _androidGameId;
    [SerializeField] private string _iosGameId;
    [SerializeField] private bool _testMode = true;
    private string _gameId;

    private void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        #if UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_IOS
        _gameId = _iosGameId; 
#elif UNITY_EDITOR
        _gameId = _androidGameId;
        #endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode,this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("유니티 광고 초기화 되었습니다");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"유니티 광고 초기화 실패{error.ToString()}-{message}");
    }
}
