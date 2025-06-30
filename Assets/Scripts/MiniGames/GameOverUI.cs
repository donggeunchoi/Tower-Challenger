using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button returnToTitle;
    [SerializeField] private Button nextFloor;
    StageManager stageManager;
    private const string _mainSceneName = "VillageScene";  //메인씬 이름
    
    private void Start()
    {
        stageManager = StageManager.instance;
        nextFloor.onClick.AddListener(OnClickNextStage);
        returnToTitle.onClick.AddListener(OnclickReturnTitle);
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
}
