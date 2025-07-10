using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance;
    public ItemManager itemManager;
    

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
                GameManager.Instance.AddGold(reward.goldReward);
                break;
            case "dia":
                GameManager.Instance.AddDiamond(reward.diaReward);
                break;
            case "Item":
                ItemData data = ItemManager.instance.GetItem(reward.name);
                if (data != null)
                {
                    ItemManager.instance.AddItem(data);
                }
                else
                {
                    Debug.Log($"{reward.name}이 없어요");
                }
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
            Debug.Log($"지급된 보상 :  {reward.name}");
            GiveReward(reward);
        }
    }
}
