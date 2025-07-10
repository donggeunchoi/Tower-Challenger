using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AllClear : MonoBehaviour
{

    StageManager stageManager;
    public RewardBase rewardBase;
    private RewardManager rewardManager;

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
        // if (reward != null)
        //     reward = null;

        UpdateRewardGold();
        UpdateRewardDia();
        //추가되는 랜덤 아이템을 받아서
        //랜덤아이템 자료형 list + for (int i = 0; i <= 랜덤아이템.length; i++) 인스턴스 이미지

        stageManager = StageManager.instance;
        rewardManager = this.GetComponent<RewardManager>();
        
        GiveTowerReward();
        nextFloor.onClick.AddListener(OnClickNextStage);
        returnToTitle.onClick.AddListener(OnclickReturnTitle);

        nextFloorText.text = $"Clear {stageManager.floor} floor";
        stageManager.stageTimer.StopTimer();
    }
    private void OnClickNextStage()
    {
        stageManager.NextFloor();
        Destroy(this.gameObject);
    }

    private void OnclickReturnTitle()
    {
        SceneManager.LoadScene(_mainSceneName);
        StageManager.instance.infoUI.SetActive(false);
        Destroy(this.gameObject);
    }

    void GiveTowerReward()
    {
        if (rewardBase == null || rewardBase.rewards.Count == 0)
        {
            rewardBase = FindObjectOfType<RewardBase>();
            if (rewardBase == null)
            {
                Debug.LogError("없어요 rewardBase가");
                return;
            }
        }

        if (rewardBase.rewards == null)
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
        
        List<RewardTableData> currentRewards = rewardBase.rewards.FindAll(r => r.floor == currentFloor);

        if (currentRewards.Count == 0)
        {
            Debug.Log("해당층에 보상이 없데이");
            return;
        }

        if (RewardManager.Instance == null)
        {
            Debug.Log("여기구나?");
        }
        rewardManager.GiveRewards(currentRewards);
        
    }

    public void UpdateRewardGold()
    {
        rewardBase = FindObjectOfType<RewardBase>();
        
        int currentFloor = StageManager.instance.floor;
        
        List<RewardTableData> currentRewards = rewardBase.rewards.FindAll(r => r.floor == currentFloor);
        
        goldReward.sprite = ItemManager.instance.GoldSprite;
        goldRewardText.text =  currentRewards.Count > 0 ? currentRewards[0].goldReward.ToString() : "0";
        
    }

    public void UpdateRewardDia()
    {
        rewardBase = FindObjectOfType<RewardBase>();
        
        int currentFloor = StageManager.instance.floor;
        
        List<RewardTableData> currentRewards = rewardBase.rewards.FindAll(r => r.floor == currentFloor);

        if (currentRewards[0].diaReward != 0)
        {
            diamondReward.sprite = ItemManager.instance.DiamondSprite;
            diamondRewardText.text = currentRewards.Count > 0 ? currentRewards[0].diaReward.ToString() : "0";
        }
        else
        {
            unenalble();
        }
        
        
    }

    void unenalble()
    {
        diamondReward.gameObject.SetActive(false);
        diamondRewardText.text = "";
    }
    
}
