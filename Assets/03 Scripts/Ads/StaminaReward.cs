using UnityEngine;

public class StaminaReward : MonoBehaviour
{
    [SerializeField] private RewardedAds rewardedAds;

    public void OnClickWatchAd()
    {
        rewardedAds.ShowAd(RewardType.Stamina);
    }
}
