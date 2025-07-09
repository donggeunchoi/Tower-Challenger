using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AllClear : MonoBehaviour
{

    StageManager stageManager;
    public RewardBase rewardBase;

    [SerializeField] private TextMeshProUGUI nextFloorText;
    //아이템 자료형 추가되는 랜덤아이템 관리

    [SerializeField] private Image reward;

    [SerializeField] private Transform rewardImage;

    [SerializeField] private Button returnToTitle;
    [SerializeField] private Button nextFloor;

    private const string _mainSceneName = "VillageScene";  //메인씬 이름

    private void Start()
    {
        if (reward != null)
            reward = null;

        //추가되는 랜덤 아이템을 받아서
        //랜덤아이템 자료형 list + for (int i = 0; i <= 랜덤아이템.length; i++) 인스턴스 이미지

        stageManager = StageManager.instance;

        // Debug.Log(RewardManager.Instance.name);
        // GiveTowerReward();
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
        rewardBase = FindObjectOfType<RewardBase>();
        
        List<RewardTableData> rewards = rewardBase.rewards;
        RewardManager.Instance.GiveRewards(rewards);
    }
}
