using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GiveReward(RewardTableData reward)
    {
        switch (reward.name)
        {
            case "gold":
                GameManager.Instance.AddGold(reward.count);
                break;
            case "dia":
                GameManager.Instance.AddDiamond(reward.count);
                break;
            
            default:
                Debug.Log("알 수 없는 보상");
                break;
        }
    }

    public void GiveRewards(List<RewardTableData> rewards)
    {
        foreach (RewardTableData reward in rewards)
        {
            GiveReward(reward);
        }
    }
}
