using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiaRewardAd : MonoBehaviour
{
    [SerializeField] private TMP_Text countText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Button showAdButton;
    [SerializeField] private RewardedAds rewardedAds;

    private PlayerData data;
    
    void Start()
    {
        data = GameManager.Instance.playerData;
        RewardedAds.OnRewardDataChanged += UpdateUI;
        UpdateUI();
        StartCoroutine(UpdateTimer());
    }

    private void OnDestroy()
    {
        RewardedAds.OnRewardDataChanged -= UpdateUI;
    }

    private void UpdateUI()
    {
        countText.text = $"{data.diamondAdRemaining}/3";
        showAdButton.interactable = data.diamondAdRemaining > 0;
    }

    private IEnumerator UpdateTimer()
    {
        while (true)
        {
            DateTime now = DateTime.Now;
            DateTime resetTime = DateTime.Today.AddDays(1).AddSeconds(-1);
            TimeSpan remain = resetTime - now;

            if (remain.TotalSeconds <= 0)
            {
                timerText.text = "";
                RewardedAds.CheckResetAdData(data);
                UpdateUI();
            }
            else
            {
                timerText.text = $"{remain.Hours:D2}:{remain.Minutes:D2}:{remain.Seconds:D2}";
            }
            yield return new WaitForSeconds(1);
        }
    }
    public void OnClickWatchAd()
    {
        rewardedAds.ShowAd(RewardType.Diamond);
    }
}
