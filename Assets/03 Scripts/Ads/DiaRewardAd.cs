using UnityEngine;

public class DiaRewardAd : MonoBehaviour
{
    [SerializeField] private RewardedAds rewardedAds;

    public void OnClickWatchAd()
    {
        rewardedAds.ShowAd(RewardType.Diamond);
    }
}
