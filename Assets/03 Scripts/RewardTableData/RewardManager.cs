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
        if (reward.goldReward >= 0)
        {
            GameManager.Instance.AddGold(reward.goldReward);
        }

        if (reward.goldReward >= 0)
        {
            GameManager.Instance.AddDiamond(reward.diaReward);
        }
        
        // if (reward.speedMoveReward)
        // {
        //     ItemData item = itemManager.GetItem("이동속도 주문서");
        //     ItemManager.instance.AddItem(item);
        // }
        //
        // if (reward.sandGlassReward)
        // {
        //    ItemData item = itemManager.GetItem("마법의 모래시계 +10");
        //     if (item != null) ItemManager.instance.AddItem(item);
        // }
        //
        // if (reward.topTicketReward)
        // {
        //     ItemData data = ItemManager.instance.GetItem("타워 입장권 1매");
        //     if (data != null) ItemManager.instance.AddItem(data);
        // }
    }

    public void GiveRewards(List<RewardTableData> rewards)
    {
        foreach (RewardTableData reward in rewards)
        {
            Debug.Log($"지급된 보상 :  {reward.goldReward}, {reward.diaReward},{reward.speedMoveReward},{reward.sandGlassReward},{reward.topTicketReward}");
            GiveReward(reward);
        }
    }
}
