using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AllClear : MonoBehaviour
{

    StageManager stageManager;
    // public RewardBase rewardBase;
    private RewardManager rewardManager;
    private ItemManager itemManager;
    

    [SerializeField] private TextMeshProUGUI nextFloorText;
    //아이템 자료형 추가되는 랜덤아이템 관리

    [Header("골드")]
    [SerializeField] private Image goldReward;
    [SerializeField] private TMP_Text goldRewardText;
    
    [Header("다이아")]
    [SerializeField] private Image diamondReward;
    [SerializeField] private TMP_Text diamondRewardText;

    [Header("아이템")] 
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemNameText;

    [SerializeField] private Transform rewardImage;

    [SerializeField] private Button returnToTitle;
    [SerializeField] private Button nextFloor;

    private const string _mainSceneName = "VillageScene";  //메인씬 이름

    private void Start()
    {
        stageManager = StageManager.instance;
        rewardManager = this.GetComponent<RewardManager>();

        CVSLoader.LoadRewardCVS();

        UpdateRewardGold();
        UpdateRewardDia();
        UpdateRewrdItem();
        
        nextFloor.onClick.AddListener(OnClickNextStage);
        returnToTitle.onClick.AddListener(OnclickReturnTitle);

        nextFloorText.text = $"Clear {stageManager.floor} floor";
        stageManager.stageTimer.StopTimer();
    }
    private void OnClickNextStage()
    {
        GiveTowerReward();
        stageManager.NextFloor();
        Destroy(this.gameObject);
    }

    private void OnclickReturnTitle()
    {
        GiveTowerReward();
        SceneManager.LoadScene(_mainSceneName);
        StageManager.instance.infoUI.SetActive(false);
        Destroy(this.gameObject);
    }

    void GiveTowerReward()
    { 
        if (CVSLoader.rewardTableDataList == null)
        {
            Debug.LogError("RewardBase.rewards가 없어요");
            return;
        }
        
        if (StageManager.instance == null)
        {
            Debug.LogError("StageManager.instance가 null입니다. StageManager가 씬에 존재하는지 확인하세요.");
            return;
        }
        
        int currentFloor = StageManager.instance.floor;
        
        RewardTableData currentReward = CVSLoader.rewardTableDataList.Find(r => r.floor == currentFloor);

        if (RewardManager.Instance == null)
        {
            Debug.Log("여기구나?");
        }
        rewardManager.GiveRewards(currentReward);
        
    }

    public void UpdateRewardGold()
    {   
        int currentFloor = StageManager.instance.floor;
        
        List<RewardTableData> currentRewards = CVSLoader.rewardTableDataList.FindAll(r => r.floor == currentFloor);
        
        goldReward.sprite = ItemManager.instance.GoldSprite;
        goldRewardText.text =  currentRewards.Count > 0 ? currentRewards[0].goldReward.ToString() : "0";
        
    }

    public void UpdateRewardDia()
    {   
        int currentFloor = StageManager.instance.floor;
        
        List<RewardTableData> currentRewards = CVSLoader.rewardTableDataList.FindAll(r => r.floor == currentFloor);

        if (currentRewards[0].diaReward != 0)
        {
            diamondReward.sprite = ItemManager.instance.DiamondSprite;
            diamondRewardText.text = currentRewards.Count > 0 ? currentRewards[0].diaReward.ToString() : "0";
        }
        else
        {
            diamondReward.gameObject.SetActive(false);
            diamondRewardText.text = "";
        }
        
    }

    public void UpdateRewrdItem()
    {   
        int currentFloor = StageManager.instance.floor;

        List<RewardTableData> currentRewards = CVSLoader.rewardTableDataList.FindAll(r => r.floor == currentFloor);

        if (currentRewards.Count == 0) return;

        if (currentRewards[0].speedMoveReward)
        {
            ShowItem("이동속도 주문서");
        }
        else if (currentRewards[0].sandGlassReward)
        {
            ShowItem("마법의 모래시계 +10");
        }
        else if (currentRewards[0].topTicketReward)
        {
            ShowItem("타워 입장권 1매");
        }
        else
        {
            itemImage.gameObject.SetActive(false);
            itemNameText.text = "";
        }

    }

    private void ShowItem(string itemName)
    {
        if (itemManager == null)
        {
            itemManager = FindObjectOfType<ItemManager>();
        }


        foreach (ItemData item in itemManager.rewardsItmes)
        {
            if (item.itemName.Trim() == itemName.Trim())
            {
                itemImage.gameObject.SetActive(true);
                itemImage.sprite = item.icon;
                itemNameText.text = item.itemName;
                return; // 찾았으면 종료
            }
        }
    }
}
