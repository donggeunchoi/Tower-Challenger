using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button returnToTitle;
    
    StageManager stageManager;
    private const string _mainSceneName = "VillageScene";  //메인씬 이름
    
    private void Start()
    {
        stageManager = StageManager.instance;
        returnToTitle.onClick.AddListener(OnclickReturnTitle);
        stageManager.stageTimer.StopTimer();
    }

    private void OnclickReturnTitle()
    {
        SceneManager.LoadScene(_mainSceneName);
        stageManager.infoUI.SetActive(false);
        Destroy(this.gameObject);
    }
}
