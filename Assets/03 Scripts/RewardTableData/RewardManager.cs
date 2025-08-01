using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

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
            GameManager.Instance.account.AddGold(reward.goldReward);
        }

        if (reward.goldReward >= 0)
        {
            GameManager.Instance.account.AddDiamond(reward.diaReward);
        }
        
        if (reward.speedMoveReward)
        {
            ItemData item = itemManager.GetItem("이동속도 주문서");
            ItemManager.instance.AddItem(item);
        }
        
        if (reward.sandGlassReward)
        {
           ItemData item = itemManager.GetItem("마법의 모래시계 +10");
            if (item != null) ItemManager.instance.AddItem(item);
        }
        
        if (reward.topTicketReward)
        {
            ItemData data = ItemManager.instance.GetItem("타워 입장권 1매");
            if (data != null) ItemManager.instance.AddItem(data);
        }
    }

    public void GiveRewards(RewardTableData rewards)
    {
        GiveReward(rewards);
        Debug.Log($"지급된 보상 :  {rewards.goldReward}, {rewards.diaReward},{rewards.speedMoveReward},{rewards.sandGlassReward},{rewards.topTicketReward}");
    }
}
